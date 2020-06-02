using System;
using System.IO;
using static System.Console;
using static System.Math;

class main{
	static void Main(){
	
		// To test that the subspline works as intended, it is first of all neccesary to
		// generate some data that can be splined. For simplicity let's use some simple
		// trigonometric functions.
		int n = 10;
		double[] xs = new double[n+1];
		double[] ys = new double[n+1];
		double[] yps = new double[n+1];
	
		// Create a streamwriter for writing out the tabulated values to an output file
		StreamWriter writeTabVal = new StreamWriter("tabVal.txt");

		for(int i=0; i<n+1; i++){
			xs[i] = 2*PI*i/n;
			ys[i] = Sin(xs[i]);
			yps[i] = Cos(xs[i]);
			writeTabVal.WriteLine("{0}\t{1}\t{2}", xs[i], ys[i], yps[i]);
		}
		writeTabVal.Close();

		// Spline the data
		var subspline = new subspline(xs, ys, yps);

	
		// Now let's generate a lot of data points from 0 to 2*pi and evaluate the spline at
		// that point, and the derivative and integral
		double dz = 0.1;
		for(double z=0; z<=2*PI; z+=dz){
			double splineVal = subspline.eval(z);
			double splineDeriv = subspline.deriv(z);
			double splineInteg = subspline.integrate(z);
			WriteLine("{0}\t{1}\t{2}\t{3}", z, splineVal, splineDeriv, splineInteg);
		}

	} // Main
} // class
