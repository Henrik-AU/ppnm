using System;
using System.IO;
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
		
	// Calculate the initial function values for the points
	vector fValues = new vector(n);
	for(int i=0; i<n; i++){
		fValues[i] = f(points[i]);
	}

	StreamWriter printAni = new StreamWriter("animation.txt");

	while(true){
		nsteps++;

		// Print out the vectors for a fancy animation plot
		if(nsteps < 120){
			for(int i=0; i<n; i++){
				printAni.WriteLine("{0}\t{1}\t0", points[i][0], points[i][1]);
			}
			printAni.WriteLine("{0}\t{1}\t0", points[0][0], points[0][1]);
			printAni.Write("\n\n");		
		}	

		// Find the index of the vector giving the highest function value.
		maxIndex = findMaxIndex(fValues);
		
		// Likewise we find the lowest point
		minIndex = findMinIndex(fValues);

		// Calculate the centroid using all points except for the highest point
		vector centroid = calcCentroid(points, maxIndex);
		
		// Once the simplex becomes small enough we assume that we have converged to a
		// minimum. We do not know if the points come ordered such that this is actually
		// the distance along the "circumference" of the simplex, but it should get smaller
		// and smaller during the runs, and thus converge at some point, so it should work.
		double distance = 0;
		for(int i=0; i<n-1; i++){
			distance += (points[i]-points[i+1]).norm();
		}

		// Divide the circumference distance with n-1, which is the amount of arguments the
		// objective function takes. Thus the divergence criterium should be somewhat the 
		// same no matter the dimension of the objective function.
		distance = distance/(n-1);
		if(distance < eps){
			Error.WriteLine("Steps: {0}\n\n", nsteps);
			break;
		}
		
		
		// Now it is time to attempt to take a step
		// Vector highPoint and lowPoint introduced only for readability of the code.
		// For better optimization they can be omitted and points[maxIndex] or
		// points[minIndex] used directly instead.
		vector highPoint = points[maxIndex];
		vector lowPoint = points[minIndex];
		double fHigh = fValues[maxIndex];
		double fLow = fValues[minIndex];

		// First we try reflection
		vector reflected = centroid + (centroid - highPoint);
		double fRef = f(reflected); 

		if(fRef < fLow){
			// Try expansion
			vector expanded = centroid + 2*(centroid - highPoint);
			double fExp = f(expanded);
			if(fExp < fRef){
				// Accept expansion instead of highest point
				points[maxIndex] = expanded;
				fValues[maxIndex] = fExp;
				Error.WriteLine("Expanding");
			}else{
				// Accept reflection instead of highest point
				points[maxIndex] = reflected;
				fValues[maxIndex] = fRef;
				Error.WriteLine("Reflected - better than lowest value");

			}			
		}else{
			// If the reflected point gives a lower value than the previous highest point
			// then we accept it as a small improvement
			if(fRef < fHigh){
				// Accept reflection instead of highest point
				points[maxIndex] = reflected;
				fValues[maxIndex] = fRef;
				Error.WriteLine("Reflected - better than highest value");
			}else{
				// Try contraction
				vector contracted = centroid + (highPoint - centroid)/2;
				double fCon = f(contracted);
				
				if(fCon < fHigh){
					// Accept contraction instead of highest point
					points[maxIndex] = contracted;
					fValues[maxIndex] = fCon;
					Error.WriteLine("Contracted");
				}else{
					Error.WriteLine("Reduced");
					// Reduce the simplex towards the lowest point
					for(int i=0; i<n; i++){
						if(i!=minIndex){
							points[i] = (points[i] + points[minIndex])/2;
							// Calculate new function values for the
							// moved points
							fValues[i] = f(points[i]);
						}
					}
				}
			}

		}

	}
	
	printAni.Close();

	// Return the vector that give the lowest point
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
