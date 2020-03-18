using System;
using System.IO;
using static System.Console;
using System.Diagnostics;

class createtabval{
	static void Main(string[] args){
		Trace.Assert(args.Length == 4,"createtabval takes 2 input numbers and the names of the output files");
		
		// We will use two Streamwriters to write the data points for the spline to one
		// textfile and the values of the function and the integral to another textfile
		string tabvalpath = args[0];
		string exactfuncspath = args[1];
		StreamWriter writetabval = new StreamWriter(tabvalpath);
		StreamWriter writeexactfuncs = new StreamWriter(exactfuncspath);
		
		// Generate som data points that we can test the linear spline on
		// Also generates the values for the integral from x[0] to z
		
		Func<double, double> f = (x) => x*x + x + 4;
		Func<double, double> F = (x) => x*x*x/3 + x*x/2 + 4*x;
		
		int xstart = int.Parse(args[2]);
		int n = int.Parse(args[3]);
		for(int i=xstart; i<n+xstart; i++){
			int xtabval = i;
			double ytabval = f(i);
			writetabval.WriteLine("{0}\t{1}", xtabval, ytabval);
		
		}
		double delta = 0.02;
		for(double j=xstart; j<(n-1)+xstart; j+=delta){
			double integralval = F(j) - F(xstart);
			writeexactfuncs.WriteLine("{0}\t{1}\t{2,4:f4}", j, f(j), integralval);
		}

		writetabval.Close();
		writeexactfuncs.Close();
	}
}
