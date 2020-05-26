using static System.Math;
using static System.Console;

class main{
	static void Main(){
	// Generate a lot of x and lngamma(x) values from ~1 to 100000 for a plot. We need many points
	// in the beginning, and then less points later on.
	// We might as well do as Dmitri has suggested and generate them as many different exponents
	// of 10.
	double dx = 1.0/128;

	for(double p=dx; p<=5; p+=dx){
		// We write out the values of lngamma at the x-points so we can later plot them
		// with Gnuplot.
		// We will also write out values for Stirlings approximation, so we have something
		// to compare to.
		double x = Pow(10, p);
		WriteLine("{0}\t{1}\t{2}", x, math.lngamma(x), stirling(x));


	}

	} // end Main

	// First few terms of Stirlings approximation
	static double stirling(double x){
		return x*Log(x) - x - Log(x/(2*PI))/2;
	}

} // end class
