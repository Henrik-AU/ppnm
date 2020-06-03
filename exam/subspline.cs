using System;
using static System.Console;
using System.Diagnostics;


public class subspline{

	// The coefficients for the interpolated polynomial should be accessible for the whole class,
	// and so should the x and y values used for the spline. Let's keep them private to keep
	// the user from changing them by accident.
	private double[] x, y, yp, b, c, d, e;


	/*
	The constructor will do the splining.
	The data is assumed to be ordered from lowest x-values to highest. Thus xs[0] is the
	lowest x-value, and xs[xs.Length - 1] is the highest x-value. The subspline will only be
	defined for points in between these two values.
	*/
	public subspline(double[] xs, double[] ys, double[] yps){
		int n = xs.Length;
		Trace.Assert(ys.Length == n, "The amount of x and y values are not identical.");
		Trace.Assert(yps.Length == n,
		"The amount of x and y-prime values are not identical.");


		// Prepare the arrays that will hold the x, y and y-prime values in the class
		x = new double[n];
		y = new double[n];
		yp = new double[n];

		// Store the xs, ys and yps values in the x, y and yp variables accesible to the
		// whole class
		for(int i=0; i<n; i++){
			x[i] = xs[i];
			y[i] = ys[i];
			yp[i] = yps[i];
		}

		double[] h = new double[n-1];
		double[] p = new double[n-1];	
		double[] q = new double[n-1];
		for(int i=0; i<n-1; i++){
			h[i] = x[i+1]-x[i];
			// The x-values should be ordered. If not, then this Trace.Assert statement
			// will spot it and throw an error
			Trace.Assert(h[i] > 0);

			// Calculate p[i] and q[i] via the just-found h[i]	
			p[i] = (y[i+1]-y[i])/h[i];
			q[i] = (yp[i+1]-yp[i])/h[i];
		}


		// Set up the arrays for the coefficients
		b = new double[n-1];
		c = new double[n-1];
		d = new double[n-1];
		e = new double[n-1];

		// The b-coefficients are equal to the y-prime values. The last y prime point (yp[n])
		// is not needed.
		for(int i=0; i<n-1; i++){
			b[i] = yp[i];
		}

		// Calculate the c- and d-coefficients
		for(int i=0; i<n-1; i++){
			c[i] = 3*(p[i]-b[i])/h[i] - q[i];
			d[i] = (q[i] - 2*c[i]) / (3*h[i]);
		}
		
		// At last we calculate the e-coefficients recursively, at first in one direction,
		// and then in the other direction
		e[0] = 0;
		for(int i=0; i<n-2; i++){
			e[i+1] = ((c[i]-c[i+1]) + 3*d[i]*h[i] + e[i]*h[i]*h[i])/h[i+1]/h[i+1];
		}

			
		e[n-2] = e[n-2]/2;
		for(int i=n-3; i>=0; i--){
			e[i] = ((c[i+1]-c[i]) - 3*d[i]*h[i] + e[i+1]*h[i+1]*h[i+1])/h[i]/h[i];
		}
		
	 
	} // end constructor

	// We use the binary search strategy (also used previously in the course), to locate the
	// index i of the interval that contains the z value, x[i] < z < x[i+1]
	public static int binsearch(double[] x, double z){
		int i=0;
		int j=x.Length-1;
		// locate the interval for z
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]){
				i=mid;
			}else{
				j=mid;
			}
		}
		return i;
	}

	// Function that evaluates the spline at a point z in the domain that the spline has been
	// defined for (that is, between the x_min and x_max values used for the splining).
	public double eval(double z){
		Trace.Assert(z>=x[0] && z<=x[x.Length-1]);
		int i = binsearch(x,z);
		// Calculate the spline value
		double dx = z - x[i];
		double dxe = z - x[i+1];
		return y[i]+dx*(b[i]+dx*(c[i] + e[i]*dxe*dxe + dx*d[i]));
	}
	
	// Function that evaluates the derivative of the splined function at the point z
	// (z must lie in the splined domain).
	public double deriv(double z){
		Trace.Assert(z>=x[0] && z<=x[x.Length-1]);
		int i=binsearch(x,z);
		double dx=z-x[i];
		double dxe = z - x[i+1];
		return b[i]+dx*(2*c[i]+dx*3*d[i]) + 2*e[i]*(dx*dxe*dxe + dx*dx*dxe);
		}
	
	// Function that evaluates the second derivative of the splined function at the point z
	public double deriv2(double z){
		Trace.Assert(z >= x[0] && z <= x[x.Length-1]);
		int i=binsearch(x,z);
		double dx=z-x[i];
		double dxe = z - x[i+1];
		return 2*c[i]+6*d[i]*dx +2*e[i]*(dxe*dxe + dx*dx + 4*dx*dxe);
		}

	// Function that evaluates the integral of the splined function from x[0] to the point z
	// (z must lie in the splined domain).
	// The integrate function returns the integral from x[0] to z.
	public double integrate(double z){
		Trace.Assert(z >= x[0] && z <= x[x.Length-1]);
		int iz=binsearch(x,z);
		double sum = 0;
		double dx, dxe;
		// Add up the integrals for all the intervals before the interval z lies in
		for(int i=0;i<iz;i++){
			dx = x[i+1]-x[i];
			dxe = -dx;
			sum += dx*(y[i]+dx*(b[i]/2+dx*(c[i]/3+dx*d[i]/4)))
			   + e[i]*dx*dx*dx*(dxe*dxe/3 + dx*dxe/2 + dx*dx/5);
		}
		// Add the remaining part of the integral (the remaining bit between the point z,
		// and the start of the interval that z lies in).
		dx = z-x[iz];
		dxe = x[iz]-x[iz+1];
		sum += dx*(y[iz]+dx*(b[iz]/2+dx*(c[iz]/3+dx*d[iz]/4)))
		    + e[iz]*dx*dx*dx*(dxe*dxe/3 + dx*dxe/2 + dx*dx/5);
	return sum;
	}

} // end class
