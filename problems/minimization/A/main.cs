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
		WriteLine("Analytic minima is located at x = 1, y = 1.");
		WriteLine("Starting search at x = {0:f5}, y = {1:f5}", xstart[0], xstart[1]);
		
		int nsteps = minimization.qnewton(rosenbrock, ref xstart);

		WriteLine("Minimum found at x = {0:f5}, y = {1:f5}", xstart[0], xstart[1]);
		WriteLine("f(x_min) = {0}", rosenbrock(xstart));

		vector grad = minimization.gradient(rosenbrock, xstart);
		WriteLine("Accuracy goal for gradient: {0}", eps);
		WriteLine("Actual gradient norm: \t{0:f6}", grad.norm());
		WriteLine("Search completed in {0} steps.", nsteps);


		WriteLine("\n---------------------------------------------\n");

		// Next let's try with Himmelblau's function

		WriteLine("Attempting to find the minima for Himmelblau's function.");
		WriteLine("Himmelblau's function has four local minima. These are located at:");

		// Write out the position of the four local minima for Himmelblau's function
		vector xminima = new vector(3.0, -2.805118, -3.779310, 3.584428);		
		vector yminima = new vector(2.0, 3.131312, -3.283186, -1.848126);		

		for(int i=0; i<4; i++){
			WriteLine("x = {0}\t y = {1}", xminima[i], yminima[i]);
		}

		WriteLine("\nLooking for the first minimum.");	
		// Starting point that should give the first minimum.
		xstart = new vector(1, 1);
		himmelblauMin(xstart, eps);
	
		WriteLine("Looking for the second minimum.");	
		// Starting point that should give the second minimum
		xstart = new vector(-5, -5);
		himmelblauMin(xstart, eps);
	
		WriteLine("Looking for the third minimum.");	
		// Starting point that should give the third minimum
		xstart = new vector(-2, 4);
		himmelblauMin(xstart, eps);	
	
		WriteLine("Looking for the fourth minimum.");	
		// Starting point that should give the fourth minimum
		xstart = new vector(4, -2);
		himmelblauMin(xstart, eps);	
	}


	static void himmelblauMin(vector xstart, double eps){
		// Find a minimum for the Himmelblau function and print relevant information
		
		// We define a function that returns the value of the function at x,y
		Func<vector, double> himmelblau = delegate(vector k){
			double x = k[0];
			double y = k[1];
			double fVal = Pow(x*x+y-11, 2) + Pow(x+y*y-7, 2);
			return fVal;
		};
		
		WriteLine("Starting search at x = {0}, y = {1}", xstart[0], xstart[1]);

		int nsteps = minimization.qnewton(himmelblau, ref xstart, eps);
		
		WriteLine("Minimum found at x = {0:f6}, y = {1:f6}", xstart[0], xstart[1]);
		WriteLine("f(x_min) = {0}", himmelblau(xstart));
		
		vector grad = minimization.gradient(himmelblau, xstart);
		WriteLine("Accuracy goal for gradient: {0}", eps);
		WriteLine("Actual gradient norm: \t{0:f6}", grad.norm());

		WriteLine("Search completed in {0} steps.", nsteps);
		Write("\n");
	}

}
