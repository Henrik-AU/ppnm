using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(){

		// Convergence criteria for the gradient
		double eps = 1e-4;

		// Let's try to find the minimum for Rosenbrock's valley function
		// Apparently also called Rosenbrocks banana function, although a valley and a
		// banana look vastly different...
	
		// We define a function that returns the value of the function at x,y
		Func<vector, double> rosenbrock = delegate(vector k){
			double x = k[0];
			double y = k[1];
			double fVal = (1-x)*(1-x) + 100*(y-x*x)*(y-x*x);
			return fVal;
		};


		// For the chosen Rosenbrock function the minimum should be at (1,1). We create a
		// random starting position that is somewhat in that vicinity
		var rand = new Random(2);
		vector xstart = new vector(4+1.1*rand.NextDouble(), 2-3*rand.NextDouble());


		WriteLine("Attempting to find the minimum for Rosenbrock's valley function.");
		WriteLine("The analytic minimum is located at:");
		WriteLine("x = 1");
		WriteLine("y = 1");
		findMinimum(xstart, rosenbrock, eps);

		WriteLine("\n---------------------------------------------\n");

		// Next let's try with Himmelblau's function
		
		// We define a function that returns the value of the function at x,y
		Func<vector, double> himmelblau = delegate(vector k){
			double x = k[0];
			double y = k[1];
			double fVal = Pow(x*x+y-11, 2) + Pow(x+y*y-7, 2);
			return fVal;
		};

		WriteLine("Attempting to find the minima for Himmelblau's function.\n");

		// Write out the position of the four local minima for Himmelblau's function
		vector xminima = new vector(3.0, -2.805118, -3.779310, 3.584428);		
		vector yminima = new vector(2.0, 3.131312, -3.283186, -1.848126);		


		WriteLine("Looking for the first minimum (at x = {0}, y = {1})",
		xminima[0], yminima[0]);	
		// Starting point that should give the first minimum.
		xstart = new vector(1, 1);
		findMinimum(xstart, himmelblau, eps);	
	
		WriteLine("Looking for the second minimum (at x = {0}, y = {1})",
		xminima[1], yminima[1]);	
		// Starting point that should give the second minimum
		xstart = new vector(-5, -5);
		findMinimum(xstart, himmelblau, eps);	
	
		WriteLine("Looking for the third minimum (at x = {0}, y = {1})",
		xminima[2], yminima[2]);	
		// Starting point that should give the third minimum
		xstart = new vector(-2, 4);
		findMinimum(xstart, himmelblau, eps);	
	
		WriteLine("Looking for the fourth minimum (at x = {0}, y = {1})",
		xminima[3], yminima[3]);	
		// Starting point that should give the fourth minimum
		xstart = new vector(4, -2);
		findMinimum(xstart, himmelblau, eps);	
	}


	static void findMinimum(vector xstart, Func<vector, double> f, double eps){
		// Find a minimum for the given function (Rosenbrock og Himmelblau) and print
		// relevant information
		
		WriteLine("Starting search at:");
		WriteLine("x = \t\t\t\t{0}", xstart[0]);
		WriteLine("y = \t\t\t\t{0}", xstart[1]);

		int nsteps = minimization.qnewton(f, ref xstart, eps);
		
		WriteLine("Minimum found at:");
		WriteLine("x = \t\t\t\t{0:f6}", xstart[0]);
		WriteLine("y = \t\t\t\t{0:f6}", xstart[1]);
		WriteLine("f(x_min) = \t\t\t{0}", f(xstart));
		
		vector grad = minimization.gradient(f, xstart);
		WriteLine("Accuracy goal for gradient: \t{0}", eps);
		WriteLine("Actual gradient norm: \t\t{0:f6}", grad.norm());

		WriteLine("Minimization steps: \t\t{0}", nsteps);
		Write("\n\n");
	}


}
