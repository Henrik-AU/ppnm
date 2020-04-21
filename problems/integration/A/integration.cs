using System;
using static System.Console;
using static System.Math;
using static System.Double;

public class integration{
	public static vector o4a(Func<double, double> f, double a, double b, double acc, double eps,
	double f2 = NaN, double f3 = NaN){

		// We will use a simple open 4-point adaptive integrator with a trapezoidal rule and
		// with a rectangular rule.
		// The weights are:
		vector w = new vector(new double[] {2.0/6, 1.0/6, 1.0/6, 2.0/6});
		vector v = new vector(new double[] {1.0/4, 1.0/4, 1.0/4, 1.0/4});

		// Abscissas when scaled to an interval [0,1].
		vector xi = new vector(new double[] {1.0/6, 2.0/6, 4.0/6, 5.0/6});

		double L = b - a;

		// We evaluate the functions at the rescaled abscissas (a + (b-a)*xi) = a + L*xi
		double f1 = f(a + L*xi[0]);
		double f4 = f(a + L*xi[3]);

		// During the first run we have to calculate the values at the two middle points,
		// f1 and f2,also. During recursive runs we will reuse the ones from the previous run.
		if(IsNaN(f2)){
			f2 = f(a + L*xi[1]);
			
		}
		if(IsNaN(f3)){
			f3 = f(a + L*xi[2]);
		}	
		
		// Evaluate the integral with the trapezoidal weights and the rectangular weights
		double Q = (f1*w[0]+f2*w[1]+f3*w[2]+f4*w[3])*L;
		double q = (f1*v[0]+f2*v[1]+f3*v[2]+f4*v[3])*L;

		// Estimate the error
		double error = Abs(Q-q);

		// If the error is less than the tolerated error, then accept the result and return it
		if(error < acc + eps*Abs(Q)){
			return new vector(new double[] {Q, error});
		}else{
			// If the error is too large then split the interval in the middle and
			// evaluate each half individually, then add the results and return it
			vector interval1 = o4a(f, a, (a+b)/2, acc/Sqrt(2), eps, f1, f2);
			vector interval2 = o4a(f, (a+b)/2, b, acc/Sqrt(2), eps, f3, f4);
			
			// Add results
			Q = interval1[0] + interval2[0];
			error = Sqrt(Pow(interval1[1], 2) + Pow(interval2[1], 2));
			return new vector(new double[] {Q, error});
		}
	}
}
