using static System.Console;

class main{
	static void Main(){
	// We create a spacing for our x-values, and a small epsilon shift for the endpoints.
	// The epsilon shift helps us avoid problematic x-values (integer numbers)
	double eps = 1.0/64;
	double dx = 1.0/32;

	for(double x = -4+eps;x <=4-eps; x+=dx){
		// We write out the values of gamma at the x-points so we can later plot them with Gnuplot
		WriteLine("{0,6:f3} {1,16:f8}", x, math.gamma(x));


	}


}

}
