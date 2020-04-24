using System;
using static System.Console;
using static System.Math;
using static System.Double;

class main{
	static void Main(){
		/*
		Let's try to integrate a few functions with the implemented open 4-point recursive
		adaptive integrator with variable transformation to handle infinite limits, and
		compare to Dmitris o8av routine.

		The functions can be counted by making the function increment a counter each time
		it is called. I actually implemented a system to return the amount of function
		calls without this trick, so my o4a code automatically counts it and returns it
		as a part of the result vector. That is however not implemented in Dmitris o8av
		routine. Thus this trick here is used for the calls to that function, although
		it could also as well have been used for the calls to the o4a and o4av functions.
		*/ 
		int calls = 0;
		Func<double,double> f = (x) => {calls++; return Exp(-2*x*x);};

		double a = 0;
		double b = PositiveInfinity;
		double eps = 1e-9;
		double acc = 1e-9; 

		vector result_o4av = integration.o4av(f, a, b, acc, eps);
		calls = 0;
		double integral = quad.o8av(f, a, b, acc, eps);
		vector result_o8av = new vector(new double[] {integral, calls});		

		WriteLine("Calculating the integral from 0 to +infty of Exp(-2*x^2)");
		printResults(result_o4av, result_o8av, 0.5*Sqrt(PI/2), eps, acc);
	
		// Let's try another function on the same interval
		Func<double, double> g = (x) => {calls++; return 1/(x*x + 4*4);};

		result_o4av = integration.o4av(g, a, b, acc, eps);
		calls = 0;
		integral = quad.o8av(g, a, b, acc, eps);
		result_o8av = new vector(new double[] {integral, calls});		

		WriteLine("Calculating the integral from 0 to +infty of 1/(x^2 + 4^2)");
		printResults(result_o4av, result_o8av, 0.5*PI/4, eps, acc);

		
		// Let's try the same funktion from minus infinity to zero. The result should be
		// the same since the integrand is even.
		Func<double, double> h = (x) => {calls++; return 1/(x*x + 4*4);};
		a = NegativeInfinity;
		b = 0;
	
		result_o4av = integration.o4av(h, a, b, acc, eps);
		calls = 0;
		integral = quad.o8av(h, a, b, acc, eps);
		result_o8av = new vector(new double[] {integral, calls});		

		WriteLine("Calculating the integral from -infty to 0 of 1/(x^2 + 4^2)");
		printResults(result_o4av, result_o8av, 0.5*PI/4, eps, acc);

	}

	public static void printResults(vector result_o4av, vector result_o8av, 
		double resultAnalytical, double eps, double acc){
			WriteLine("Relative tolerance: {0}", eps);
			WriteLine("Absolute tolerance: {0}", acc);
			WriteLine("Analytical result: \t\t{0}", resultAnalytical);
			WriteLine("Result with o4av: \t\t{0:f14} (deviation: {1})",
			result_o4av[0], resultAnalytical-result_o4av[0]); 
			WriteLine("Result with o8av: \t\t{0:f14} (deviation: {1})",
			result_o8av[0], resultAnalytical-result_o8av[0]);
			WriteLine("Estimated error with o4av: \t{0}", result_o4av[1]);
			WriteLine("Function evaluations with o4av: {0}", result_o4av[2]);	
			WriteLine("Function evaluations with o8av: {0}", result_o8av[1]);
			WriteLine();
	}
}
