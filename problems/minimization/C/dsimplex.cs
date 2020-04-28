using System;
using static System.Console;
using System.Diagnostics;
using System.Collections.Generic;

public partial class minimization{
	public static vector dsimplex(Func<vector, double> f, List<vector> points,
	ref int nsteps, double eps = 1e-3){

	int n = points.Count;

	// There should be n+1 vectors for a function taking n parameters
	Trace.Assert(n == points[0].size + 1, "Incorrect amount of starting points");

	int minIndex;
	int maxIndex;

	while(true){
		nsteps++;

		// Calculate the function values for the points
		vector fValues = new vector(n);
		for(int i=0; i<n; i++){
			fValues[i] = f(points[i]);
			Error.Write("{0}\t", fValues[i]);
		}

		// Find the index of the vector giving the highest function value.
		maxIndex = findMaxIndex(fValues);
		
		// Likewise we find the lowest point
		minIndex = findMinIndex(fValues);

		// Calculate the centroid using all points except for the highest point
		vector centroid = calcCentroid(points, maxIndex);
		Error.WriteLine("\n{0}\t{1}", centroid[0], centroid[1]);
		
		// Once the simplex becomes small enough we assume that we have converged to a
		// minimum. We do not know if the points come ordered such that this is actually
		// the distance along the "circumference" of the simplex, but it should get smaller
		// and smaller during the runs, and thus converge at some point, so it should work.
		double distances = 0;
		for(int i=0; i<n-1; i++){
			distances += (points[i]-points[i+1]).norm();
		}
		if(distances < eps){
			Error.WriteLine("Steps: {0}", nsteps);
			Error.WriteLine("Distances: {0}", distances);
			break;
		}
		
		
		// Now it is time to attempt to take a step
		vector highPoint = points[maxIndex];
		vector lowPoint = points[minIndex];
		double fHigh = fValues[maxIndex];
		double fLow = fValues[minIndex];

		// First we try reflection
		//vector reflected = new vector(n-1);
		vector reflected = centroid + (centroid - highPoint);
		double fRef = f(reflected); 

		if(fRef < fLow){
			// Try expansion
			vector expanded = centroid + 2*(centroid - highPoint);
			double fExp = f(expanded);
			if(fExp < fRef){
				// Accept expansion instead of highest point
				points[maxIndex] = expanded;
				Error.WriteLine("Expanding");
			}else{
				// Accept reflection instead of highest point
				points[maxIndex] = reflected;
				Error.WriteLine("Reflected 1");

			}			
		}else{
			// If the reflected point gives a lower value than the previous highest point
			// then we accept it as a small improvement
			if(fRef < fHigh){
				// Accept reflection instead of highest point
				points[maxIndex] = reflected;
				Error.WriteLine("Reflected 2");
			}else{
				// Try contraction - I have found the 1/1.75 factor to be effective
				// 1/2 did not work at all - the simplex shrunk too quickly which
				// gave issues with the simplex getting stuck in a wrong spot
				vector contracted = centroid + (centroid - highPoint)/1.75;
				double fCon = f(contracted);
				if(fCon < fHigh){
					// Accept contraction instead of highest point
					points[maxIndex] = contracted;
					Error.WriteLine("Contracted");
				}else{
					Error.WriteLine("Reducing");
					// Reduce the simplex towards the lowest point
					for(int i=0; i<n; i++){
						if(i!=minIndex){
							points[i] = (points[i] + points[minIndex])/2;
						}
					}
				}
			}

		}

	}

	// Return the vector that give the current lowest point
	vector result = points[minIndex];
	return result;
	}

	public static int findMaxIndex(vector fValues){
		int n = fValues.size;
		int maxIndex = 0;
		for(int i=1; i<n; i++){
			if(fValues[i] > fValues[maxIndex]){
				maxIndex = i;	
			}
		}
		return maxIndex;
	}

	public static int findMinIndex(vector fValues){
		int n = fValues.size;
		int minIndex = 0;
		for(int i=1; i<n; i++){
			if(fValues[i] < fValues[minIndex]){
				minIndex = i;	
			}
		}
		return minIndex;
	}

	public static vector calcCentroid(List<vector> points, int maxIndex){
		int n = points.Count;
		vector centroid = new vector(n-1);
		for(int i=0; i<n; i++){
			if(i!=maxIndex){
				centroid += points[i]; 
			}
		}
		centroid = centroid/(n-1);

		return centroid;
	}
}
