using System;
using static System.Console;
public class lsfit{
	
	// We want c and f to be accessible outside of the class. Both c and f are needed in the eval
	// function also
	public vector c;
	public Func<double, double>[] f;
	public matrix sigma;
	
	public lsfit(vector x, vector y, vector dy, Func<double, double>[] fs){
		int n = x.size;
		int m = fs.Length;
		
		// We move the functions into the variable f
		f=fs;
		
		matrix A = new matrix(n, m);
		vector b = new vector(n);

		for(int i=0; i<n; i++){
			b[i] = y[i]/dy[i];
			for(int k=0; k<m; k++){
				A[i,k] = f[k](x[i])/dy[i];
			}
		}
		
		// Solve the system of equations
		var qrGS = new qrDecompositionGS(A);
		c = qrGS.solve(b);

		// At last we calculate the covariance matrix, A^T * A
		matrix Ainv = qrGS.inverse();
		sigma = Ainv*Ainv.T;
		WriteLine("Sigma done");
	}

	public double eval(double z){
		// Evaluate the found linear combination of functions at the point z
		double sum = 0;
		for(int i=0; i<c.size; i++){
			sum += c[i]*f[i](z);
		}
		return sum;
	}
}

