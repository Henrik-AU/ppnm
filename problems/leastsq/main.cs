using System;
using System.IO;
using static System.Math;

class main{
	static void Main(){
		// Arrays with the measured data points
		vector x = new vector(new double[] {1, 2, 3, 4, 6, 9, 10, 13, 15});
		vector y = new vector (new double[] {117, 100, 88, 72, 53, 29.5, 25.2, 15.2, 11.1});
		// The uncertainty of the data points is 5% of the values
		vector dy = y/20;

		int n = x.size;
		StreamWriter writeData = new StreamWriter("originalData.txt");
		for(int i=0; i<x.size; i++){
			writeData.WriteLine("{0}\t{1}\t{2}", x[i], y[i], dy[i]);
		}
		writeData.Close();
		
		vector yln = new vector(n);
		vector dyln = new vector(n);
		// Take the logarithm of the data
		for(int i=0; i<n; i++){
			yln[i] = Log(y[i]);
			dyln[i] = dy[i]/y[i];
		}

		// Fit the data
		// We fit with a linear equation with a constant
		var f = new Func<double, double>[] {t=>1, t=>t};
		var fit = new lsfit(x, yln, dyln, f);
		
		// The lambda coefficient is now stored in the c[1] entry of the c-vector
		// The uncertainty of lambda can be found from the covariance matrix
		double lambda = fit.c[1];
		double dlambda = Sqrt(fit.sigma[1,1]);
		double lna = fit.c[0];
		double dlna = Sqrt(fit.sigma[0,0]);

		// The half-life is then
		double T = -Log(2.0)/lambda;

		// Write out the found half-life and lambda+-dlambda
		StreamWriter writeFitResult = new StreamWriter("fit.txt");
		writeFitResult.WriteLine("The least squares fit gave the results");
		writeFitResult.WriteLine("lambda = {0:f5} +/- {1:f5}", lambda, dlambda);
		writeFitResult.WriteLine("ln(a) = {0:f5} +/- {1:f5}", lna, dlna);
		writeFitResult.WriteLine("The half-life is thus estimated to be {0:f2} days", T);
		writeFitResult.WriteLine("The table value half-life is 3.66 days");
		writeFitResult.Close();


		// Generate data for the fitted curve
		StreamWriter writeFitData = new StreamWriter("fitData.txt");
		double delta = 0.02;
		for(double i=x[0]; i<=x[n-1]; i+=delta){
			writeFitData.WriteLine("{0:f2}\t{1}", i, Exp(fit.eval(i)));
		}
		writeFitData.Close();
		
	}
}
