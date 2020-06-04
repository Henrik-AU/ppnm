using System;
using System.IO;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

class main{
	static void Main(){
	
		// To test that the subspline works as intended, we first  generate some data that
		// can be splined. For simplicity let's use a simple sine function.
		int n = 10;
		double[] xs = new double[n];
		double[] ys = new double[n];
		double[] yps = new double[n];
	
		// Create a streamwriter for writing out the tabulated values to an output file
		StreamWriter writeTabSin = new StreamWriter("tabValSin.txt");

		for(int i=0; i<n; i++){
			xs[i] = 2*PI*i/n;
			ys[i] = Sin(xs[i]);
			yps[i] = Cos(xs[i]);
			writeTabSin.WriteLine("{0}\t{1}\t{2}", xs[i], ys[i], yps[i]);
		}
		writeTabSin.Close();

		// Spline the data
		subspline splineSin = new subspline(xs, ys, yps);

	
		// Now let's generate a lot of data points from 0 to 2*pi and evaluate the spline at
		// these points, and the derivative, second derivative and integral
		double dz = 0.1;
		for(double z=0; z<=2*PI; z+=dz){
			double splineVal = splineSin.eval(z);
			double splineDeriv = splineSin.deriv(z);
			double splineInteg = splineSin.integrate(z);
			double splineDeriv2 = splineSin.deriv2(z);
			WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", z, splineVal, splineDeriv, splineInteg,
			splineDeriv2);
		}


		
		/*
		Let's try with a more interesting function. Let's actually solve a second order
		differential equation to get y and y-prime values and then feed them to the
		subspline function. This is how one would usually obtain such a set of y and 
		y-prime values in physics problems. To solve the differential equation I'll just
		use my own implemented rk45 solver from the ODE problem.
		Let's go with the spherical Bessel functions. They are widely used in physics, for
		example in scattering theory. Let's see if we can find j_0(x).
		*/

		double N = 0.0;
		Func<double, vector, vector> bessel = delegate(double x, vector y){
			double y0 = y[1];
			double y1 = -1*(2*x*y[1] + (x*x - N*(N+1))*y[0])/x/x;
			return new vector(y0, y1);
		};

		// To avoid divide by zero errors, we start out at a slight off-set from zero
		double a = 0.01;
		double b = 10;
		double stepsize = 0.1;
		double acc = 1e-4;
		double eps = 1e-4;

		// Starting guess
		vector ya = new vector(1.0, 0.0);

		// Lists to collect the solution in
		List<double> xList = new List<double>();
		List<vector> yList = new List<vector>();

		// Call the ODE solver to solve the system
		ode.rk45(bessel, a, ya, b, stepsize, acc, eps, xlist:xList, ylist:yList);

		// The subspline function takes arrays of data. So before calling that routine,
		// we have to move the solution from lists to arrays.
		int m = xList.Count;
		double[] xsBessel = new double[m];
		double[] ysBessel = new double[m];
		double[] ypsBessel = new double[m];
		
		StreamWriter writeTabBessel = new StreamWriter("tabValBessel.txt");
		for(int i=0; i<xList.Count; i++){
			xsBessel[i] = xList[i];
			ysBessel[i] = yList[i][0];
			ypsBessel[i] = yList[i][1];
			writeTabBessel.WriteLine("{0}\t{1}\t{2}", xsBessel[i], ysBessel[i],
			ypsBessel[i]);
		}
		writeTabBessel.Close();

		// Spline the values obtained by the ODE-solver
		subspline splineBessel = new subspline(xsBessel, ysBessel, ypsBessel);

		// Now let's generate a lot of data points from a to b and evaluate the spline at
		// these points, and the derivative, second derivative and integral
		StreamWriter writeBessel = new StreamWriter("splineBessel.txt");
		double dp = 0.1;
		for(double p=a; p<=b; p+=dp){
			double splineVal = splineBessel.eval(p);
			double splineDeriv = splineBessel.deriv(p);
			double splineInteg = splineBessel.integrate(p);
			double splineDeriv2 = splineBessel.deriv2(p);
			writeBessel.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", p, splineVal, splineDeriv,
			splineInteg, splineDeriv2);
		}
		
		writeBessel.Close();

	} // Main
} // class
