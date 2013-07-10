using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;
using DotNumerics.Optimization;

namespace SpeechAnalyzer.Model
{
	class NeuralNetwork
	{
		// operation
        private DenseMatrix Theta1, Theta2, X;
        private DenseVector y;
		private int m;
		private NeuralNetworkParameters nnp;

		public NeuralNetwork(NeuralNetworkParameters parameters)
		{
			this.m			= 0;
			this.nnp		= parameters;
			this.Theta1		= DenseMatrix.Create(nnp.nHidden, nnp.nInput + 1, (i, j) => 0);
			this.Theta2		= DenseMatrix.Create(nnp.nOutput, nnp.nHidden + 1, (i, j) => 0);

			if (parameters.theta != null)
			{
				setTheta(parameters.theta);
			}
		}

		public NeuralNetworkParameters getParameters()
		{
			return this.nnp;
		}

		public void setTheta(double[] theta)
		{
			int thetaLength = (nnp.nHidden * (nnp.nInput+1)) + (nnp.nOutput * (nnp.nHidden+1));
			if (theta == null) throw new ArgumentException("theta can't be null");
			if ( theta.Length != thetaLength ) throw new ArgumentException("Theta length does not match layer sizes");
			
			double[] t1 = theta.Take(Theta1.RowCount * Theta1.ColumnCount).ToArray();
			double[] t2 = theta.Skip(Theta1.RowCount * Theta1.ColumnCount).ToArray();

			this.nnp.theta = theta;
			this.Theta1 = new DenseMatrix(Theta1.RowCount, Theta1.ColumnCount, t1);
			this.Theta2 = new DenseMatrix(Theta2.RowCount, Theta2.ColumnCount, t2);
		}

		public double[] getTheta()
		{
			double[] t1 = Theta1.ToColumnWiseArray();
			double[] t2 = Theta2.ToColumnWiseArray();

			return t1.Concat(t2).ToArray();
		}

		public void RandInitializeTheta()
		{
			double epsilon = 0.12;
			Random rnd = new Random();

			this.Theta1.MapInplace(a => rnd.NextDouble() * 2 * epsilon - epsilon);
			this.Theta2.MapInplace(a => rnd.NextDouble() * 2 * epsilon - epsilon);
		}

		public double costFunction()
		{
			double[] theta = getTheta();
			return this.costFunction(theta);
		}



		private double costFunction(double[] theta)
		{
			DenseMatrix ybinary, aux1, aux2, aux, a2, a3, z2, z3;
			double J = 0;

			setTheta(theta);
			feedForward(out a2, out a3, out z2, out z3, out ybinary);

			// calculate cost
			aux1 = DenseMatrix.OfMatrix(a3);
			aux2 = DenseMatrix.OfMatrix(a3);

			aux1.MapIndexedInplace((i, j, a) => (ybinary[i, j] == 1 ) ? Math.Log(a)     : 0);	// ybinary * log(a3)
			aux2.MapIndexedInplace((i, j, a) => (ybinary[i, j] == 0 ) ? Math.Log(1 - a) : 0);	// (1-ybinary) .* log(1-a3)
			aux = aux1.Add(aux2) as DenseMatrix;

			J = aux.SumVertically().Sum();
			J = -J / m;

			// add regularization
			aux1 = Theta1.SubMatrix(0, Theta1.RowCount, 1, Theta1.ColumnCount - 1) as DenseMatrix;
			aux1.MapInplace(a => Math.Pow(a, 2));

			aux2 = Theta2.SubMatrix(0, Theta2.RowCount, 1, Theta2.ColumnCount - 1) as DenseMatrix;
			aux2.MapInplace(a => Math.Pow(a, 2));

			J += nnp.lambda * (aux1.SumHorizontally().Sum() + aux2.SumHorizontally().Sum()) / 2 * m;
			return J;
		}


		private double[] gradFunction(double[] theta)
		{
			DenseMatrix Delta1, Delta2, del2, del3, z2, z3, a2, a3, ybinary, Theta2Aux, Theta1grad, Theta2grad;

			setTheta(theta);
			feedForward(out a2, out a3, out z2, out z3, out ybinary);

			Delta1 = DenseMatrix.OfMatrix(Theta1).Multiply(0) as DenseMatrix;// zeros, same size
			Delta2 = DenseMatrix.OfMatrix(Theta2).Multiply(0) as DenseMatrix;// zeros, same size

			Theta2Aux = Theta2.GetSubMatrix(0, 0, 1, -1) as DenseMatrix;

			for (int t = 0; t < m; t++)
			{
				del3 = a3.Row(t).Subtract(ybinary.Row(t)).ToColumnMatrix() as DenseMatrix;
				
				del2 = Theta2Aux.TransposeThisAndMultiply(del3) as DenseMatrix;
				DenseMatrix sig = z2.Row(t).ToColumnMatrix().sigmoidGradient() as DenseMatrix;
				del2 = del2.PointwiseMultiply(sig) as DenseMatrix;

				Delta2 = Delta2.Add(del3.Multiply(a2.Row(t).ToRowMatrix())) as DenseMatrix;
				Delta1 = Delta1.Add(del2.Multiply(X.Row(t).ToRowMatrix())) as DenseMatrix;
			}

			Theta1grad = Delta1.Divide(m) as DenseMatrix;
			Theta2grad = Delta2.Divide(m) as DenseMatrix;
			
			Theta1grad.MapIndexedInplace((i, j, z) => (j == 0) ? z : z + nnp.lambda * Theta1[i, j]);	// regularization ## Del(i,j) += λ * Θ(i,j)(l)   if j != 0
			Theta2grad.MapIndexedInplace((i, j, z) => (j == 0) ? z : z + nnp.lambda * Theta2[i, j]);	// regularization ## Del(i,j) += λ * Θ(i,j)(l)   if j != 0

			// unroll gradient
			double[] t1 = Theta1grad.ToColumnWiseArray();
			double[] t2 = Theta2grad.ToColumnWiseArray();
			return t1.Concat(t2).ToArray();
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
			a2 = z2.Sigmoid() as DenseMatrix;

			// a2 -> a3
			a2 = a2.InsertColumn(0, DenseVector.Create(m, i => 1)) as DenseMatrix;

			z3 = a2.TransposeAndMultiply(Theta2) as DenseMatrix;
			a3 = z3.Sigmoid() as DenseMatrix;

			// auxiliary
			ybinary = new DenseMatrix(m, nnp.nOutput);
			ybinary.MapIndexedInplace((i, j, a) => (y[i] == j + 1) ? 1 : 0);
		}






		
		public void Train(int funEvaluations, DenseMatrix X, DenseVector y)
		{
			SetInputData(X, y, null);

			// execute minimization algorithm
			L_BFGS_B LBFGSB = new L_BFGS_B();
			LBFGSB.MaxFunEvaluations = funEvaluations;

			this.RandInitializeTheta();
			nnp.theta = LBFGSB.ComputeMin(this.costFunction, this.gradFunction, this.getTheta() );
			setTheta(nnp.theta);
		}

		public double Predict(DenseMatrix X, DenseVector y, out int[] predictions)
		{
			DenseMatrix z2, z3, a2, a3, ybinary;

			SetInputData(X, y, nnp.GetNormalization() );
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

		private void SetInputData(DenseMatrix X, DenseVector y, DenseMatrix normParams)
		{
			if (X.ColumnCount != this.nnp.nInput)
			{
				throw new Exception("Input data have different features number " + X.ColumnCount);
			}

			// normalize and add ones column
			var normalX = normalizeFeatures(X, normParams);

			// save training data
			this.X = normalX.Item1.InsertColumn(0, DenseVector.Create(X.RowCount, i => 1)) as DenseMatrix;
			this.y = y;
			this.m = X.RowCount;
			this.nnp.SetNormalization(normalX.Item2);
		}





        private DenseMatrix gradDescent(DenseMatrix X,DenseMatrix y,DenseMatrix theta,float alpha ,int num_iters)
        {
            int cont = 1;
			
            while(cont <= num_iters ){
                DenseMatrix xTheta = X.Multiply(theta.Inverse()) as DenseMatrix;
                DenseMatrix mult1= xTheta - y.Inverse() as DenseMatrix;
                DenseMatrix sum = mult1.Multiply(X) as DenseMatrix;
                theta = theta - sum.Multiply(alpha/m) as DenseMatrix;
                cont++;
            }

            return theta;
        }


        public static Tuple<DenseMatrix,DenseMatrix>  normalizeFeatures(DenseMatrix X, DenseMatrix normParameters)
        {
            DenseVector meanN,stdN, temp;
			DenseMatrix XN;

            XN		= DenseMatrix.OfMatrix(X);
			int m	= XN.RowCount;
			int n	= XN.ColumnCount;

			if (normParameters == null)
			{
				meanN = XN.MeanVertically() as DenseVector;
				stdN = XN.StdVertically() as DenseVector;
				
				normParameters = DenseMatrix.Create(2, n, (i, j) => 1);
				normParameters.SetRow(0, meanN);
				normParameters.SetRow(1, stdN);
			}
			else
			{
				meanN = normParameters.Row(0) as DenseVector;
				stdN = normParameters.Row(1) as DenseVector;
			}

			int cont = 0;
			while (cont < m)
			{
				temp = (XN.Row(cont).Subtract(meanN)).PointwiseDivide(stdN) as DenseVector;
				XN.SetRow(cont, temp);
				cont++;
			}
			return new Tuple<DenseMatrix, DenseMatrix>(XN, normParameters);
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
			Random rnd = new Random();
			DenseVector selector = new DenseVector(data.RowCount);
			part1 = new DenseMatrix((int) (data.RowCount * proportion), data.ColumnCount);
			part2 = new DenseMatrix(data.RowCount - part1.RowCount, data.ColumnCount);
			
			int count1 = 0, count2 = 0, classes1 = 0, classes2 = 0, i=0;

			do
			{
				selector.MapInplace(x => (rnd.NextDouble() <= proportion) ? 1 : 0);

				classes1 = selector.PointwiseMultiply(y).Distinct().Count() - 1;						// remove class 0 as it doesn't exists
				classes2 = selector.Multiply(-1).Add(1).PointwiseMultiply(y).Distinct().Count() - 1;	// remove class 0 as it doesn't exists
				i++;

			} while (selector.SumMagnitudes() != part1.RowCount || classes1 != classes2);


			for (i = 0; i < data.RowCount; i++ )
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
	}
}
