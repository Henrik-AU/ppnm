using System;
using System.IO;
using static System.Math;

class main{
	static void Main(){

		// Note: Contains problem A, B and C, since they are so interrelated, that splitting
		// it up in several files and folders would be quite an unneccesary hassle.

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
		double lna = fit.c[0];
		double dlna = Sqrt(fit.sigma[0,0]);
		double lambda = fit.c[1];
		double dlambda = Sqrt(fit.sigma[1,1]);

		// The half-life is then
		double T = -Log(2.0)/lambda;

		// Write out the found half-life and lambda+-dlambda
		StreamWriter writeFitResult = new StreamWriter("outA-B.txt");
		writeFitResult.WriteLine("The least squares fit gave the results:");
		writeFitResult.WriteLine("lambda = {0:f5} +/- {1:f5}", lambda, dlambda);
		writeFitResult.WriteLine("ln(a) = {0:f5} +/- {1:f5}", lna, dlna);
		writeFitResult.WriteLine("The half-life is thus estimated to be {0:f2} days", T);
		writeFitResult.WriteLine("The table value half-life is 3.66 days");


		// Generate data for the fitted curve
		StreamWriter writeFitData = new StreamWriter("fitData.txt");
		double delta = 0.02;
		for(double i=x[0]; i<=x[n-1]; i+=delta){
			writeFitData.WriteLine("{0:f2}\t{1}", i, Exp(fit.eval(i)));
		}
		

		// Part B
		// The uncertainty of the half-life value for ThX is calculated via the usual formula
		// for the uncertainty of a function with one variable: dq = dx*|dq/dx|
		double dT = Log(2.0)*1/(lambda*lambda)*dlambda;
		writeFitResult.WriteLine("\nThe uncertainty in the estimated halflife is {0:f2} days",
		dT);
		writeFitResult.WriteLine("The estimated value does thus not match the modern value" +
		" within the estimated uncertainty, but it is close.");
		writeFitResult.Close();

		// Part C
		// We generate data for plots where the fit coefficients are changed by the estimated
		// uncertainties. First they are changed one at time, and at last they are changed
		// together for the upper and lower boundaries of the uncertainty
		writeFitData.WriteLine();
		writeFitData.WriteLine();
		// ln(a) + dln(a)
		fit.c[0] += dlna;
		for(double i=x[0]; i<=x[n-1]; i+=delta){
			writeFitData.WriteLine("{0:f2}\t{1}", i, Exp(fit.eval(i)));
		}

		writeFitData.WriteLine();
		writeFitData.WriteLine();
		// ln(a) - dln(a)
		fit.c[0] -= 2*dlna;
		for(double i=x[0]; i<=x[n-1]; i+=delta){
			writeFitData.WriteLine("{0:f2}\t{1}", i, Exp(fit.eval(i)));
		}

		// Restore ln(a) to the original value from the fit
		fit.c[0] += dlna;
		
		writeFitData.WriteLine();
		writeFitData.WriteLine();
		// lambda + dlambda	
		fit.c[1] += dlambda;
		for(double i=x[0]; i<=x[n-1]; i+=delta){
			writeFitData.WriteLine("{0:f2}\t{1}", i, Exp(fit.eval(i)));
		}

		writeFitData.WriteLine();
		writeFitData.WriteLine();
		// lambda - dlambda
		fit.c[1] -= 2*dlambda;
		for(double i=x[0]; i<=x[n-1]; i+=delta){
			writeFitData.WriteLine("{0:f2}\t{1}", i, Exp(fit.eval(i)));
		}
		// Restore lambda to original value
		fit.c[1] += dlambda;

		// Lower boundary - lowest constant with fastest decay
		writeFitData.WriteLine();
		writeFitData.WriteLine();	
		fit.c[0] += dlna;
		fit.c[1] += dlambda;
		for(double i=x[0]; i<=x[n-1]; i+=delta){
			writeFitData.WriteLine("{0:f2}\t{1}", i, Exp(fit.eval(i)));
		}


		// Upper boundary - highest constant with slowest decay
		writeFitData.WriteLine();
		writeFitData.WriteLine();	
		fit.c[0] -= 2*dlna;
		fit.c[1] -= 2*dlambda;
		for(double i=x[0]; i<=x[n-1]; i+=delta){
			writeFitData.WriteLine("{0:f2}\t{1}", i, Exp(fit.eval(i)));
		}

		writeFitData.Close();

	}
}
