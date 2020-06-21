using System;
using static System.Math;

public partial class integration{
	public static vector o4ac(Func<double, double> f, double a, double b, double acc, double eps){

		// Clenshaw-Curtis substitution
		// Performs the substitution, then calls the normal o4a integrator.

		// We use a linear map such that u(a) = -1, and u(b) = 1, to rescale the problem
		Func<double, double> fv = x => f((a+b)/2+(b-a)*Cos(x)/2)*Sin(x)*(b-a)/2;

		// Call the o4a algorithm to handle the actual integration of the reformulated problem
		return o4a(fv, 0, PI, acc, eps);
	}
}
