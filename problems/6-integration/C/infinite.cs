using System;
using static System.Math;
using static System.Double;

public partial class integration{
	public static vector o4av(Func<double, double> f, double a, double b, double acc, double eps){
		
		// Accepts infinite limits. Converts to an integral with finite limits which can
		// then be solved by the o4a routine.
		// There are 3 cases we want to handle: Integrals from minus infinity to infinity,
		// and integrals where one of the limits are either plus or minus infinity, and
		// one limit is not.

		// From minus to plus infinity
		if(IsNegativeInfinity(a) && IsPositiveInfinity(b)){
			return o4a(u=>f(u/(1-u*u))*(1+u*u)/Pow(1-u*u, 2), -1, 1, acc, eps);
		}

		// Negative infinity as lower limit
		if(IsNegativeInfinity(a) && !IsPositiveInfinity(b)){
			return o4a(u=>f(b-(1-u)/u)/(u*u), 0, 1, acc, eps);
		}

		// Positive infinity as upper limit
		if(!IsNegativeInfinity(a) && IsPositiveInfinity(b)){
			return o4a(u=>f(a+(1-u)/u)/(u*u), 0, 1, acc, eps);	
		}
		// If none applies, just call the o4a routine as usual
		return o4a(f, a, b, acc, eps);
	}
}
