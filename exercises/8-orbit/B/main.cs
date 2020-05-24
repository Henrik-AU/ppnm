using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;

class main{
	static void Main(){
		

		// Part I)
		vector ystart = new vector(1, 0);
		double eps = 0;
		double xa = 0;
		double xb = 2*PI;
		string filepathI = "outBpartI.txt";

		// To avoid code repetition we call a function orbitsolver for each set of parameters
		orbitsolver(eps, xa, xb, ystart, filepathI);
		
		
		// Part II)
		ystart[1] = -0.5;
		string filepathII = "outBpartII.txt";
		orbitsolver(eps, xa, xb, ystart, filepathII);
		
		
		// Part III)
		eps = 0.01;
		xb = 15*PI;
		string filepathIII = "outBpartIII.txt";
		orbitsolver(eps, xa, xb, ystart, filepathIII);


}

	public static void orbitsolver(double eps, double xa, double xb, vector ystart,
	string filepath){
		
		// Create a function for the planet equation
		Func<double, vector, vector> planetmotion = delegate(double x, vector y){
			return new vector(y[1], 1 + eps*y[0]*y[0] - y[0]);
		};

		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();
		
		double stepsize = 0.001;


		ode.rk23(planetmotion,xa,ystart,xb,xlist:xs,ylist:ys, h:stepsize);
		
		// Create a streamwriter connected to the wanted output file
		var outfile = new System.IO.StreamWriter(filepath);
		
		// Print out the data to the file (we don't need to print the first derivative)
		for(int i=0; i<xs.Count; i++){
			outfile.WriteLine("{0:f8}\t{1:f8}", xs[i], ys[i][0]);
		}

		outfile.Close();

	}	


}

