using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;

namespace SpeechAnalyzer.Model
{
	class NeuralNetwork
	{
        public DenseMatrix Theta1, Theta2, X;
        DenseVector y;
		int nOutput, nHidden, m, n;
		double lambda;

		public NeuralNetwork(DenseMatrix X, DenseVector y, int nOutput, int nHidden, double lambda)
		{
			this.X = X;
			this.y = y;
			this.nOutput = nOutput;
			this.lambda = lambda;
			this.nHidden = nHidden;

			this.n = X.ColumnCount;
			this.m = X.RowCount;
			int nOut = (int)y.Max();

			this.X = X.InsertColumn(0, DenseVector.Create(m, i => 1)) as DenseMatrix;

			this.Theta1 = DenseMatrix.Create(nHidden, n + 1, (i, j) => 0);
			this.Theta2 = DenseMatrix.Create(nOut, nHidden + 1, (i, j) => 0);
		}

		public void reshapeTheta(double[] theta)
		{
			double[] t1 = theta.Take(Theta1.RowCount * Theta1.ColumnCount).ToArray();
			double[] t2 = theta.Skip(Theta1.RowCount * Theta1.ColumnCount).ToArray();

			this.Theta1 = new DenseMatrix(Theta1.RowCount, Theta1.ColumnCount, t1);
			this.Theta2 = new DenseMatrix(Theta2.RowCount, Theta2.ColumnCount, t2);
		}

		public double[] getTheta()
		{
			double[] t1 = Theta1.ToColumnWiseArray();
			double[] t2 = Theta2.ToColumnWiseArray();

			return t1.Concat(t2).ToArray();
		}

		public double costFunction(double[] theta)
		{
			DenseMatrix ybinary, aux1, aux2, aux, a2, a3, z2, z3;
			double J = 0;

			reshapeTheta(theta);
			feedForward(out a2, out a3, out z2, out z3, out ybinary);

			// calculate cost
			aux1 = DenseMatrix.OfMatrix(a3);
			aux2 = DenseMatrix.OfMatrix(a3);

			aux1.MapIndexedInplace((i, j, a) => ybinary[i, j] * Math.Log(a));			// ybinary * log(a3)
			aux2.MapIndexedInplace((i, j, a) => (1 - ybinary[i, j]) * Math.Log(1 - a));	// (1-ybinary) .* log(1-a3)
			aux = aux1.Add(aux2) as DenseMatrix;

			J = aux.SumVertically().Sum();
			J = -J / m;

			// add regularization
			aux1 = Theta1.SubMatrix(0, Theta1.RowCount, 1, Theta1.ColumnCount - 1) as DenseMatrix;
			aux1.MapInplace(a => Math.Pow(a, 2));

			aux2 = Theta2.SubMatrix(0, Theta2.RowCount, 1, Theta2.ColumnCount - 1) as DenseMatrix;
			aux2.MapInplace(a => Math.Pow(a, 2));

			J += lambda * (aux1.SumHorizontally().Sum() + aux2.SumHorizontally().Sum()) / 2 * m;
			return J;
		}

		public double[] gradFunction(double[] theta)
		{
			DenseMatrix Delta1, Delta2, del2, del3, z2, z3, a2, a3, ybinary, Theta1Aux;

			reshapeTheta(theta);
			feedForward(out a2, out a3, out z2, out z3, out ybinary);

			Delta1 = DenseMatrix.OfMatrix(Theta1).Multiply(0) as DenseMatrix;// zeros, same size
			Delta2 = DenseMatrix.OfMatrix(Theta2).Multiply(0) as DenseMatrix;// zeros, same size

			Theta1Aux = Theta2.GetSubMatrix(0, 0, 1, -1) as DenseMatrix;

			for (int t = 0; t < m; t++)
			{
				del3 = a3.Row(t).Subtract(ybinary.Row(t)).ToColumnMatrix() as DenseMatrix;
				del2 = Theta1Aux.TransposeThisAndMultiply(del3).
							PointwiseMultiply(
								sigmoidGradient(z2.Row(t).ToColumnMatrix() as DenseMatrix)
							) as DenseMatrix;

				Delta2 = Delta2.Add(del3.Multiply(a2.Row(t).ToRowMatrix())) as DenseMatrix;
				Delta1 = Delta1.Add(del2.Multiply(X.Row(t).ToRowMatrix())) as DenseMatrix;
			}

			Theta1 = Delta1.Divide(m) as DenseMatrix;
			Theta2 = Delta2.Divide(m) as DenseMatrix;

			// unroll gradient
			double[] t1 = Theta1.ToColumnWiseArray();
			double[] t2 = Theta2.ToColumnWiseArray();
			return t1.Concat(t2).ToArray();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="theta"></param>
		/// <param name="predictions"></param>
		/// <returns>Accuracy value</returns>
		public double Predict(double[] theta, out int[] predictions)
		{
			DenseMatrix z2, z3, a2, a3, ybinary;

			reshapeTheta(theta);
			feedForward(out a2, out a3, out z2, out z3, out ybinary);

			var query = from row in a3.RowEnumerator()
						select row.Item2.AbsoluteMaximumIndex() + 1;

			predictions = query.ToArray();

			double acc = 0;
			for (int i = 0; i < m; i++)
			{
				acc += (predictions[i] == (int)y[i]) ? 1 : 0;
			}
			acc /= m;
			return acc;
		}

		private void feedForward(
			out DenseMatrix a2, out DenseMatrix a3,
			out DenseMatrix z2, out DenseMatrix z3,
			out DenseMatrix ybinary
			)
		{
			DenseMatrix a1 = DenseMatrix.OfMatrix(X) as DenseMatrix;

			// a1 -> a2
			a1 = DenseMatrix.OfMatrix(X);
			//a1.InsertColumn(0, DenseVector.Create(m, i => 1)) as DenseMatrix;

			z2 = a1.TransposeAndMultiply(Theta1) as DenseMatrix;
			a2 = sigmoid(z2);

			// a2 -> a3
			a2 = z2.InsertColumn(0, DenseVector.Create(m, i => 1)) as DenseMatrix;

			z3 = a2.TransposeAndMultiply(Theta2) as DenseMatrix;
			a3 = sigmoid(z3);

			// auxiliary
			ybinary = new DenseMatrix(m, nOutput);
			ybinary.MapIndexedInplace((i, j, a) => (y[i] == j + 1) ? 1 : 0);
		}






		public void RandInitializeTheta()
		{
			double epsilon = 0.5;
			Random rnd = new Random();

			this.Theta1.MapInplace(a => rnd.NextDouble() * 2 * epsilon - epsilon);
			this.Theta2.MapInplace(a => rnd.NextDouble() * 2 * epsilon - epsilon);
		}

		private DenseMatrix sigmoid(DenseMatrix X)
		{
			X.MapInplace(z => 1.0 / (1.0 + Math.Exp(-z)));
			return X;
		}

		private DenseMatrix log(DenseMatrix X)
		{
			X.MapInplace(z => Math.Log(z));
			return X;
		}

		private DenseMatrix sigmoidGradient(DenseMatrix X)
		{
			DenseMatrix aux = sigmoid(X);
			aux.MapInplace(z => 1 - z);		// = (1-sigmoid(X))

			X = sigmoid(X).PointwiseMultiply(aux) as DenseMatrix;	// = sigmoid(X) .* aux
			return X;
		}
        public Double[] gradDescent(DenseMatrix X,DenseVector y,DenseVector theta,double alpha ,int num_iters)
        {
            int cont = 1;
            DenseMatrix thetaAux = theta.ToColumnMatrix() as DenseMatrix;
            DenseMatrix yAux = y.ToColumnMatrix() as DenseMatrix;
            while(cont <= num_iters ){
                DenseMatrix xTheta = (thetaAux.Transpose()).Multiply(X.Transpose()) as DenseMatrix;
                DenseMatrix mult1= xTheta - yAux.Transpose() as DenseMatrix;
                DenseMatrix sum = mult1.Multiply(X) as DenseMatrix;
                thetaAux = thetaAux - sum.Multiply(alpha / m) as DenseMatrix;
                cont++;
            }
            Double[] thetaD = thetaAux.ToColumnWiseArray();
            return thetaD;
        }
        public static Tuple<DenseMatrix,DenseMatrix>  normalizeFeatures(DenseMatrix X)
        {
            DenseVector meanN,stdN;
            DenseMatrix XN = X;
            meanN = X.MeanVertically() as DenseVector;
            stdN = X.StdVertically() as DenseVector;
            int m = X.RowCount;
            int n = X.ColumnCount;
            int n2 = stdN.Count;
            DenseMatrix parametersl = DenseMatrix.Create(2, n, (i, j) => 1); 
            parametersl.SetRow(0,meanN);
            parametersl.SetRow(1,stdN);
            DenseVector temp;
            int cont = 0;
            while (cont < m)
            {
                temp = (X.Row(cont).Subtract(meanN)).PointwiseDivide(stdN) as DenseVector;
                XN.SetRow(cont, temp);
                cont++;
            }
            return new Tuple<DenseMatrix,DenseMatrix> (XN,parametersl);
        }
	}
}
