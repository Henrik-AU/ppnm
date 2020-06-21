using System;
using System.IO;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

public partial class montecarlo{

	// A random number generator has already been created in the montecarlo.cs file, and is
	// also available for this file

	public static vector mcStrat(Func<vector, double> f, vector a, vector b, int N, double acc,
	double V = 1, vector prevStats = null, bool printP = false){

	// Lists for storing of the evaluated points and function values
	List<vector> xs = new List<vector>();
	List<double> fVals = new List<double>();

	// Attept to integrate via the plain Monte Carlo routine. We do it directly in this function,
	// such that we have access to the statistical data throughout the function.
	int dim = a.size;
	
	// Initial setup if it is the first function call
	if(prevStats == null){
		for(int i=0; i<dim; i++){
			V *= b[i]-a[i];
		}
		// prevStats contains the mean, the variance and the number of evaluated points
		// In the begining they are all zero, since there has been no previous function call
		prevStats = new vector(0, 0, 0);
	}

	// Do plain Monte Carlo with N points
	double sum = 0;
	double sumSquare = 0;
	for(int i=0; i<N; i++){
		// The randomPoint creates a brand new vector each time to avoid issues where the
		// elements in the lists change afterwards, because the vector they refer to is
		// changed
		vector x = randomPoint(a,b,dim);
		double fx = f(x);
		sum += fx;
		sumSquare += fx*fx;
		// Save the point and the function value in lists
		xs.Add(x);
		fVals.Add(fx);
	}
	
	// If wanted, print out the data points to an output file (for a plot for example)
	if(printP){
		StreamWriter pointWriter = new StreamWriter("stratPoints.txt", true);
		for(int i=0; i<N; i++){
			for(int j=0; j<dim-1; j++){
				pointWriter.Write("{0}\t", xs[i][0]);
			}
			// Write last point in vector - outside j-loop for formatting reasons (no tab
			// at the end then)
			pointWriter.Write("{0}", xs[i][dim-1]);
			pointWriter.WriteLine();
		}
		pointWriter.Close();
	}


	// Calculate the integral estimate via the new and old points in the subvolume

	// prevStats contains the mean, the variance and the amount of points evaluated in the
	// previous run, in that order.
	double mean = sum/N;
	double estimate = V*(mean*N + prevStats[0]*prevStats[2])/(N+prevStats[2]);

	// Estimate the total error via the new and old points in the subvolume
	double sigma = Sqrt(sumSquare/N - mean*mean);
	double error = V*Sqrt(sigma*N + prevStats[1]*prevStats[2])/(N+prevStats[2]);


	if(error < acc){
		// No need for stratification, we just return the result from the plain Monte carlo
		return new vector(estimate, error);
	}else{
		// Subdivide the interval and perform plain Monte Carlo on new intervals.

		// Dimension to divide along
		int iMax = 0;

		// Variables for variations. vMax < 0 ensures that there will always be one dimension
		// stored as the one with the highest variation, even if some are zero
		double vMax = -1;
		double v;
		vector prevStatsLeft = new vector(3);
		vector prevStatsRight = new vector(3);

		for(int i=0; i<dim; i++){
			// We split the evaluated points in two groups - the new left and right
			// intervals
			double sumLeft = 0;
			double sumRight = 0;
			double sumLeft2 = 0;
			double sumRight2 = 0;
			int NLeft = 0;
			int NRight = 0;

			for(int j = 0; j<N; j++){
				if(xs[j][i] < (a[i]+b[i])/2){
					sumLeft += fVals[j];
					sumLeft2 += fVals[j]*fVals[j];
					NLeft++;
				}else{
					sumRight += fVals[j];
					sumRight2 += fVals[j]*fVals[j];
					NRight++;
				}
			}

			// Calculate the mean in each half
			double meanLeft = sumLeft/NLeft;
			double meanRight = sumRight/NRight;

			// Find the variation in the mean in the two groups
			v = Abs(meanLeft - meanRight);
		
			// If this variation is larger than for any previous dividings, then we save
			// the data, before looping again
			if(v > vMax){
				iMax = i;
				vMax = v;
				// We want to save the variance also
				double sigmaLeft = Sqrt(sumLeft2/NLeft - meanLeft*meanLeft);
				double sigmaRight = Sqrt(sumRight2/NRight - meanRight*meanRight);

				prevStatsLeft = new vector(meanLeft, sigmaLeft, NLeft);
				prevStatsRight = new vector(meanRight, sigmaRight, NRight);
			}
		}

		// Once the dimension to subdivide is found we perform the subdivision and
		// recursively call this function again on the two subintervals.
		vector a2 = a.copy();
		vector b2 = b.copy();
		a2[iMax] = (a[iMax]+b[iMax])/2;
		b2[iMax] = (a[iMax]+b[iMax])/2;
		
		// New required accuracies
		acc = acc/Sqrt(2);

		// Do two recursive calls on the new intervals
		vector estimate1 = mcStrat(f, a, b2, N, acc, V/2, prevStatsLeft, printP:printP);
		vector estimate2 = mcStrat(f, a2, b, N, acc, V/2, prevStatsRight, printP:printP);
		
		double integralTotal = estimate1[0] + estimate2[0];
		double errorTotal = Sqrt(estimate1[1]*estimate1[1] + estimate2[1]*estimate2[1]);

		return new vector(integralTotal, errorTotal);
	}

	} // end mcStrat


	public static vector randomPoint(vector a, vector b, int dim){
		// Generate a pseudo random point that lies within our volume
		// We create a new vector to store the random point in
		vector x = new vector(dim);
		for(int i=0; i<dim; i++){
			x[i] = a[i] + rand.NextDouble()*(b[i] - a[i]);
		}
		return x;
	}


} // end class
