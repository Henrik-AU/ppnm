using System;
using static System.Console;
using System.Diagnostics;

class createtabval{
	static void Main(string[] args){
		Trace.Assert(args.Length == 2,"createtabval takes 2 input numbers");

		// Generate som data points that we can test the linear spline on
		// Also generates the values for the integral from x[0] to z
		
		Func<double, double> f = (x) => x*x + x + 4;
		Func<double, double> F = (x) => x*x*x/3 + x*x/2 + 4*x;
		
		int xstart = int.Parse(args[0]);
		int n = int.Parse(args[1]);
		for(int i=xstart; i<n+xstart; i++){
			int xtabval = i;
			double ytabval = f(i);
			double integralval = F(i) - F(xstart);
			WriteLine("{0}\t{1}\t{2,4:f4}", xtabval, ytabval, integralval);
			//WriteLine("{0}\t{1}", xtabval, ytabval);
		
		}
	}
}
