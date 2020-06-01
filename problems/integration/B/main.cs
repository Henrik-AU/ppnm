using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(){
		/*
		Let's try to integrate a few functions with the implemented open 4-point recursive
		adaptive integrator with Clenshaw-Curtis substitution, and compare with the routine
		without substitution, and Dmitris o8av routine.

		The functions can be counted by making the function increment a counter each time
		it is called. I actually implemented a system to return the amount of function
		calls without this trick, so my o4a code automatically counts it and returns it
		as a part of the result vector. That is however not implemented in Dmitris o8av
		routine. Thus this trick here is used for the calls to that function, although
		it could also as well have been used for the calls to the o4a and o4ac functions.
		*/ 
		int calls = 0;
		Func<double,double> f = (x) => {calls++; return 1/Sqrt(x);};

		double a = 0;
		double b = 1;
		double eps = 1e-3;
		double acc = 1e-3; 

		vector result_o4ac = integration.o4ac(f, a, b, acc, eps);
		vector result_o4a = integration.o4a(f, a, b, acc, eps);
		calls = 0;
		double integral = quad.o8av(f, a, b, acc, eps);
		vector result_o8av = new vector(new double[] {integral, calls});
		double exact = 2;		

		WriteLine("Calculating the integral from 0 to 1 of 1/sqrt(x):");
		printResults(result_o4ac, result_o4a, result_o8av, exact, eps, acc);
	

		// Let's try another function on the same interval
		Func<double, double> g = (x) => {calls++; return Log(x)/Sqrt(x);};

		eps = 1e-4;
		acc = 1e-4;
		
		result_o4ac = integration.o4ac(g, a, b, acc, eps);
		result_o4a = integration.o4a(g, a, b, acc, eps);
		calls = 0;
		integral = quad.o8av(g, a, b, acc, eps);
		result_o8av = new vector(new double[] {integral, calls});
		exact = -4;		

		WriteLine("Calculating the integral from 0 to 1 of ln(x)/sqrt(x):");
		printResults(result_o4ac, result_o4a, result_o8av, exact, eps, acc);

		// Let's try a third function
		Func<double, double> h = (x) => {calls++; return 4*Sqrt(1-x*x);};
	
		eps = 1e-6;
		acc = 1e-6; 

		result_o4ac = integration.o4ac(h, a, b, acc, eps);
		result_o4a = integration.o4a(h, a, b, acc, eps);
		calls = 0;
		integral = quad.o8av(h, a, b, acc, eps);
		result_o8av = new vector(new double[] {integral, calls});
		exact = PI;		

		WriteLine("Calculating the integral from 0 to 1 of 4*sqrt(1-x^2):");
		printResults(result_o4ac, result_o4a, result_o8av, exact, eps, acc);

		WriteLine();
		WriteLine("The o4ac routine is significantly more efficient than o4a for some" +
		" of the specific integrands.");
		WriteLine("The o8av routine is however still superior.");
		WriteLine("Notice that o4a is allowed to run unrestricted, doing recursive calls" +
		" until it deems that the error is low enough.");

	}

	public static void printResults(vector result_o4ac, vector result_o4a, vector result_o8av, 
		double exact, double eps, double acc){
			double tolGoal = acc + Abs(exact)*eps;
			WriteLine("Relative tolerance: \t\t{0}", eps);
			WriteLine("Absolute tolerance: \t\t{0}", acc);
			WriteLine("Tolerance goal: \t\t{0}", tolGoal);
			WriteLine("Analytic result: \t\t{0}", exact);
			WriteLine("Result with o4ac: \t\t{0:f14}", result_o4ac[0]);
			WriteLine("Result with o4a: \t\t{0:f14}", result_o4a[0]); 
			WriteLine("Result with o8av: \t\t{0:f14}", result_o8av[0]);
			WriteLine("Estimated error with o4ac: \t{0}", result_o4ac[1]);
			WriteLine("Estimated error with o4a: \t{0}", result_o4a[1]);
			WriteLine("Actual error with o4ac: \t{0}", Abs(exact-result_o4ac[0]));
			WriteLine("Actual error with o4a: \t\t{0}", Abs(exact-result_o4a[0]));
			WriteLine("Actual error with o8av: \t{0}", Abs(exact-result_o8av[0]));
			WriteLine("Function evaluations with o4ac: {0}", result_o4ac[2]);	
			WriteLine("Function evaluations with o4a: \t{0}", result_o4a[2]);
			WriteLine("Function evaluations with o8av: {0}", result_o8av[1]);
			WriteLine();
	}
}
