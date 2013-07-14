using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.IO;
using MathNet.Numerics.Statistics;


namespace SpeechAnalyzer.Model
{
	/// <summary>
	/// Stores a feature data set with information to separate into training/testing/validation sets
	/// </summary>
	public class FeaturesDataSet
	{
		/// <summary>
		/// Data saves the values as rows with the form: [ FeatureType, label, x0, x1, ... xn ]
		/// where FeatureType and label are double values
		/// </summary>
		public double[,] data;

		public FeaturesDataSet(DenseMatrix dataMat, bool validationSet = false)
		{
			DenseVector selector;
			FeaturesGenerator.SplitDataRandomly( dataMat.Column(0) as DenseVector, 0.8, out selector);

			selector.MapInplace(val => (val == 1) ? (double) FeatureType.TRAINING: (double) FeatureType.TESTING);
			
			DenseMatrix dataMatCopy = DenseMatrix.OfMatrix(dataMat);
			dataMatCopy.InsertColumn(0, selector);
			this.data = dataMatCopy.ToArray();
		}

		public void GetDataSet(FeatureType type, out DenseMatrix X, out DenseVector y)
		{
			DenseMatrix dataMat = DenseMatrix.OfArray(data);
			int count = dataMat.Column(0).Where(val => val == (double)type).Count();
			int n = dataMat.ColumnCount - 2;

			X = DenseMatrix.Create(count, n, (i, j) => 0);
			y = DenseVector.Create(count, (i) => 0);

			for (int i = 0, row = 0; i < dataMat.RowCount; i++)
			{
				if ( dataMat[0, i] == (double) type)
				{
					y[row] = dataMat[i, 0];
					X.SetRow(row, dataMat.Row(i).SubVector(2, n));
					row++;
				}
			}
		}
	}

	public enum FeatureType
	{
		TRAINING = 0,
		VALIDATION = 1,
		TESTING = 2
	}

	public class FeaturesGenerator
	{
		private String DataDirectory;
		private String TempDirectory;
		private String SonicAnnotator;

		public FeaturesGenerator(String dataDir, String tempDir, String sonicPath)
		{
			this.DataDirectory = dataDir;
			this.TempDirectory = tempDir;
			this.SonicAnnotator = sonicPath;
		}

		/// <summary>
		/// Generates the feature vectors for every file passed in the AudioInfosList argument
		/// </summary>
		/// <param name="AudioInfosList"></param>
		/// <param name="worker">Optional for progress reporting</param>
		public DenseMatrix ProcessFiles(List<AudioFileFeatures> AudioInfosList, Action<int> progressCallback = null)
		{
			DirectoryInfo srcDir = new DirectoryInfo(DataDirectory);
			if (!srcDir.Exists)
			{
				System.Diagnostics.Debug.WriteLine(String.Format("Directorio {0} no existe", DataDirectory));
				return null;
			}

			// Generate features for every file
			int mfccLimit = 55;
			int pitchLimit = 100;
			for (int count = 0; count < AudioInfosList.Count; count++)
			{
				AudioFileFeatures audio = AudioInfosList[count];

				// call sonic-annotator to generate the features
				GenerateFeatures(audio, this.DataDirectory, this.TempDirectory);

				// extract statistical information from the previous features
				DenseMatrix featureVector = DenseMatrix.Create(1, 1, (i, j) => audio.label);				// 1x1 matrix
				DenseMatrix mfccVector = ExtractFeatureVector(audio.mfcc, mfccLimit, 20, true, true);		// append row
				DenseMatrix pitchVector = ExtractFeatureVector(audio.pitch, pitchLimit, 1, false, true);	// apend row

				featureVector = featureVector.Append(mfccVector) as DenseMatrix;
				featureVector = featureVector.Append(pitchVector) as DenseMatrix;
				audio.featureVector = featureVector;

				// report progress
				if (progressCallback != null) progressCallback((count + 1) * 100 / AudioInfosList.Count);
			}

			var query = from file in AudioInfosList
						select file.featureVector.ColumnCount;

			int widthsCount = query.Distinct().Count();
			if (widthsCount > 1) throw new Exception("Feature vectors are not the same width, Aborted...");
			if (widthsCount == 0) throw new Exception("No file was processed");
			int width = query.First();

			// generate labels:features matrix
			DenseMatrix allFeatures = DenseMatrix.Create(AudioInfosList.Count, width, (i, j) => 0);
			for (int i = 0; i < AudioInfosList.Count; i++)
			{
				DenseMatrix features = AudioInfosList[i].featureVector;
				allFeatures.SetSubMatrix(i, 1, 0, features.ColumnCount, features);
			}

			return allFeatures;
		}

		/// <summary>
		/// sets the mfcc and pitch properties of the audioInfo argument
		/// using sonic-annotator to extract the low level features
		/// </summary>
		/// <param name="audioInfo"></param>
		/// <param name="dataDir"></param>
		/// <param name="tempDir"></param>
		private void GenerateFeatures(AudioFileFeatures audioInfo, String dataDir, String tempDir)
		{
			/*
				A1.wav
				A1_vamp_vamp-aubio_aubiopitch_frequency
				A1_vamp_qm-vamp-plugins_qm-mfcc_coefficients
				A1_vamp_vamp-aubio_aubiosilence_noisy
			 */
			String wavFileName = Path.GetFileNameWithoutExtension(audioInfo.fileInfo.Name);
			String wavFilePath = audioInfo.fileInfo.FullName.Replace("\\", "/");
			String filePitch = String.Format("{0}_vamp_vamp-aubio_aubiopitch_frequency.csv", wavFileName);
			String fileMfcc = String.Format("{0}_vamp_qm-vamp-plugins_qm-mfcc_coefficients.csv", wavFileName);

			filePitch = Path.Combine(tempDir, filePitch);
			fileMfcc = Path.Combine(tempDir, fileMfcc);

			// execute sonic annotator
			String sonicArgs = String.Format("-t {0} \"{1}\" -w csv --csv-basedir {2} --csv-force",
				"transform-descriptor.n3",
				wavFilePath,
				tempDir
			);
			ProcessStartInfo pSonicInfo = new ProcessStartInfo()
			{
				FileName = SonicAnnotator,
				Arguments = sonicArgs,
				UseShellExecute = false,
				CreateNoWindow = true,
				RedirectStandardError = true
			};

			Process pSonic = Process.Start(pSonicInfo);
			String sonicOutput = pSonic.StandardError.ReadToEnd();
			pSonic.WaitForExit();

			if (sonicOutput.ToLower().Contains("error"))
			{
				System.Diagnostics.Debug.WriteLine(sonicOutput);
			}

			// read csv files into matrices
			DelimitedReader<DenseMatrix> matrixReader = new DelimitedReader<DenseMatrix>(",");
			audioInfo.mfcc = matrixReader.ReadMatrix(fileMfcc);
			audioInfo.pitch = matrixReader.ReadMatrix(filePitch);

			// remove last 9 rows of data, for some reason SonicAnnotator plugin adds 9 rows full of zeros
			audioInfo.mfcc = audioInfo.mfcc.SubMatrix(0, audioInfo.mfcc.RowCount - 9, 0, audioInfo.mfcc.ColumnCount) as DenseMatrix;

			// delete generated files
			foreach (String file in new String[] { filePitch, fileMfcc })
			{
				FileInfo fInfo = new FileInfo(file);
				fInfo.Delete();
			}

			System.Diagnostics.Debug.WriteLine(String.Format("Processed: {0} -> {1}", audioInfo.fileInfo.Name, audioInfo.label));
		}

		/// <summary>
		/// returns a 1xN row matrix with the statistics values for the first {nColumns} columns in {values}
		/// </summary>
		/// <param name="values"></param>
		/// <param name="limit"></param>
		/// <param name="nColumns"></param>
		/// <param name="bData"></param>
		/// <param name="bStats"></param>
		/// <returns></returns>
		public DenseMatrix ExtractFeatureVector(DenseMatrix values, int limit, int nColumns, bool bData, bool bStats)
		{
			// zero length vectors are not supported
			DenseMatrix result = null;
			if (!bData && !bStats) throw new ArgumentException("at least one of bData or bStats must be true");
			nColumns = Math.Min(values.ColumnCount, nColumns + 1);

			if (bStats)
			{
				for (int i = 1; i < nColumns; i++)
				{
					Vector column = values.Column(i) as Vector;
					if (result == null)
					{
						result = GetStatistics(column);
					}
					else
					{
						result = result.Append(GetStatistics(column)) as DenseMatrix;
					}
				}
			}

			if (bData)
			{
				double[] dValues = values
						.SubMatrix(0, Math.Min(values.RowCount, limit), 1, nColumns - 1)
						.ToColumnWiseArray();

				DenseMatrix dmValues = new DenseMatrix(1, dValues.Length, dValues);

				if (result == null)
				{
					result = dmValues;
				}
				else
				{
					result = result.Append(dmValues) as DenseMatrix;
				}
			}

			return result;
		}

		/// <summary>
		/// Returns a 1x4 horizontal vector with statistics for the given vector
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private DenseMatrix GetStatistics(Vector data)
		{
			double media = data.Mean();
			double varianza = data.Variance();
			double max = data.Maximum();
			double min = data.Minimum();

			return new DenseMatrix(1, 4, new double[] { media, varianza, max, min });
		}

		/// <summary>
		/// Split the data as randomly as possible, but ensure that there are not missing classes on any output matrix
		/// </summary>
		/// <param name="data"></param>
		/// <param name="y"></param>
		/// <param name="proportion"></param>
		/// <param name="part1"></param>
		/// <param name="part2"></param>
		public static void SplitDataRandomly(DenseMatrix data, DenseVector y, double proportion, out DenseMatrix part1, out DenseMatrix part2)
		{
			int count1 = 0, count2 = 0, i = 0;
			DenseVector selector;

			part1 = new DenseMatrix((int)(data.RowCount * proportion), data.ColumnCount);
			part2 = new DenseMatrix(data.RowCount - part1.RowCount, data.ColumnCount);
			SplitDataRandomly(y, proportion, out selector);
			
			for (i = 0; i < data.RowCount; i++)
			{
				if (selector[i] == 1)
				{
					part1.SetRow(count1++, data.Row(i));
				}
				else
				{
					part2.SetRow(count2++, data.Row(i));
				}
			}
		}

		public static void SplitDataRandomly(DenseVector y, double proportion, out DenseVector selector)
		{
			Random rnd = new Random();
			int classes1, classes2, part1Count = (int) (y.Count * proportion );

			selector = DenseVector.Create(y.Count, i => 0);
			do
			{
				selector.MapInplace(x => (rnd.NextDouble() <= proportion) ? 1 : 0);

				classes1 = selector.PointwiseMultiply(y).Distinct().Count();						// remove class 0 as it doesn't exists
				classes2 = selector.Multiply(-1).Add(1).PointwiseMultiply(y).Distinct().Count();	// remove class 0 as it doesn't exists

			} while (selector.SumMagnitudes() != part1Count || classes1 != classes2);
		}


	}
}
