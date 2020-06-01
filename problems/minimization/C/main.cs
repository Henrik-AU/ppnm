using System;
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

		// The function takes two input arguments, so we need 3 starting points
		// for the downhill simplex algorithm

		vector point1 = new vector(20, 15);		
		vector point2 = new vector(8, 3);		
		vector point3 = new vector(4, 1);
		
		List<vector> points = new List<vector>();
		points.Add(point1);		
		points.Add(point2);		
		points.Add(point3);

		// Next we call the Downhill Simplex algorithm
		int nsteps = 0;
		double eps = 1e-6;
		vector minPoint = minimization.dsimplex(rosenbrock, points, ref nsteps, eps);

		// Print out the results
		WriteLine("Attempting to find the minimum for Rosenbrock's function.");
		WriteLine("Analytic minimum at:");
		WriteLine("x = 1");
		WriteLine("y = 1");
		printResults(point1, point2, point3, minPoint, rosenbrock, nsteps);

		WriteLine("\n---------------------------------------------\n");

	
		// Let's try with Himmelblau's function
		Func<vector, double> himmelblau = delegate(vector k){
			double x = k[0];
			double y = k[1];
			double fVal = Pow(x*x+y-11, 2) + Pow(x+y*y-7, 2);
			return fVal;
		};

		WriteLine("Himmelblau's function has four local minima. These are located at:");

		// Write out the position of the four local minima for Himmelblau's function
		vector xminima = new vector(3.0, -2.805118, -3.779310, 3.584428);		
		vector yminima = new vector(2.0, 3.131312, -3.283186, -1.848126);		

		for(int i=0; i<4; i++){
			WriteLine("x = {0}\t y = {1}", xminima[i], yminima[i]);
		}

		// Let's just try and see if we can find a minimum using the same starting
		// points that were used for the Rosenbrock function

		points.Clear();
		points.Add(point1);		
		points.Add(point2);		
		points.Add(point3);		

		nsteps = 0;
		minPoint = minimization.dsimplex(himmelblau, points, ref nsteps, eps);

		// Print out the results
		WriteLine();
		WriteLine("Attempting to find a minimum for Himmelblau's function.");
		printResults(point1, point2, point3, minPoint, himmelblau, nsteps);



		// Second attept with the Himmelblau function
		vector point4 = new vector(6, 11);	
		vector point5 = new vector(-8, -13);	
		vector point6 = new vector(4, 19);	

		points.Clear();
		points.Add(point4);		
		points.Add(point5);		
		points.Add(point6);	
	
		nsteps = 0;
		minPoint = minimization.dsimplex(himmelblau, points, ref nsteps, eps);
		
		// Print out the results
		Write("\n\n");
		WriteLine("Attempting to find a different minimum for Himmelblau's function.");
		printResults(point4, point5, point6, minPoint, himmelblau, nsteps);
	}


	static void printResults(vector point1, vector point2, vector point3, vector minPoint,
	Func<vector, double> f, int nsteps){
		WriteLine("Starting simplex at the three points:\n");
		WriteLine("\tP1\tP2\tP3");
		WriteLine("x\t{0}\t{1}\t{2}", point1[0], point2[0], point3[0]);
		WriteLine("y\t{0}\t{1}\t{2}\n", point1[1], point2[1], point3[1]);

		WriteLine("Minimum found at:");
		WriteLine("x = {0}", minPoint[0]);
		WriteLine("y = {0}", minPoint[1]);
		WriteLine("f(x_min) = {0}", f(minPoint));
		WriteLine("Minimum found in {0} steps.", nsteps);
	}
}
