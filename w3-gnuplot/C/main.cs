using static System.Console;
using static cmath;
using static complex;

static class main{
	static void Main(){
		
		double eps = 1.0/128;
		double dx = 1.0/32;
		double dy = dx;

		for(double x =-4.5+eps; x<4.5-eps; x+=dx){
			// Gnuplot needs an empty line in the data set everytime the x-value changes
			// otherwise 'set pm3d' gives an error
			WriteLine();
			for(double y = -5+eps; y < 5-eps; y+=dy)
				WriteLine("{0} {1} {2}", x, y, abs(math.cgamma(x + I*y)));			
		}
}

}
