using System;
using System.IO;
using static System.Console;
using static linspline;

class main{
	public static int Main(string[] args){
		if(args.Length != 2) return 1;
		
		// The two input parameters will be the name of the file with the table values and the amount of lines
		// in that file
		string filein = args[0];
		int nlines = int.Parse(args[1]);

		StreamReader streamin = new StreamReader(filein);
		double[] xs = new double[nlines];
		double[] ys = new double[nlines];

		for(int i=0; i<nlines; i++){
			string line = streamin.ReadLine();
			
			// There is a third value in each line (the integral from x[0] to z), but we do not need the
			// value here
			string[] numbers = line.Split('\t');
			xs[i] = double.Parse(numbers[0]);
			ys[i] = double.Parse(numbers[1]);

		}

		streamin.Close();


		// Interpolate the data
		double xstart = xs[0];
		double xend = xs[nlines-1];
		double deltax = 0.02;
		var linterp = new linspline(xs, ys);
		for(double k=xstart; k<xend; k+=deltax){
			double interpval = linterp.eval(k);
			double integralval = linterp.integrate(k);

			WriteLine("{0,8:f4}\t{1,8:f4}\t{2,8:f4}", k, interpval, integralval);
		}		
		
		return 0;
}

}
