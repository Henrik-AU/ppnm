using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;

class main{
	static void Main(){

	// We define a function for the differential equation for the square root
	Func<double, vector, vector> squareroot = delegate(double x, vector y){
		return new vector(1/(2*y[0]), 0);

	};
		
	// Initial conditions and region of interest
	double xa = 0.01;
	double xb = 50;
	vector ystart = new vector(0.1, 5);

	List<double> xs = new List<double>();
	List<vector> ys = new List<vector>();

	// Solve the differential equation
	ode.rk23(squareroot, xa, ystart, xb, xlist:xs, ylist:ys);
	
	// Print out the data to a file
	for(int i=0; i<xs.Count; i++)
		WriteLine("{0:f8}\t{1:f8}", xs[i], ys[i][0]);


}

}
