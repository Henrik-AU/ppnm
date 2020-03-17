using System.Diagnostics;

public class quadspline{

	double[] x, y, b, c, p; 

	public static int binsearch(double[] x, double z)
		{/* locates the interval for z by bisection */ 
		int i=0, j=x.Length-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
			}
		return i;
	}


	public quadspline(double[] xs, double[] ys){
		int n = xs.Length;
		// Check if the dimension of the x and y arrays are equal - otherwise display
		// an error
		Trace.Assert(ys.Length == n,"The dimension of the x and y arrays are not equal.");
		
		x = new double[n];
		y = new double[n];
		b = new double[n-1];
		c = new double[n-1];
		p = new double[n-1];
		var dx = new double[n-1];

		for(int i=0; i<n; i++){
			x[i] = xs[i];
			y[i] = ys[i];
		}
	
		for(int i=0; i<n-1; i++){
			dx[i] = x[i+1] - x[i];
			Trace.Assert(dx[i] > 0, "The x-array is not ordered from lowest to highest.");
		}

		for(int i=0; i<n-1; i++){
			p[i] = (y[i+1] - y[i])/dx[i];
		}

		// Forward recursion
		c[0] = 0;
		for(int i=0; i<n-2; i++){
			c[i+1] = (p[i+1] - p[i] - c[i]*dx[i])/dx[i+1];
		}
		// Backward recursion starting from 1/2 * c[n-2]
		c[n-2] = c[n-2]/2;
		for(int i=n-3; i>=0; i--){
			c[i] = (p[i+1] - p[i] - c[i+1]*dx[i+1])/dx[i];
		}

		// Calculation of the b-coefficients
		for(int i=0; i<n-1; i++){
			b[i] = p[i] - c[i]*dx[i];
		}

	}

	public double eval(double z){
		Trace.Assert(z >= x[0] && z<=x[x.Length-1], "The z-value is outside the valid x region.");
		int i = binsearch(x, z);
		return y[i] + b[i]*(z - x[i]) + c[i]*(z - x[i])*(z - x[i]);
	}

	public double integrate(double z){
		Trace.Assert(z >= x[0] && z<=x[x.Length-1], "The z-value is outside the valid x region.");
		int i = binsearch(x, z);
		double sum = 0;
		// We integrate S_i between each set of points individually and add the results
		for(int k=0; k<i; k++){
			double dx = x[k+1] - x[k];
			sum+= y[k]*dx + b[k]*dx*dx/2 + c[k]*dx*dx*dx/3;
		}
		// At last we add the part from the interval which z lies in.
		double dxi = z - x[i];
		sum+= y[i]*dxi + b[i]*dxi*dxi/2 + c[i]*dxi*dxi*dxi/3;
		
		return sum;
	}


	public double deriv(double z){
		Trace.Assert(z >= x[0] && z<=x[x.Length-1], "The z-value is outside the valid x region.");
		int i = binsearch(x, z);
		double dx = z - x[i];
		double deriv = b[i] + 2*c[i]*dx;
		return deriv;
	}

}
