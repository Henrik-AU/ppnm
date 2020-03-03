using static System.Console;
using static System.Math;

class main{
	static void Main(){

	// We make a loop over a bunch of x-values and write out the value of the error
	// function such that we can redirect it to a .txt file with our Makefile, and
	// then plot it in Gnuplot.
	for(double x = -3; x<=3; x+=0.1){
		// :f is a 'fixed point' format specifier, setting a specific amount of
		// decimals. The number after the comma in {} is an added padding to
		// format the output nicely
		WriteLine("{0,6:f3} {1,16:f8}", x, math.erf(x));
	}

}

}
