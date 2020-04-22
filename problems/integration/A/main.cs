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
	
		WriteLine("Calculating the integral from 0 to 1 of sqrt(x):");
		WriteLine("Using relative tolerance {0} and absolute tolerance {1}.", eps, acc);
		WriteLine("The o4a routine estimates the integral to be {0}", result[0]); 
		WriteLine("The analytical value for this integral is {0}.", 2.0/3);

		// Let's try another function on the same interval
		Func<double, double> f = (x) => 4*Sqrt(1-x*x);

		result = integration.o4a(f, a, b, acc, eps);

		WriteLine();
		WriteLine("Calculating the integral from 0 to 1 of 4*sqrt(1-x^2):");	
		WriteLine("Using relative tolerance {0} and absolute tolerance {1}.", eps, acc);
		WriteLine("The o4a routine estimates the integral to be {0}", result[0]); 
		WriteLine("The analytical value for this integral is {0}.", PI);
	}
}
