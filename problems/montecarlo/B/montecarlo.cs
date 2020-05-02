using System;
using static System.Math;

public class montecarlo{

	public static Random rand = new Random();
	
	public static vector plainmc(Func<vector, double> f, vector a, vector b, int N){

		int dim = a.size;

		// Calculate the integration volume in a loop (viable for all dimensions)
		double volume = 1;
		for(int i=0; i<dim; i++){
			volume *= b[i]-a[i];
		}

		// Calculate the sum in N random points and sum the values, and the sum of the
		// squares of the values (which will be needed for the error estimates)
		double sum = 0;
		double sumSquare = 0;
		vector x = new vector(dim);
		for(int i=0; i<N; i++){
			double fx = f(randomPoint(a,b,x,dim));
			sum += fx;
			sumSquare += fx*fx;
		}

		// Calculate the integral estimate
		double mean = sum/N;
		double estimate = mean*volume;

		// Estimate the error
		double sigma = Sqrt(sumSquare/N - mean*mean);
		double error = volume*sigma/Sqrt(N);

	   	return new vector(estimate, error);
	}


	public static vector randomPoint(vector a, vector b, vector x, int dim){
		// Generate a pseudo random point that lies within our volume
		for(int i=0; i<dim; i++){
			x[i] = a[i] + rand.NextDouble()*(b[i] - a[i]);
		}
		return x;
	}

}
