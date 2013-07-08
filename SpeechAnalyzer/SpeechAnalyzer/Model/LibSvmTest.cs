using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVM;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.IO;
using MathNet.Numerics.LinearAlgebra.IO;

namespace SpeechAnalyzer.Model
{
	class LibSvmTest
	{
		public static void Test()
		{
			// First, read in the training and test data
			Problem train = Problem.Read(@"temp\dna.scale.tr");
			Problem test = Problem.Read(@"temp\dna.scale.t");

			// For this example (and indeed, many scenarios), the default 
			// parameters will suffice.
			Parameter parameters = new Parameter();

			double C;
			double Gamma;
			// This will do a grid optimization to find the best parameters
			// and store them in C and Gamma, outputting the entire
			// search to params.txt.
			System.Diagnostics.Debug.WriteLine("ParameterSelection.Grid");
			ParameterSelection.Grid(
					train,
					parameters,
					@"temp\params.txt",
					out C,
					out Gamma
			);
			System.Diagnostics.Debug.WriteLine("ParameterSelection.Grid Finished");
			parameters.C = C; 
			parameters.Gamma = Gamma;

			// Train the model using the optimal parameters.
			System.Diagnostics.Debug.WriteLine("ParameterSelection.Grid Train");
			SVM.Model model = Training.Train(train, parameters);
			System.Diagnostics.Debug.WriteLine("ParameterSelection.Grid Train Finished");
			// Perform classification on the test data, putting the
			// results in results.txt.
			Prediction.Predict(test, @"temp\results.txt", model, false);
		}

		public static double TestOnData()
		{
			DelimitedReader<DenseMatrix> matrixReader = new DelimitedReader<DenseMatrix>(",");
			DelimitedWriter matrixWriter = new DelimitedWriter(",");

			// read and normalize data
			DenseMatrix dataMat = matrixReader.ReadMatrix(@"temp\training-features.csv");	// load the features matrix from csv file
			var dataNorm = NeuralNetwork.normalizeFeatures(dataMat.GetSubMatrix(0, 0, 1, 0) as DenseMatrix, null);
			dataMat.SetSubMatrix(0, dataMat.RowCount, 1, dataMat.ColumnCount - 1, dataNorm.Item1);
			
			// split into training/testing and save data
			DenseMatrix mat1, mat2;
			NeuralNetwork.SplitDataRandomly(dataMat, 0.8, out mat1, out mat2);
			matrixWriter.WriteMatrix(mat1, @"temp\svm-training-features.csv");
			matrixWriter.WriteMatrix(mat2, @"temp\svm-testing--features.csv");

			// generate problem info
			Problem train = MatricesToProblem(mat1);
			Problem test = MatricesToProblem(mat2);

			// For this example (and indeed, many scenarios), the default 
			// parameters will suffice.
			Parameter parameters = new Parameter();

			double C;
			double Gamma;
			// This will do a grid optimization to find the best parameters
			// and store them in C and Gamma, outputting the entire
			// search to params.txt.
			System.Diagnostics.Debug.WriteLine("ParameterSelection.Grid");
			ParameterSelection.Grid(
					train,
					parameters,
					@"temp\params.txt",
					out C,
					out Gamma
			);
			System.Diagnostics.Debug.WriteLine("ParameterSelection.Grid Finished");
			parameters.C = C;
			parameters.Gamma = Gamma;

			// Train the model using the optimal parameters.
			System.Diagnostics.Debug.WriteLine("ParameterSelection.Grid Train");
			SVM.Model model = Training.Train(train, parameters);
			System.Diagnostics.Debug.WriteLine("ParameterSelection.Grid Train Finished");
			// Perform classification on the test data, putting the
			// results in results.txt.

			
			double accuracy = Prediction.Predict(test, @"temp\results.txt", model, false);
			return accuracy;
		}

		private static Problem MatricesToProblem(DenseMatrix data)
		{
			DenseMatrix X = data.SubMatrix(0, data.RowCount, 1, data.ColumnCount - 1) as DenseMatrix;
			DenseVector y = data.Column(0) as DenseVector;

			// convertir los datos a svm Problem
			Node[][] NodeX = new Node[X.RowCount][];
			for (int i = 0; i < X.RowCount; i++)
			{
				NodeX[i] = new Node[X.ColumnCount];
				for (int j = 0; j < X.ColumnCount; j++)
				{
					NodeX[i][j] = new Node(j, X[i, j]);
				}
			}

			return new Problem(X.RowCount, y.ToArray(), NodeX, X.ColumnCount);
		}



	}
}
