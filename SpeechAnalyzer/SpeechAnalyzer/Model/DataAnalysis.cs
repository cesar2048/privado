using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.IO;
using MathNet.Numerics.LinearAlgebra.IO;
using Newtonsoft.Json;
using DotNumerics.Optimization;

namespace SpeechAnalyzer.Model
{
	class DataAnalysis
	{
		private String DataDirectory;
		private String TempDirectory;
        private SQLiteDatabase db;
		private FeaturesGenerator featuresGenerator;
		private NeuralNetworkParameters nnp;
		private DenseMatrix dataMat;
		private String feature;

		// properties
		public Action<String> ConsoleFunction { get; set; }
		public FileInfo trainingFile { get; set; }
		public FileInfo networkFile { get; set; }

		// input properties
		public Double Lambda { get; set; }
		public Int32 Attempts { get; set; }
		public Int32 Iterations { get; set; }

		// output properties
		public Double FinalCostValue { get; set; }
		public Double FinalAccuracy { get; set; }
		public List<double> CostTest { get; set;  }
		public List<double> CostTrain { get; set; }
		public List<double> ErrorTest { get; set; }
		public List<double> ErrorTrain { get; set; }
		public List<double> LambdaValues { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dataDir"></param>
		/// <param name="tempDir"></param>
		/// <param name="SonicAnotatorPath"></param>
		public DataAnalysis(String dataDir, String tempDir, String SonicAnotatorPath, Action<String> ConsoleFunction = null) 
		{
			this.Lambda			= 0.1;
			this.FinalCostValue = Double.PositiveInfinity;
			this.DataDirectory	= dataDir;
			this.TempDirectory	= tempDir;
			this.CostTest		= new List<double>();
			this.CostTrain		= new List<double>();
			this.ErrorTest		= new List<double>();
			this.ErrorTrain		= new List<double>();
			this.LambdaValues = new List<double>(new double[] { 0, 0.0001, 0.003, 0.001, 0.003, 0.01, 0.03, 0.1, 0.3, 1, 3, 10 } );
			this.ConsoleFunction = ConsoleFunction;
			this.UpdateProblemName("");

			this.db					= new SQLiteDatabase();
			this.featuresGenerator = new FeaturesGenerator(dataDir, tempDir, SonicAnotatorPath, ConsoleFunction);
		}

		private void Log(String msg)
		{
			if (this.ConsoleFunction != null)
			{
				this.ConsoleFunction(msg);
			}
		}


		/// <summary>
		/// Step 1, generate all the features for the training files
		/// </summary>
		/// <param name="progressCallback"></param>
		/// <returns></returns>
		public DenseMatrix GenerateTrainingFeatures(Action<int> progressCallback = null)
		{
			List<AudioFileFeatures> trainingAudiosList = GetTrainingFilesList();
			DenseMatrix dataMat = this.featuresGenerator.ProcessFiles(trainingAudiosList, progressCallback);

			DelimitedWriter matrixWriter = new DelimitedWriter(",");
			matrixWriter.WriteMatrix(dataMat, trainingFile.FullName);	// save the features matrix in a csv file

			this.dataMat = dataMat;
			return dataMat;
		}

		/// <summary>
		/// Step 2, Train the neural network
		/// </summary>
		/// <exception cref="System.Exception">The features haven't been generated</exception>
		public void TrainNeuralNetwork(Action<int> progressCallback = null, bool useLambdaSet = false)
		{
			// load features
			LoadData();
			if (this.dataMat == null) {
				throw new Exception("No features exists for training");
			}

			DenseMatrix mat1, mat2;
			FeaturesGenerator.SplitDataRandomly(dataMat, dataMat.Column(0) as DenseVector, 0.8, out mat1, out mat2);

			//
			// execute machine learning process
			//
			DenseMatrix Xtrain = mat1.SubMatrix(0, mat1.RowCount, 1, mat1.ColumnCount - 1) as DenseMatrix;
			DenseVector ytrain = mat1.Column(0) as DenseVector;

			DenseMatrix Xtest = mat2.SubMatrix(0, mat2.RowCount, 1, mat2.ColumnCount - 1) as DenseMatrix;
			DenseVector ytest = mat2.Column(0) as DenseVector;

			nnp = new NeuralNetworkParameters(Xtrain.ColumnCount, 50, (int)ytrain.Max(), this.Lambda);
			NeuralNetwork nn = new NeuralNetwork(nnp, this.ConsoleFunction);
			
			int[] predictions;
			List<double> lambdas = new List<double>(this.LambdaValues);
			if (!useLambdaSet)
			{
				lambdas.Clear();
				lambdas.Add(this.Lambda);
			}

			CostTrain.Clear();
			CostTest.Clear();
			ErrorTrain.Clear();
			ErrorTest.Clear();

			for (int i = 0; i < lambdas.Count; i++)
			{
				Log("");
				Log("Training with λ = " + lambdas[i]);

				nn.setLambda(lambdas[i]);
				nn.Train(this.Iterations, Xtrain, ytrain);
				
				CostTrain.Add(nn.costFunction(Xtrain, ytrain, false));
				CostTest.Add(nn.costFunction(Xtest, ytest, false));

				ErrorTrain.Add(1 - nn.Predict(Xtrain, ytrain, out predictions) );
				ErrorTest.Add(1-nn.Predict(Xtest, ytest, out predictions));

				if (progressCallback != null) progressCallback((i + 1) * 100 / lambdas.Count);
				Log("Avg cost exec time = " + nn.AvgCostExecTime);
				Log("Avg grad exec time = " + nn.AvgGradExecTime);
				Log(String.Format("Train error {0,5:p}", ErrorTrain.Last() ));
				Log(String.Format("Test  error {0,5:p}", ErrorTest.Last() ));
			}

			this.FinalAccuracy = 1 - ErrorTest.Last();
			this.FinalCostValue = CostTest.Last();
			
			// save nerual network
			NeuralNetworkParameters.Save(networkFile.FullName, nnp);

			// process finished
			Log("------------ Results");
			Log("lambda,cost train,error train,cost test, error test");
			for (int i = 0; i < lambdas.Count; i++)
			{
				Log(String.Format("{0},{1},{2},{3},{4}", lambdas[i], CostTrain[i], ErrorTrain[i], CostTest[i], ErrorTest[i]));
			}
			
			System.Diagnostics.Debug.WriteLine("training result J = {0,5:f}	accuracy = {1,8:p}", FinalCostValue, FinalAccuracy);
		}

		/// <summary>
		/// Step 3, Executes the neural network on a wav file
		/// </summary>
		/// <param name="wavFilePath">The input wav file</param>
		/// <returns>The label predicted by the neural network</returns>
		/// <exception cref="System.IO.FileNotFoundException">If the wavFilePath doesn't exists</exception>
		/// <exception cref="System.Exception">The neural network hasn't been trained</exception>
		public String TestNeuralNetwork(String wavFilePath)
		{
			FileInfo wavFile = new FileInfo(wavFilePath);
			if (!wavFile.Exists) throw new FileNotFoundException("Test file doesn't exists");
			
			LoadData();
			if (nnp == null) throw new Exception("Neural network parameters are not loaded");
			NeuralNetwork nn = new NeuralNetwork(nnp);

			// generate features for the testing file
			List<AudioFileFeatures> trainingAudiosList = new List<AudioFileFeatures>();
			trainingAudiosList.Add(new AudioFileFeatures(wavFile, 0));

			DenseMatrix dataMat = this.featuresGenerator.ProcessFiles(trainingAudiosList);
			DenseMatrix X = dataMat.SubMatrix(0, dataMat.RowCount, 1, dataMat.ColumnCount - 1) as DenseMatrix;
			DenseVector y = dataMat.Column(0) as DenseVector;

			// execute neural network
			int[] predictions;
			DenseMatrix outLayerValues;
			this.FinalAccuracy = nn.Predict(X, y, out predictions, out outLayerValues);

			// get the label
			var labels		= LoadLabelFeatures(this.feature);
			String label	= "error";
			if (predictions[0] >= 0 && predictions[0] <= labels.Count)
			{
				label		= labels[predictions[0] - 1];
			}

			// log the probabilities
			for (int i = 0; i < labels.Count; i++)
			{
				Log(String.Format("{0,6:F4} -> {1}", outLayerValues[0, i], labels[i]));
			}

			return label;
		}
		



		/// <summary>
		/// Returns a list of all the files in the DataDirectory whose file names match
		/// one of the labels as a prefix
		/// </summary>
		/// <returns></returns>
		private List<AudioFileFeatures> GetTrainingFilesList()
		{
			DirectoryInfo srcDir = new DirectoryInfo(DataDirectory);
			List<String> labels = LoadLabelFeatures(feature);
			FileInfo[] files = srcDir.GetFiles("*.wav");
			List<AudioFileFeatures> AudioInfosList = new List<AudioFileFeatures>();

			// Add every matching file and its label to a list
			foreach (FileInfo file in files)
			{
				for (int i = 0; i < labels.Count; i++)
				{
                    if (Regex.IsMatch(file.Name, String.Format(@"{0}.*\.wav", labels[i])))
					{
						AudioInfosList.Add(new AudioFileFeatures(file, i+1));
						break;
					}
				}
			}
			return AudioInfosList;
		}

		public List<String> LoadFeatures()
		{
			List<String> listF = new List<String>();

			try
			{
				DataTable recipe;
				String query = "select nombre from funciones; ";
				recipe = db.GetDataTable(query);
				foreach (DataRow r in recipe.Rows)
				{
					listF.Add(r["nombre"].ToString());
				}
			}
			catch (Exception e)
			{
				MessageBox.Show("Error leyendo archivo labels: " + e.Message);
				throw new Exception("Error leyendo archivo labels: " + e.Message);

			}

			return listF;
		}
		public List<String> LoadLabelFeatures(string feature)
		{
			List<String> listF = new List<String>();

			try
			{
				DataTable recipe;
				String query = "select etiquetanombre from etiquetasfunciones where funcionnombre='"+feature+"'; ";
				recipe = db.GetDataTable(query);
				foreach (DataRow r in recipe.Rows)
				{
					listF.Add(r["etiquetanombre"].ToString());
				}
			}
			catch (Exception e)
			{
				MessageBox.Show("Error leyendo archivo labels: " + e.Message);
				throw new Exception("Error leyendo archivo labels: " + e.Message);

			}

			return listF;
		}

		/// <summary>
		/// Changes the current problem
		/// </summary>
		/// <param name="problem"></param>
		public void UpdateProblemName(String problem)
		{
			if (String.Equals(this.feature, problem))
			{	// do nothing in case we are changing to the same name
				return;
			}

			this.feature = problem;
			problem += "-";
			
			this.trainingFile = new FileInfo(Path.Combine(TempDirectory, problem + "training-features.csv"));
			this.networkFile = new FileInfo(Path.Combine(TempDirectory, problem + "networkParams.js"));

			this.nnp = null;
			this.dataMat = null;
		}

		/// <summary>
		/// Load the neural network model and training data for the current problem
		/// </summary>
		private void LoadData()
		{
			// Load previously generated parameters if these are not already loaded
			if (this.networkFile.Exists && this.nnp == null)
			{
				this.nnp = NeuralNetworkParameters.Load(networkFile.FullName);
			}

			// load previously generated features if the file exists
			if (this.trainingFile.Exists && this.dataMat == null)
			{
				DelimitedReader<DenseMatrix> matrixReader = new DelimitedReader<DenseMatrix>(",");
				this.dataMat = matrixReader.ReadMatrix(trainingFile.FullName);	// load the features matrix from csv file
			}
		}











        public void SaveLabels(string label)
        {
			Dictionary<string,string> campos = new Dictionary<string,string>();
            campos.Add("nombre",label);
            db.Insert("etiquetas", campos);  
        }

		public void SaveFeature(string feature)
		{
			Dictionary<string, string> campos = new Dictionary<string, string>();
			campos.Add("nombre", feature);
			db.Insert("funciones", campos);  
		}

		public void SaveFeatureLabel(string feature,string label)
		{
			Dictionary<string, string> campos = new Dictionary<string, string>();
			campos.Add("funcionnombre", feature);
			campos.Add("etiquetanombre", label);
			db.Insert("etiquetasfunciones", campos);
		}

        public void DeleteLabel(string label)
        {
            db.Delete("etiquetas", "nombre='"+label+"'");
        }

		public void DeleteFeature(string feature)
		{
			db.Delete("funciones", "nombre='" + feature + "'");
		}

		public void DeleteFeatureLabel(string feature, string label)
		{
			db.Delete("etiquetasfunciones", "funcionnombre='" + feature + "' AND " + "etiquetanombre='"+label+"'");
		}


	}
}
