using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;

class main{
	static void Main(){
		
		double eps;

		// Create a function for the planet equation
		Func<double, vector, vector> planetmotion = delegate(double x, vector y){
			return new vector(y[1], 1 + eps*y[0]*y[0] - y[0]);
		};
		
		// Setup region of interest, start conditions and prepare lists for x and y data
		double xa = 0;
		double xb = 6*PI;
				
		//vector ystart = new vector(0.5, 0);
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();

		// Solve the differential equation
		ode.rk23(planetmotion,xa,ystart,xb,xlist:xs,ylist:ys);
		

		// Print out the data to a file
		//for(int i=0; i<xs.Count; i++)
		//	WriteLine("{0:f8}\t{1:f8}\t{2:f8}", xs[i], ys[i][0], ys[i][1]);

}

}
