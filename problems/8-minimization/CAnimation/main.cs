using System;
using System.IO;
using static System.Console;
using static System.Math;
using System.Collections.Generic;


class main{
	static void Main(){

		// Let's try to find the minimum for Rosenbrock's valley function
		// We define a function that returns the value of the function at x,y
		Func<vector, double> rosenbrock = delegate(vector k){
			double x = k[0];
			double y = k[1];
			double fVal = (1-x)*(1-x) + 100*(y-x*x)*(y-x*x);
			return fVal;
		};
		
		StreamWriter printRosen = new StreamWriter("rosenbrock.txt");
		double dl = 0.2;
		double dp = 0.2;
		for(double l =-7; l<=7; l+=dl){
			// Gnuplot needs an empty line in the data set everytime the x-value changes
			// otherwise 'set pm3d' gives an error
			printRosen.WriteLine();
			for(double p = -7; p<=7; p+=dp){
				vector pr = new vector(l, p);
				printRosen.WriteLine("{0} {1} {2}", l, p, rosenbrock(pr));
			}
		}
		printRosen.Close();

		// The function takes two input arguments, so we need 3 starting points
		// for the downhill simplex algorithm

		vector point1 = new vector(-2, -4.8);		
		vector point2 = new vector(0, -1);		
		vector point3 = new vector(-3, 2);
		
		List<vector> points = new List<vector>();
		points.Add(point1);		
		points.Add(point2);		
		points.Add(point3);		

		// Next we call the Downhill Simplex algorithm
		int nsteps = 0;
		vector minPoint = minimization.dsimplex(rosenbrock, points, ref nsteps, 1e-6);

		// Print out the results
		WriteLine("Attempting to find the minimum for Rosenbrock's function.");
		WriteLine("Starting at the three points:\n");
		WriteLine("\tP1\tP2\tP3");
		WriteLine("x\t{0}\t{1}\t{2}", point1[0], point2[0], point3[0]);
		WriteLine("y\t{0}\t{1}\t{2}\n", point1[1], point2[1], point3[1]);

		WriteLine("Minimum found at: x = {0} \t y = {1}", minPoint[0], minPoint[1]);
		WriteLine("f(x_min) = {0}", rosenbrock(minPoint));
		WriteLine("Minimum found in {0} steps.", nsteps);

	}
}
