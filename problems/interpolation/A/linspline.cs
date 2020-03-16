using System.Diagnostics;

public class linspline{

	double[] x, y, p; 

	public static int binsearch(vector x, double z)
		{/* locates the interval for z by bisection */ 
		int i=0, j=x.size-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
			}
		return i;
	}


	public linspline(double[] xs, double[] ys){
		int n = xs.Length;
		// Check if the dimension of the x and y arrays are equal - otherwise display
		// an error
		Trace.Assert(ys.Length == n,"The dimension of the x and y arrays are not equal.");
		
		x = new double[n];
		y = new double[n];
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
		

	}

	public double eval(double z){
		Trace.Assert(z >= x[0] && z<=x[x.Length-1], "The z-value is outside the valid x region.");
		int i = binsearch(x, z);
		double dx = z - x[i];
		return y[i] + p[i]*dx;
	}

	public double integrate(double z){
		Trace.Assert(z >= x[0] && z<=x[x.Length-1], "The z-value is outside the valid x region.");
		int i = binsearch(x, z);
		double sum;
		double dx;
		// We integrate S_i between each set of points individually and add the results
		for(int k=0; k<i; k++){
			dx = x[k+1] - x[k];
			sum+= dx*(y[k] + p[k]*dx/2 - p[k]*dx[k]);
		}
		// At last we add the part from the interval which z lies in.
		dx = z - x[i];	
		sum+= dx*(y[i] + p[i]*dx/2 - p[i]*dx[i]);
		
		return sum;
	}

}
