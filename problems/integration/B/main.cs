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

		vector resultCC = integration.o4ac(f, a, b, acc, eps);
		vector resultNoCC = integration.o4a(f, a, b, acc, eps);
		calls = 0;
		double integral = quad.o8av(f, a, b, acc, eps);
		vector resultV = new vector(new double[] {integral, calls});		

		WriteLine("Calculating the integral from 0 to 1 of 1/sqrt(x):");
		printResults(resultCC, resultNoCC, resultV, 2, eps, acc);
	

		// Let's try another function on the same interval
		Func<double, double> g = (x) => {calls++; return Log(x)/Sqrt(x);};

		resultCC = integration.o4ac(g, a, b, acc, eps);
		resultNoCC = integration.o4a(g, a, b, acc, eps);
		calls = 0;
		integral = quad.o8av(g, a, b, acc, eps);
		resultV = new vector(new double[] {integral, calls});		

		WriteLine("Calculating the integral from 0 to 1 of ln(x)/sqrt(x):");
		printResults(resultCC, resultNoCC, resultV, -4, eps, acc);

		// Let's try a third function
		Func<double, double> h = (x) => {calls++; return 4*Sqrt(1-x*x);};
	
		eps = 0;
		acc = 1e-9; 

		resultCC = integration.o4ac(h, a, b, acc, eps);
		resultNoCC = integration.o4a(h, a, b, acc, eps);
		calls = 0;
		integral = quad.o8av(h, a, b, acc, eps);
		resultV = new vector(new double[] {integral, calls});		

		WriteLine("Calculating the integral from 0 to 1 of 4*sqrt(1-x^2):");
		printResults(resultCC, resultNoCC, resultV, PI, eps, acc);

		WriteLine();
		WriteLine("The o4ac routine is significantly more efficient than o4a for some" +
		" of the specific integrands. The o8av routine is however still superior.");

	}

	public static void printResults(vector resultCC, vector resultNoCC, vector resultV, 
		double resultAnalytical, double eps, double acc){
			WriteLine("Relative tolerance: {0}", eps);
			WriteLine("Absolute tolerance: {0}", acc);
			WriteLine("Analytical result: \t\t{0}", resultAnalytical);
			WriteLine("Result with o4ac: \t\t{0:f14} (deviation: {1})",
			resultCC[0], resultAnalytical-resultCC[0]); 
			WriteLine("Result with o4a: \t\t{0:f14} (deviation: {1})",
			resultNoCC[0], resultAnalytical-resultNoCC[0]); 
			WriteLine("Result with o8av: \t\t{0:f14} (deviation: {1})",
			resultV[0], resultAnalytical-resultV[0]); 
			WriteLine("Function evaluations with o4ac: {0}", resultCC[2]);	
			WriteLine("Function evaluations with o4a: \t{0}", resultNoCC[2]);
			WriteLine("Function evaluations with o8av: {0}", resultV[1]);
			WriteLine();
	}
}
