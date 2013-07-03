using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Generic;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics;
using MathNet.Numerics.Statistics;

namespace SpeechAnalyzer.Model
{
	/// <summary>
	/// code provided by Gregor959, see link: http://mathnetnumerics.codeplex.com/discussions/357834
	/// </summary>
	public static class MatrixExtensionMethods
	{

		public static Vector<double> MeanHorizontally(this Matrix<double> thisMatrix)
		{
			Vector<double> v = DenseVector.Create(thisMatrix.ColumnCount, i => 1.0);
			Vector<double> sum = thisMatrix * v;
			return sum * (1.0 / (double)thisMatrix.ColumnCount);
		}

		public static Vector<double> MeanVertically(this Matrix<double> thisMatrix)
		{
			Vector<double> w = DenseVector.Create(thisMatrix.RowCount, i => 1.0);
			Vector<double> sum = w * thisMatrix;
			return sum * (1.0 / (double)thisMatrix.RowCount);
		}

		public static Vector<double> SumHorizontally(this Matrix<double> thisMatrix)
		{
			Vector<double> v = DenseVector.Create(thisMatrix.ColumnCount, i => 1.0);
			return thisMatrix * v;
		}

		public static Vector<double> SumVertically(this Matrix<double> thisMatrix)
		{
			Vector<double> w = DenseVector.Create(thisMatrix.RowCount, i => 1.0);
			return w * thisMatrix;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="thisMatrix"></param>
		/// <param name="rowIndex">0 to RowCount</param>
		/// <param name="rowEnd">0 for end, negative for an offset from the end</param>
		/// <param name="colIndex">0 to ColumnCount</param>
		/// <param name="colEnd">0 for end, negative for an offset from the end</param>
		/// <returns></returns>
		public static Matrix<double> GetSubMatrix(this Matrix<double> thisMatrix,
			int rowIndex, int rowEnd, int colIndex, int colEnd)
		{
			if (rowEnd <= 0) rowEnd = thisMatrix.RowCount - rowEnd;
			if (colEnd <= 0) colEnd = thisMatrix.ColumnCount - colEnd;

			int rowCount = Math.Min(rowEnd, thisMatrix.RowCount) - rowIndex;
			int colCount = Math.Min(colEnd, thisMatrix.ColumnCount) - colIndex;

			return thisMatrix.SubMatrix(rowIndex, rowCount, colIndex, colCount);
		}


		public static Vector<double> StdHorizontally(this Matrix<double> thisMatrix)
		{
			var sDeviations = new DenseVector(thisMatrix.RowCount);
			foreach (var index in thisMatrix.RowEnumerator())
			{
				sDeviations[index.Item1] = index.Item2.StandardDeviation();
			}

			return sDeviations;
		}

		public static Vector<double> StdVertically(this Matrix<double> thisMatrix)
		{
			var sDeviations = new DenseVector(thisMatrix.ColumnCount);
			foreach (var index in thisMatrix.ColumnEnumerator())
			{
				sDeviations[index.Item1] = index.Item2.StandardDeviation();
			}

			return sDeviations;
		}

		public static Matrix<double> Sigmoid(this Matrix<double> X)
		{
			DenseMatrix aux = DenseMatrix.OfMatrix(X);
			aux.MapInplace(z => 1.0 / (1.0 + Math.Exp(-z)));
			return aux;
		}

		public static Matrix<double> sigmoidGradient(this Matrix<double> X)
		{
			Matrix<double> aux = X.Sigmoid();
			aux.MapInplace(z => 1 - z);		// = (1-sigmoid(X))

			aux = X.Sigmoid().PointwiseMultiply(aux) as DenseMatrix;	// = sigmoid(X) .* aux
			return aux;
		}

		public static Matrix<double> Log(this Matrix<double> X)
		{
			X.MapInplace(z => Math.Log(z));
			return X;
		}
	}
}
