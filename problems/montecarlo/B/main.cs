using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(){

		// We will integrate a relatively simple function (x^2 + y^2 + z^2) many times
		// using different amount of point evaluations in the volume

		// Lower and upper corner point of volume
		vector a = new vector(0.0, 0.0, 0.0);
		vector b = new vector(3, 3, 3);

		Func<vector, double> f = delegate(vector x){
			// This function is on a simple rectangular volume
			double result = x[0]*x[0] + x[1]*x[1] + x[2]*x[2];
			return result;
		};
		
		double expected = 243;

		// We run it once outside a loop so we can save the scaling factor for the
		// O(1/sqrt(N)) part.
		int N = 10000;
		vector estimate = montecarlo.plainmc(f, a, b, N);
		double scaling = Sqrt(N)*estimate[1];

		WriteLine("{0}\t{1}\t{2}\t{3}",
		N, estimate[1], Abs(expected - estimate[0]), scaling*1/Sqrt(N));

		// We run the rest in a loop and print out the data
		for(int n = 20000; n<300000; n+=5000){
			estimate = montecarlo.plainmc(f, a, b, n);

			// Save the error estimate and actual error along with the amount of function
			// evaluations N and O(1/sqrt(N)) scaled
			WriteLine("{0}\t{1}\t{2}\t{3}",
			n, estimate[1], Abs(expected - estimate[0]), scaling*1/Sqrt(n));
		}

	} // end Main function

} // end class
