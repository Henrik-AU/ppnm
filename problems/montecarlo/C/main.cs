using System;
using System.IO;
using static System.Console;
using static System.Math;

class main{
	static void Main(){

		// TO BE IMPLEMENTED	

		// To test the stratified Monte Carlo algorithm, we use a function with stronger
		// variations in some regions than others.

		// Lower and upper corner point of volume
		//vector a = new vector(0.0, 0.0, 0.0);
		//vector b = new vector(3, 3, 3);

		/*
		Func<vector, double> f = delegate(vector x){
			// This function is on a simple rectangular volume
			double result = ;
			return result;
		};
		*/

		// double expected = ;

		// Amount of random points to be evaluated at first, and the wanted accuracy of the
		// result
		int N = 10000;
		double acc = 1e-3;

		vector estimate = montecarlo.mcStrat(f, a, b, N);
		
		// Print out results

	} // end Main function

} // end class
