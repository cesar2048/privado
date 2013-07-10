using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.IO;
using MathNet.Numerics.LinearAlgebra.IO;
using MathNet.Numerics.Statistics;
using Newtonsoft.Json;
using DotNumerics.Optimization;
using System.ComponentModel;

namespace SpeechAnalyzer.Model
{
	class DataAnalysis
	{
		private String SonicAnnotator;
		private String DataDirectory;
		private String TempDirectory;
        private SQLiteDatabase db;

		// properties
		public NeuralNetworkParameters nnp { get; set; }
		public FileInfo trainingFile { get; set; }
		public FileInfo networkFile { get; set; }
		public FileInfo labelsFile { get; set; }

		// input properties
		public Double Lambda { get; set; }
		public Int32 Attempts { get; set; }
		public Int32 Iterations { get; set; }

		// output properties
		public String ConsoleOut { get; set; }
		public Double FinalCostValue { get; set; }
		public Double FinalAccuracy { get; set; }
		

		public DataAnalysis(String dataDir, String tempDir, String SonicAnotatorPath) 
		{
			this.SonicAnnotator = SonicAnotatorPath;
			this.TempDirectory = tempDir;
			this.DataDirectory = dataDir;

			this.FinalCostValue = Double.PositiveInfinity;
			this.ConsoleOut = "";
			this.Lambda = 0.1;
            db = new SQLiteDatabase();
			this.trainingFile = new FileInfo(Path.Combine(this.TempDirectory, "training-features.csv"));
			this.networkFile = new FileInfo(Path.Combine(TempDirectory, "networkParams.js"));
			this.labelsFile = new FileInfo(Path.Combine(TempDirectory, "labels.js"));
		}

		public String TestNeuralNetwork(String wavFilePath)
		{
			this.networkFile.Refresh();
			FileInfo wavFile = new FileInfo(wavFilePath);

			// Load parameters if these are not already loaded
			if (nnp == null && networkFile.Exists)
			{
				this.nnp = NeuralNetworkParameters.Load(networkFile.FullName);
			}
			if (nnp == null) throw new Exception("Neural network parameters are not loaded");
			if (!wavFile.Exists) throw new Exception("Test file doesn't exists");


			// generate features for the testing file
			List<AudioFileFeatures> trainingAudiosList = new List<AudioFileFeatures>();
			trainingAudiosList.Add(new AudioFileFeatures(wavFile, 0));

			DenseMatrix dataMat = ProcessFiles(trainingAudiosList);
			DenseMatrix X = dataMat.SubMatrix(0, dataMat.RowCount, 1, dataMat.ColumnCount - 1) as DenseMatrix;
			DenseVector y = dataMat.Column(0) as DenseVector;

			// execute neural network
			int[] predictions;
			NeuralNetwork nn = new NeuralNetwork(nnp);
			this.FinalAccuracy = nn.Predict(X, y, out predictions);

			// load labels
			Labels labels = LoadLabels();

			String label = "unknown";
			if (predictions[0] >= 0 && predictions[0] <= labels.labelsList.Count)
			{
				label = labels.labelsList[predictions[0]-1];
			}
			
			return label;
		}

		public void TrainNeuralNetwork()
		{
			this.trainingFile.Refresh();
			this.networkFile.Refresh();

			// load features
			if (!trainingFile.Exists) {
				throw new Exception("No features exists for training");
			}

			DelimitedReader<DenseMatrix> matrixReader = new DelimitedReader<DenseMatrix>(",");
			DenseMatrix dataMat = matrixReader.ReadMatrix(trainingFile.FullName);	// load the features matrix from csv file
			DenseMatrix mat1, mat2;
			NeuralNetwork.SplitDataRandomly(dataMat, dataMat.Column(0) as DenseVector, 0.8, out mat1, out mat2);

			//
			// execute machine learning process
			//
			DenseMatrix Xtrain = mat1.SubMatrix(0, mat1.RowCount, 1, mat1.ColumnCount - 1) as DenseMatrix;
			DenseVector ytrain = mat1.Column(0) as DenseVector;

			DenseMatrix Xtest = mat2.SubMatrix(0, mat2.RowCount, 1, mat2.ColumnCount - 1) as DenseMatrix;
			DenseVector ytest = mat2.Column(0) as DenseVector;

			nnp = new NeuralNetworkParameters(Xtrain.ColumnCount, 50, (int)ytrain.Max(), this.Lambda);
			NeuralNetwork nn = new NeuralNetwork(nnp);

			int[] predictions;
			nn.Train(this.Iterations, Xtrain, ytrain);
			this.FinalCostValue = nn.costFunction();
			this.FinalAccuracy = nn.Predict(Xtest, ytest, out predictions);

			// save nerual network
			String serialization = JsonConvert.SerializeObject(nnp, Formatting.Indented);
			StreamWriter sw = new StreamWriter(networkFile.FullName);
			sw.Write(serialization);
			sw.Close();

			// process finished
			System.Diagnostics.Debug.WriteLine("------------------------");
			System.Diagnostics.Debug.WriteLine("training result J = {0,5:f}	accuracy = {1,8:p}", FinalCostValue, FinalAccuracy);
			
		}

		public DenseMatrix GenerateTrainingFeatures(BackgroundWorker worker)
		{
			DelimitedWriter matrixWriter = new DelimitedWriter(",");
			DenseMatrix dataMat;

			List<AudioFileFeatures> trainingAudiosList = GetTrainingFilesList();
			dataMat = ProcessFiles(trainingAudiosList, worker);
			matrixWriter.WriteMatrix(dataMat, trainingFile.FullName);	// save the features matrix in a csv file
			
			return dataMat;
		}

		



		/// <summary>
		/// Reads the {srcDir}/labels.js and every .wav file in the same directory
		/// assigns the label property according to the associations in the labels.js hash
		/// </summary>
		/// <param name="srcDir"></param>
		/// <param name="labels"></param>
		/// <returns></returns>
		private List<AudioFileFeatures> GetTrainingFilesList()
		{
			DirectoryInfo srcDir = new DirectoryInfo(DataDirectory);
			Labels labels = LoadLabels();

			FileInfo[] files = srcDir.GetFiles("*.wav");
			List<AudioFileFeatures> AudioInfosList = new List<AudioFileFeatures>();

			// Add every matching file and its label to a list
			foreach (FileInfo file in files)
			{
				for (int i = 0; i < labels.labelsList.Count; i++)
				{
                    if (Regex.IsMatch(file.Name, String.Format(@"{0}.*\.wav", labels.labelsList[i])))
					{
						AudioInfosList.Add(new AudioFileFeatures(file, i+1));
						break;
					}
				}
			}
			return AudioInfosList;
		}

		public Labels LoadLabels()
		{
			Labels lbls = new Labels();
        
			try
			{
				/*StreamReader sr = new StreamReader(this.labelsFile.FullName);
				String json = sr.ReadToEnd();
				sr.Close();
				lbls = JsonConvert.DeserializeObject<Labels>(json);*/
                DataTable recipe;
                String query = "select nombre from etiquetas; ";
                recipe = db.GetDataTable(query);
                foreach (DataRow r in recipe.Rows)
                {
                    lbls.labelsList.Add(r["nombre"].ToString());
                }
			}
			catch (Exception e)
			{
                MessageBox.Show("Error leyendo archivo labels: " + e.Message);
				throw new Exception("Error leyendo archivo labels: " + e.Message);
               
			}
			
			return lbls;
		}

		/// <summary>
		/// Changes the labels assignations, deletes all the training data, and neural network parameters
		/// </summary>
		public void SaveLabels(Labels lbls)
		{
			/*StreamWriter sw = new StreamWriter(this.labelsFile.FullName);
			sw.Write(JsonConvert.SerializeObject(lbls, Formatting.Indented));
			sw.Close();*/
		}
        public void SaveLabels(string label)
        {
        Dictionary<string,string> campos = new Dictionary<string,string>();
            campos.Add("nombre",label);
            db.Insert("etiquetas", campos);  
        }
        public void DeleteLabel(string label)
        {
            db.Delete("etiquetas", "nombre='"+label+"'");
        }










		/// <summary>
		/// Generates the feature vectors for every file passed in the AudioInfosList argument
		/// </summary>
		/// <param name="AudioInfosList"></param>
		/// <param name="worker">Optional for progress reporting</param>
		private DenseMatrix ProcessFiles(List<AudioFileFeatures> AudioInfosList, BackgroundWorker worker = null)
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
				if (worker != null) worker.ReportProgress((count + 1) * 100 / AudioInfosList.Count);
			}

			var query = from file in AudioInfosList
						select file.featureVector.ColumnCount;

			int width = query.First();
			int widthsCount = query.Distinct().Count();
			if (widthsCount > 1) throw new Exception("features vectors are not the same width, aborting...");

			// generate labels:features matrix
			DenseMatrix allFeatures = DenseMatrix.Create(AudioInfosList.Count, width, (i, j) => 0);
			for (int i = 0; i < AudioInfosList.Count; i++)
			{
				DenseMatrix features = AudioInfosList[i].featureVector;
				allFeatures.SetSubMatrix(i, 1, 0, features.ColumnCount, features);
			}

			// TODO: normalize columns 2,n in allFeatures (column 1 contains labels)
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
			String wavFileName	= Path.GetFileNameWithoutExtension(audioInfo.fileInfo.Name);
			String wavFilePath	= audioInfo.fileInfo.FullName.Replace("\\", "/");
			String filePitch	= String.Format("{0}_vamp_vamp-aubio_aubiopitch_frequency.csv", wavFileName);
			String fileMfcc		= String.Format("{0}_vamp_qm-vamp-plugins_qm-mfcc_coefficients.csv", wavFileName);

			filePitch	= Path.Combine(tempDir, filePitch);
			fileMfcc	= Path.Combine(tempDir, fileMfcc);

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
			audioInfo.mfcc	= matrixReader.ReadMatrix(fileMfcc);
			audioInfo.pitch	= matrixReader.ReadMatrix(filePitch);

			// remove last 9 rows of data, for some reason SonicAnnotator plugin adds 9 rows full of zeros
			audioInfo.mfcc = audioInfo.mfcc.SubMatrix(0, audioInfo.mfcc.RowCount - 9, 0, audioInfo.mfcc.ColumnCount) as DenseMatrix;

			// delete generated files
			foreach(String file in new String[] {filePitch, fileMfcc} ) 
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
			nColumns	= Math.Min(values.ColumnCount, nColumns + 1);

			if (bStats)
			{
				for (int i = 1; i < nColumns; i++)
				{
					Vector column = values.Column(i) as Vector;
					if (result == null) {
						result = GetStatistics(column);
					} else {
						result = result.Append(GetStatistics(column)) as DenseMatrix;
					}
				}
			}

			if (bData)
			{
				double[] dValues = values
						.SubMatrix(0, Math.Min(values.RowCount, limit), 1, nColumns-1)
						.ToColumnWiseArray();
				
				DenseMatrix dmValues = new DenseMatrix(1, dValues.Length, dValues);

				if (result == null) {
					result = dmValues;
				} else {
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
			double media	= data.Mean();
			double varianza = data.Variance();
			double max		= data.Maximum();
			double min		= data.Minimum();
			
			return new DenseMatrix(1, 4, new double[] { media, varianza, max, min });
		}
	}
}
