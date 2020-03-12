using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;

class main{
	static void Main(){
		
		// Create a function for the differential equation
		Func<double, vector, vector> diffeq = delegate(double x, vector y){
			return new vector(y[0]*(1-y[0]), 0);
		};
		
		// Setup region of interest, start conditions and prepare lists for x and y data
		double xa = 0;
		double xb = 3;
		vector ystart = new vector(0.5, 0);
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();

		// Solve the differential equation
		ode.rk23(diffeq,xa,ystart,xb,xlist:xs,ylist:ys);
		
		// Create a function for the known solution to the differential equation
		Func<double, double> knownsol = (x) => 1/(1+Exp(-x));

		// Print out the data to a file
		for(int i=0; i<xs.Count; i++)
			WriteLine("{0:f8}\t{1:f8}\t{2:f8}\t{3:f8}", xs[i], ys[i][0], ys[i][1], knownsol(xs[i]));

}

}
