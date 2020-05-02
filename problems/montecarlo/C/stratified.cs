using System;
using static System.Math;


public partial class montecarlo{
	public static vector mcStrat(Func<vector, double> f, vector a, vector b, int N, double acc){

	// Attept to integrate via the plain montecarlo routine
	vector estimate = montecarlo.plainmc(f, a, b, N);

	if(estimate[1] < acc){
		// No need for stratification, we just return the result from the plain Monte carlo
		return estimate;
	}else{
		// Subdivide the interval and perform plain Monte Carlo on new intervals
		int n = a.size;
		// Subinterval to divide along
		int j = 0;
		for(int i=0; i<n; i++){
			// To subdivide we find the new upper corner - it's the same as b, but the
			// specific dimension is reduced to half the original value
			vector c = a.copy();
			vector d = b.copy();
			c[i] = (a[i]+b[i])/2;
			d[i] = (a[i]+b[i])/2;

			// If everything is uncorrelated I believe the variance is linear 
			// ... How do we estimate sub-variances?

			// This should update j, the dimension to divide along
		}

		// Once the dimension to subdivide is found we perform the subdivision and
		// recursively call this function again on the two subintervals.
		vector a2 = a.copy;
		vector b2 = b.copy;
		a2[j] = (a[j]+b[j])/2;
		b2[j] = (a[j]+b[j])/2;
		
		// New required accuracies formula? (like o4a algorithm?)
		acc = acc/Sqrt(2);

		vector estimate1 = mcStrat(f, a, b2, N, acc);
		vector estimate2 = mcStrat(f, a, b2, N, acc);
		
		// We should keep the already found points somehow, not do entire recalculations

		//double averageTotal = ...
		//double errorTotal = ...

		return new vector(averageTotal, errorTotal);
	}


	} // end mcStrat
} // end class
