using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;
using System.IO;
using Newtonsoft.Json;

namespace SpeechAnalyzer.Model
{
	class NeuralNetworkParameters
	{
		// neural network parameters
		public Int32 nOutput { get; set; }
		public Int32 nHidden { get; set; }
		public Int32 nInput { get; set; }
		public Double lambda { get; set; }
		public Double[] theta { get; set; }
		public Double [] normalization { get; set; }

		/// <summary>
		/// Used for serialization, do not use directly
		/// </summary>
		public NeuralNetworkParameters() { }

		public NeuralNetworkParameters(int input, int hidden, int output, double lambda)
		{
			this.nInput = input;
			this.nHidden = hidden;
			this.nOutput = output;
			this.lambda = lambda;

			int length = (hidden * (input+1)) + (output * (hidden+1));
			theta = new double[length];
		}

		public void SetNormalization(DenseMatrix normalization)
		{
			if (normalization.RowCount != 2) throw new ArgumentException("wrong rows count, should be 2");
			if (normalization.ColumnCount != this.nInput) throw new ArgumentException("wrong columns count, should be " + this.nInput);

			this.normalization = normalization.ToColumnWiseArray();
		}

		public DenseMatrix GetNormalization()
		{
			return new DenseMatrix(2, this.nInput, this.normalization);
		}


		public static NeuralNetworkParameters Load(String file)
		{
			NeuralNetworkParameters nnp = JsonEncoder.Load<NeuralNetworkParameters>(file);
			return nnp;
		}

		public static void Save(String file, NeuralNetworkParameters nnp)
		{
			JsonEncoder.Save(file, nnp);
		}
	}
}
