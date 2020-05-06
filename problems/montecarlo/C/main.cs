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
			if(x[0]*x[0]+x[1]*x[1]<=0.9){
				return 1;
			}else{
				return 0;
			}
		};
			
		// Amount of random points to be evaluated at first, and the wanted accuracy of the
		// result
		int N = 20;
		double acc = 1e-2;

		vector estimate = montecarlo.mcStrat(f, a, b, N, acc, printP:true);
		
		// The expected integral value is the area of a circle with radius 0.9
		double expected = PI*0.9*0.9;
		// Print out results

		WriteLine("Integrating the function f(r<=0.9) = 1, f(r>0.9) = 0.");
		printResults(estimate, a, b, expected);


	} // end Main function

	public static void printResults(vector estimate, vector a, vector b, double expected){
		Write("Lower integration limits: ");
		a.print();
		Write("Upper integration limits: ");
		b.print();
		WriteLine("Analytic value: \t\t{0}", expected);
		WriteLine("Estimate of integral: \t\t{0}", estimate[0]);
		WriteLine("Estimate of error: \t\t{0}", estimate[1]);
		WriteLine("Actual error: \t\t\t{0}", Abs(expected-estimate[0]));
	} // end printResults

} // end class
