using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(){

		// Let's try to integrate a few functions with the plain Montecarlo integrator


		// Let's try with a simple 3D function first
		Func<vector, double> f = delegate(vector x){
			// This function is on a simple rectangular volume
			double result = x[0]*x[0] + x[1]*x[1] + x[2]*x[2];
			return result;
		};
		
		// Lower and upper corner point of volume
		vector a = new vector(0.0, 0.0, 0.0);
		vector b = new vector(3, 3, 3);
		
		// Let's try with 15000 evaluations of random points in the volume
		int N = 15000;
		double expected = 243;
		vector estimate = montecarlo.plainmc(f, a, b, N);

		WriteLine("Integrating x^2 + y^2 + z^2 over x, y and z.");
		printResults(estimate, a, b, N, expected);


		Write("\n\n");

		// Let's try another 3D-function
		Func<vector, double> f2 = delegate(vector x){
			double result = 4*x[0]*x[1]*x[2];
			return result;
		};

		// Lower and upper corner point of volume
		a = new vector(1, 1, 2);
		b = new vector(6, 3, 4);

		// Let's try with 15000 evaluations of random points in the volume
		N = 15000;
		estimate = montecarlo.plainmc(f2, a, b, N);

		WriteLine("Integrating 4*x*y*z over x, y and z.");
		expected = 1680;
		printResults(estimate, a, b, N, expected);


		Write("\n\n");

		// Next let's try with Dmitris function
		Func<vector, double> fD = delegate(vector x){
			// This function is on a simple rectangular volume
			double result = 1/(1-Cos(x[0])*Cos(x[1])*Cos(x[2])) * 1/(PI*PI*PI);
			return result;
		};

		// Lower and upper corner point of volume
		a = new vector(0.0, 0.0, 0.0);
		b = new vector(PI, PI, PI);

		// Let's try with 25000 evaluations of random points in the volume
		N = 25000;
		estimate = montecarlo.plainmc(fD, a, b, N);

		WriteLine("Integrating 1/[1-cos(x)cos(y)cos(z)] * 1/(pi)^3 over x, y and z.");
		expected = 1.3932039296856768591842462603255;
		printResults(estimate, a, b, N, expected);


	} // end Main function

	public static void printResults(vector estimate, vector a, vector b, int N, double expected){
		Write("Lower integration limits: ");
		a.print();
		Write("Upper integration limits: ");
		b.print();
		WriteLine("Analytic value: \t\t{0}", expected);
		WriteLine("Estimate of integral: \t\t{0}", estimate[0]);
		WriteLine("Estimate of error: \t\t{0}", estimate[1]);
		WriteLine("Actual deviation: \t\t{0}", Abs(expected-estimate[0]));
		WriteLine("The function was sampled in {0} points.", N);
	} // end printResults

} // end class
