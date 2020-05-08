using System;
using System.IO;
using static System.Console;
using static System.Math;

class main{
	static void Main(){

		// To test the stratified Monte Carlo algorithm, we use a function with stronger
		// variations in some regions than others.
		int counts = 0;

		// Lower and upper corner point of volume
		vector a = new vector(-1, -1);
		vector b = new vector(1, 1);

		Func<vector, double> f = delegate(vector x){
			// This function is on a simple rectangular volume
			counts++;
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
		WriteLine("First we try to integrate a function with a very rapid change:\n");
		WriteLine("Integrating the function f(r<=0.9) = 1, f(r>0.9) = 0 using strata.");
		printResults(estimate, a, b, expected, counts);

		// Let's try with the plain Monte Carlo method also, using the same amount of points
		N = counts;
		counts = 0;
		estimate = montecarlo.plainmc(f, a, b, N);
		WriteLine("Integrating again using the plain Monte Carlo method:");
		printResults(estimate, a, b, expected, N);

		WriteLine("The stratified Monte Carlo method gives a better estimated accuracy than" +
		" the plain Monte Carlo method.");
		WriteLine("Neither of the methods are very accurate for this function however.\n");
		
		
		WriteLine("------------------------------------------\n");	

		
		// Let's try another function. Let's use a 3D function and compare it with the
		// plain Monte Carlo procedure once again.

		// Lower and upper corner point of volume
		a = new vector(0.0, 0.0, 0.0);
		b = new vector(3, 3, 3);

		Func<vector, double> f2 = delegate(vector x){
			// This function is on a simple rectangular volume
			counts++;
			double result = x[0]*x[0] + x[1]*x[1] + x[2]*x[2];
			return result;
		};
		
		N = 20;
		counts = 0;
		acc = 1e-1;

		expected = 243;
		estimate = montecarlo.mcStrat(f2, a, b, N, acc, printP:false);
		// Save the estimated error
		double errorStrat = estimate[1];

		// Print out results
		WriteLine("Let's try a function that varies more continuously.\n");
		WriteLine("Integrating the function x^2+y^2+z^2 using strata.");
		printResults(estimate, a, b, expected, counts);
		
		// Let's try with the plain Monte Carlo method also, using the same amount of points
		N = counts;
		estimate = montecarlo.plainmc(f2, a, b, N);
		WriteLine("Integrating again using the plain Monte Carlo method:");
		printResults(estimate, a, b, expected, N);

		// How many times better is the estimated error of the stratified version?
		// Let's try to do plain Monte Carlo with more points that should make it about 
		//as good as the stratified version (the error scales as 1/sqrt(N), for illustrative
		// purposes.

		WriteLine("Generally we notice that the estimated error is a lot lower with the" +
		" stratified Monte Carlo method");
		WriteLine("The error goes as 1/sqrt(N) so we need a lot more points to compensate " +
		"the plain Monte Carlo method.");
		WriteLine("Let's try to do it.\n");


		double errorFrac = estimate[1]/errorStrat;
		int N2 = Convert.ToInt32(N*errorFrac*errorFrac);
		estimate = montecarlo.plainmc(f2, a, b, N2);
		WriteLine("Plain Monte Carlo method with ~{0} times more points to compensate the" +
		" error:",
		N2/N);
		printResults(estimate, a, b, expected, N2);


		WriteLine("Of course the actual error may be better or worse than the estimated" +
		" error, since the Monte Carlo methods are random.");
		
	} // end Main function

	public static void printResults(vector estimate, vector a, vector b, double expected,
	int counts){
		Write("Lower integration limits: ");
		a.print();
		Write("Upper integration limits: ");
		b.print();
		WriteLine("Analytic value: \t\t{0}", expected);
		WriteLine("Estimate of integral: \t\t{0}", estimate[0]);
		WriteLine("Estimate of error: \t\t{0}", estimate[1]);
		WriteLine("Actual error: \t\t\t{0}", Abs(expected-estimate[0]));
		WriteLine("Function evaluations: \t\t{0}", counts);
		Write("\n\n");
	} // end printResults

} // end class
