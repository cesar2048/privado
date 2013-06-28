using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.IO;
using MathNet.Numerics.Statistics;
using Newtonsoft.Json;

namespace SpeechAnalyzer.Model
{
	class DataAnalysis
	{
		public static String SonicAnnotator = @"G:\Enrique\Documentos\Universidad Galileo\Semestres\Privado\software\sonic-annotator-1.0-win32\sonic-annotator.exe";

		public DataAnalysis() 
		{
 
		}


		/// <summary>
		/// Extracts the data using sonic annotator and generates the feature vector for each file
		/// </summary>
		/// <param name="sourceDirPath"></param>
		public void ReadTrainingFiles(String sourceDirPath)
		{
			DirectoryInfo srcDir = new DirectoryInfo(sourceDirPath);
			Dictionary<string, int> labels = null;

			if (!srcDir.Exists)
			{
				System.Diagnostics.Debug.WriteLine(String.Format("Directorio {0} no existe", sourceDirPath));
				return;
			}

			try
			{
				StreamReader sr = new StreamReader(Path.Combine(sourceDirPath, "labels.js"));
				String json = sr.ReadToEnd();
				labels = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
				sr.Close();
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine("Error leyendo archivo labels: " + e.Message);
				return;
			}

			FileInfo[] files = srcDir.GetFiles("*.wav");
			List<AudioFileFeatures> filesFeaturesList = new List<AudioFileFeatures>();

			// Add every matching file and its label to a list
			foreach (FileInfo file in files)
			{
				int labelValue = -1;
				foreach (String key in labels.Keys)
				{
					Regex regex = new Regex(key);
					if (regex.IsMatch(file.Name))
					{
						labelValue = labels[key];
						break;
					}
				}

				if (labelValue != -1)
				{
					AudioFileFeatures FileFeatures = new AudioFileFeatures()
					{
						fileInfo = file,
						label = labelValue
					};

					filesFeaturesList.Add(FileFeatures);
				}
			}

			// Generate features for every file
			foreach (AudioFileFeatures audio in filesFeaturesList)
			{
				GenerateFeatures(audio, sourceDirPath, "temp");
			}

			// get the shortest rowcount from all the mfcc matrices
			var query = from audio in filesFeaturesList
						select audio.mfcc.RowCount;
			
			int max = query.Max();
			int min = query.Min();

			foreach (AudioFileFeatures audio in filesFeaturesList)
			{
				DenseMatrix featureVector = DenseMatrix.Create(1, 1, (i, j) => 1);
				featureVector = featureVector.Append(ExtractMFCCFeatureVector(audio.mfcc, min, 20, false, true)) as DenseMatrix;
				audio.featureVector = featureVector;
			}
		}

		/// <summary>
		/// Uses sonic-annotator to extract the low level features, using the transform-descriptor.n3 file
		/// </summary>
		/// <param name="fileFeatures"></param>
		/// <param name="dataDir"></param>
		/// <param name="tempDir"></param>
		public void GenerateFeatures(AudioFileFeatures fileFeatures, String dataDir, String tempDir)
		{
			/*
				A1.wav
				A1_vamp_vamp-aubio_aubiopitch_frequency
				A1_vamp_qm-vamp-plugins_qm-mfcc_coefficients
				A1_vamp_vamp-aubio_aubiosilence_noisy
			 */
			String wavFileName	= Path.GetFileNameWithoutExtension(fileFeatures.fileInfo.Name);
			String wavFilePath	= fileFeatures.fileInfo.FullName.Replace("\\", "/");
			String filePitch	= String.Format("{0}_vamp_vamp-aubio_aubiopitch_frequency.csv", wavFileName);
			String fileMfcc		= String.Format("{0}_vamp_qm-vamp-plugins_qm-mfcc_coefficients.csv", wavFileName);
			String fileNoisi	= String.Format("{0}_vamp_vamp-aubio_aubiosilence_noisy.csv", wavFileName);

			filePitch	= Path.Combine(tempDir, filePitch);
			fileMfcc	= Path.Combine(tempDir, fileMfcc);
			fileNoisi	= Path.Combine(tempDir, fileNoisi);

			// execute sonic annotator
			String sonicArgs = String.Format("-t {0} \"{1}\" -w csv --csv-basedir {2} --csv-force",
				"transform-descriptor.n3",
				wavFilePath,
				tempDir
			);
			ProcessStartInfo pSonicInfo = new ProcessStartInfo()
			{
				FileName		= SonicAnnotator,
				Arguments		= sonicArgs,
				UseShellExecute	= false,
				CreateNoWindow	= true,
				RedirectStandardError = true
			};

			Process pSonic		= Process.Start(pSonicInfo);
			String sonicOutput	= pSonic.StandardError.ReadToEnd();
			pSonic.WaitForExit();

			if (sonicOutput.ToLower().Contains("error"))
			{
				System.Diagnostics.Debug.WriteLine(sonicOutput);
			}

			// read csv files into matrices
			DelimitedReader<DenseMatrix> matrixReader = new DelimitedReader<DenseMatrix>(",");
			fileFeatures.mfcc	= matrixReader.ReadMatrix(fileMfcc);
			fileFeatures.pitch	= matrixReader.ReadMatrix(filePitch);

			// remove last 9 rows of data, for some reason SonicAnnotator plugin adds 9 rows full of zeros
			fileFeatures.mfcc = fileFeatures.mfcc.SubMatrix(0, fileFeatures.mfcc.RowCount - 9, 0, fileFeatures.mfcc.ColumnCount) as DenseMatrix;

			// delete generated files
			foreach(String file in new String[] {filePitch, fileMfcc, fileNoisi} ) 
			{
				FileInfo fInfo = new FileInfo(file);
				fInfo.Delete();
			}

			System.Diagnostics.Debug.WriteLine(String.Format("Processed: {0}", fileFeatures.fileInfo.Name));
		}

		/// <summary>
		/// returns a 1xN row matrix with all the features
		/// </summary>
		/// <param name="mfccValues"></param>
		/// <param name="limit"></param>
		/// <param name="nCoeficients"></param>
		/// <param name="bData"></param>
		/// <param name="bStats"></param>
		/// <returns></returns>
		public DenseMatrix ExtractMFCCFeatureVector(DenseMatrix mfccValues, int limit, int nCoeficients, bool bData, bool bStats)
		{
			// zero length vectors are not supported
			DenseMatrix result = null;
			if (!bData && !bStats) throw new ArgumentException("at least one of bData or bStats must be true");
			nCoeficients	= Math.Min(mfccValues.ColumnCount, nCoeficients + 1);

			if (bStats)
			{
				for (int i = 1; i < nCoeficients; i++)
				{
					Vector column = mfccValues.Column(i) as Vector;
					if (result == null) {
						result = GetStatistics(column);
					} else {
						result = result.Append(GetStatistics(column)) as DenseMatrix;
					}
				}
			}

			if (bData)
			{
				double[] dValues = mfccValues
						.SubMatrix(0, mfccValues.RowCount, 1, nCoeficients)
						.ToColumnWiseArray();
				
				DenseMatrix dmValues = new DenseMatrix(1, dValues.Length, dValues);

				if (result == null) {
					result = dmValues;
				} else {
					result = result.Add(dmValues) as DenseMatrix;
				}
			}

			return result;
		}

		private DenseMatrix GetStatistics(Vector data)
		{
			double media	= data.Mean();
			double varianza = data.Variance();
			double max		= data.Maximum();
			double min		= data.Minimum();

			return new DenseMatrix(1, 4, new double[] { media, varianza, max, min });
		}

	}
}
