using System;
using System.IO;
using static System.Console;
using static System.Math;

class main{
	static void Main(){

		// To test the stratified Monte Carlo algorithm, we use a function with stronger
		// variations in some regions than others.

		// Lower and upper corner point of volume
		vector a = new vector(-1, -1);
		vector b = new vector(1, 1);

		Func<vector, double> f = delegate(vector x){
			// This function is on a simple rectangular volume
			if(x[0]*x[0]+x[1]*x[1]<0.9){
				return 1;
			}else{
				return 0;
			}
		};
			

		// double expected = ;

		// Amount of random points to be evaluated at first, and the wanted accuracy of the
		// result
		int N = 25;
		double acc = 1e-1;

		vector estimate = montecarlo.mcStrat(f, a, b, N, acc, printP:true);
		
		// Print out results

	} // end Main function

} // end class
