using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(){

		// Let's try to integrate a few functions with the implemented open 4-point recursive
		// adaptive integrator
		Func<double,double> sqrt = (x) => Sqrt(x);

		double a = 0;
		double b = 1;
		double eps = 1e-6;
		double acc = 1e-6; 

		vector result = integration.o4a(sqrt, a, b, acc, eps);
		double exact = 2.0/3;
	
		WriteLine("Calculating the integral from 0 to 1 of sqrt(x):");
		printResults(result, exact, eps, acc);


		// Let's try another function on the same interval
		Func<double, double> f = (x) => 4*Sqrt(1-x*x);

		result = integration.o4a(f, a, b, acc, eps);
		exact = PI;
		
		WriteLine();
		WriteLine("Calculating the integral from 0 to 1 of 4*sqrt(1-x^2):");	
		printResults(result, exact, eps, acc);
	}

	// Small function to print results, just to avoid too much code repetition, and to make it
	// easier to fix all formatting.
	static void printResults(vector result, double exact, double eps, double acc){
		double tolGoal = acc + exact*eps;

		WriteLine("Relative tolerance: \t\t{0}", eps);
		WriteLine("Absolute tolerance: \t\t{0}", acc);
		WriteLine("Tolerance goal: \t\t{0}", tolGoal);
		WriteLine("Numeric integral (o4a): \t{0}", result[0]); 
		WriteLine("Analytic integral: \t\t{0}", exact);
		WriteLine("Estimated error: \t\t{0}", result[1]);
		WriteLine("Actual error: \t\t\t{0}", exact-result[0]);
		WriteLine("Function evaluations: \t\t{0}", result[2]);
	}


}
