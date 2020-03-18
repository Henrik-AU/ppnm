using System.Diagnostics;

public class cubicspline{

	double[] x, y, b, c, d; 

	public static int binsearch(double[] x, double z)
		{/* locates the interval for z by bisection */ 
		int i=0, j=x.Length-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
			}
		return i;
	}


	public cubicspline(double[] xs, double[] ys){
		int n = xs.Length;
		// Check if the dimension of the x and y arrays are equal - otherwise display
		// an error
		Trace.Assert(ys.Length == n,"The dimension of the x and y arrays are not equal.");
		
		x = new double[n];
		y = new double[n];
		b = new double[n];
		c = new double[n-1];
		d = new double[n-1];
		var p = new double[n-1];
		var dx = new double[n-1];
		var D = new double[n];
		var Q = new double[n-1];
		var B = new double[n];

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

		// Calculation of the triagonal system
		// Values for intial components, as specified in the lecture PDF
		D[0] = 2;
		Q[0] = 1;
		B[0] = 3*p[0];
		D[n-1] = 2;
		B[n-1] = 3*p[n-2];
		for(int i=0; i<n-2; i++){
			Q[i+1] = dx[i]/dx[i+1];
			D[i+1] = 2*Q[i+1] + 2;
			B[i+1] = 3*(p[i]+p[i+1]*Q[i+1]);
		}		
		
		// Gaussian elemination
		for(int i=1; i<n; i++){
			D[i] -= Q[i-1]/D[i-1];		
			B[i] -= B[i-1]/D[i-1];
		}
		
		// Back-substitution
		b[n-1] = B[n-1]/D[n-1];
		for(int i=n-2; i>=0; i--){
			b[i] = (B[i] - Q[i]*b[i+1])/D[i];
		}

		// Solve for the c and d coefficients at last
		for(int i=0; i<n-1; i++){
			c[i] = (-2*b[i] - b[i+1] + 3*p[i])/dx[i];
			d[i] = (b[i] + b[i+1] - 2*p[i])/(dx[i]*dx[i]);
		}
	}

	public double eval(double z){
		Trace.Assert(z >= x[0] && z<=x[x.Length-1], "The z-value is outside the valid x region.");
		int i = binsearch(x, z);
		double dx = z - x[i];
		return y[i] + b[i]*dx + c[i]*dx*dx + d[i]*dx*dx*dx;
	}

	public double integrate(double z){
		Trace.Assert(z >= x[0] && z<=x[x.Length-1], "The z-value is outside the valid x region.");
		int i = binsearch(x, z);
		double sum = 0;
		// We integrate S_i between each set of points individually and add the results
		for(int k=0; k<i; k++){
			double dx = x[k+1] - x[k];
			sum+= dx*(y[k] + b[k]*dx/2 + c[k]*dx*dx/3 + c[k]*dx*dx*dx/4);
		}
		// At last we add the part from the interval which z lies in.
		double dxi = z - x[i];
		sum+= dxi*(y[i] + b[i]*dxi/2 + c[i]*dxi*dxi/3 + d[i]*dxi*dxi*dxi/4);
		
		return sum;
	}


	public double deriv(double z){
		Trace.Assert(z >= x[0] && z<=x[x.Length-1], "The z-value is outside the valid x region.");
		int i = binsearch(x, z);
		double dx = z - x[i];
		double deriv = b[i] + 2*c[i]*dx + 3*d[i]*dx*dx;
		return deriv;
	}

}
