using System;
using static System.Console
using System.Diagnostics;


public class subspline{

	// The coefficients for the interpolated polynomial should be accessible for the whole class,
	// and so should the x and y values used for the spline. Let's keep them private to keep
	// the user from changing them by accident.
	private double[] x, y, yp, b, c, d;


	/*
	The constructor will do the splining.
	The data is assumed to be ordered from lowest x-values to highest. Thus xs[0] is the
	lowest x-value, and xs[xs.Length - 1] is the highest x-value. The subspline will only be
	defined for points in between these two values.
	*/
	public subspline(double[] xs, double[] ys, double[] yps){
		int n = xs.Length;
		Trace.Assert(ys.Length == n, "The amount of x and y values are not identical.");
		Trace.Assert(yps.Length == n, "The amount of x and y-prime values are not identical.");

		// Prepare the arrays that will hold the x, y and y-prime values in the class.
		x = new double[n];
		y = new double[n];
		yp = new double[n];

		// Store the xs, ys and yps values in the x, y and yp variables accesible to the whole
		// class
		for(int i=0; i<n; i++){
			x[i] = xs[i];
			y[i] = ys[i];
			yp[i] = yps[i];
		}


		double[] h = new double[n-1];
		double[] p = new double[n-1];	
		double[] q = new double[n-1];
		for(int i=0; i<n-1; i++){
			h[i] = h[i+1]-h[i];
			// The x-values should be ordered. If not, then this Trace.Assert statement
			// will spot it and throw an error.
			Trace.Assert(h[i] > 0);
		}

		for(int i=0;i<n-1;i++){
			p[i] = (y[i+1]-y[i])/h[i];
		 }

		for(int i=0;i<n-1;i++){
			q[i] = (yp[i+1]-yp[i])/h[i];
		}
		
		// Set up the arrays for the coefficients
		b = new double[n-1];
		c = new double[n-1];
		d = new double[n-1];

		// The b-coefficients are equal to the y-prime values
		for(int i=0; i<n; i++){
			b[i] = yp[i];
		}

		// Calculate the c-coefficients
		for(int i=0; i<n; i++){
			c[i] = 3*(p[i]-yp[i])/h[i] - q[i];
		}
		
		// Calculate the d-coefficients via the c-coefficients
		for(int i=0; i<n; i++){
			d[i] = (q[i] - 2*c[i]) / (3*h[i]);
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
			else{
				j=mid;
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
		return y[i]+dx*(b[i]+dx*(c[i]+dx*d[i]));
	}
	
	// Function that evaluates the derivative of the splined function at the point z
	// (z must lie in the splined domain).
	public double deriv(double z){
		Trace.Assert(z>=x[0] && z<=x[x.Length-1]);
		int i=binsearch(x,z);
		double dx=z-x[i];
		return b[i]+dx*(2*c[i]+dx*3*d[i]);
	}

	// Function that evaluates the integral of the splined function from x[0] to the point z
	// (z must lie in the splined domain).
	public double integrate(double z){
		Trace.Assert(z>=x[0] && z<=x[x.Length-1]);
		int iz=binsearch(x,z);
		double sum=0;
		double dx;
		// Add up the integrals for all the intervals before the interval z lies in
		for(int i=0;i<iz;i++){
			dx=x[i+1]-x[i];
			sum+=dx*(y[i]+dx*(b[i]/2+dx*(c[i]/3+dx*d[i]/4)));
		}
		// Add the remaining part of the integral (the remaining bit between the point z,
		// and the start of the interval that z lies in).
		dx=z-x[iz];
		sum+=dx*(y[iz]+dx*(b[iz]/2+dx*(c[iz]/3+dx*d[iz]/4)));
	return sum;
	}

} // end class
