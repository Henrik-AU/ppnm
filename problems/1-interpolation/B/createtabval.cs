using System;
using System.IO;
using static System.Console;
using System.Diagnostics;
using static System.Math;

class createtabval{
	static void Main(string[] args){
		Trace.Assert(args.Length == 4,"createtabval takes 2 input numbers and the names of the output files");

		// We will use two Streamwriters to write the data points for the spline to one
		// textfile and the values of the function, the derivative and integral to
		// another textfile
		string tabvalpath = args[0];
		string exactfuncspath = args[1];
		StreamWriter writetabval = new StreamWriter(tabvalpath);
		StreamWriter writeexactfuncs = new StreamWriter(exactfuncspath);

		// Generate som data points that we can test the quadratic spline on
		// Also generates the values for the integral from x[0] to z
		Func<double, double> f = (x) => x*x + 2*x + 1 + 25*Sin(x);
		Func<double, double> F = (x) => x*x*x/3 + x*x + x - 25*Cos(x);
		Func<double, double> fprime = (x) => 2*x + 2 + 25*Cos(x);
		
		int xstart = int.Parse(args[2]);
		int n = int.Parse(args[3]);
		for(int i=xstart; i<n+xstart; i++){
			int xtabval = i;
			double ytabval = f(i);
			writetabval.WriteLine("{0}\t{1,8:f4}", xtabval, ytabval);
		
		}
		double delta = 0.02;
		for(double j=xstart; j<(n-1)+xstart; j+=delta){
			double integralval = F(j) - F(xstart);
			double derivval = fprime(j);
			writeexactfuncs.WriteLine("{0,4:f4}\t{1,8:f4}\t{2,8:f4}\t{3,8:f4}", j, f(j), integralval, derivval);
		}		

		writetabval.Close();
		writeexactfuncs.Close();

	}
}
