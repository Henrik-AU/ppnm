using System;
using static System.Console;
using System.Diagnostics;
using static System.Math;

class createtabval{
	static void Main(string[] args){
		Trace.Assert(args.Length == 2,"createtabval takes 2 input numbers");

		// Generate som data points that we can test the quadratic spline on
		// Also generates the values for the integral from x[0] to z
		
		Func<double, double> f = (x) => x*x + 2*x + 1 + 25*Sin(x);
		Func<double, double> F = (x) => x*x*x/3 + x*x + x - 25*Cos(x);
		Func<double, double> fprime = (x) => 2*x + 2 + 25*Cos(x);
		
		int xstart = int.Parse(args[0]);
		int n = int.Parse(args[1]);
		for(int i=xstart; i<n+xstart; i++){
			int xtabval = i;
			double ytabval = f(i);
			double integralval = F(i) - F(xstart);
			double derivval = fprime(i);
			WriteLine("{0}\t{1,8:f4}\t{2,8:f4}\t{3,8:f4}", xtabval, ytabval, integralval, derivval);
		
		}
	}
}
