using System;

public class hydrogen{
	public static double solve(double e, double rmax){
		
		// Solve the Schrödinger equation via the ODE solver
		double h = 1e-3;
		double acc = 1e-3;
		double eps = 0;
		double r0 = 1e-3;

		// Starting condition. We start at a low r-value. We are in the asymptotic area here
		// so we will use f(r) = r - r^2, and f'(r) = 1 - 2*r to set up starting conditons
		vector f0 = new vector(r0-r0*r0, 1 - 2*r0);
	
		// Solve and return
		vector frmax = ode.rk45(diffeq(e), r0, f0, rmax, h, acc, eps);
		return frmax[0];
	}
	
	public static Func<double, vector, vector> diffeq(double e){	
		return (r, f) => new vector(f[1], -2*(1/r + e)*f[0]);
	}
}
