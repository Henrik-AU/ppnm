using System;
using static System.Console;
using static System.Math;

public partial class jacobi{
	
	// The bool low dictates if we want to find the lowest eigenvalue first, or the highest
	public static int findEigenvalue(matrix A, vector e, matrix V, int eigValNum, bool low){
		
	bool changed;
	int rotations = 0;
	int n = A.size1;

	// Copy the diagonal of the A matrix into a vector
	for(int i = 0; i<n; i++){
		e[i] = A[i,i];
	}
	// Initialize the V matrix
	for(int i = 0; i<n; i++){
		V[i,i] = 1.0;
		for(int j=i+1; j<n; j++){
			V[i,j] = 0.0;
			V[j,i] = 0.0;
		}
	}

	// This is just a slight modification of the normal jacobi cycling routine. We only want to zero
	// the first eigValNum rows one at a time.
	
	
	int p;
	int q;

	for(p = 0; p<eigValNum; p++){
		do{
			changed = false;
			for(q=p+1; q<n; q++){
				double app = e[p];
				double aqq = e[q];
				double apq = A[p,q];
				// Calculate the angle phi that zeros the new apq element.
				// If we want the highest eigenvalue first (low == False)
				// then we have to rotate in the opposite direction
				double phi;
				if(low){
					phi = Atan2(2*apq,aqq-app)/2;
				}
				else{
					phi = Atan2(-2*apq,-aqq+app)/2;			
				}
				double c = Cos(phi);
				double s = Sin(phi);
				// Calculate the updated app and aqq values
				double appu = c*c*app - 2*s*c*apq + s*s*aqq;
				double aqqu = s*s*app + 2*s*c*apq + c*c*aqq;

				// If the app and aqq values have changed since the last run then set
				// changed to true and calculate the other matrix elements
				if(appu!=app || aqqu!=aqq){
					rotations++;
					changed = true;
					e[p] = appu;
					e[q] = aqqu;
					A[p,q] = 0.0;


					// We update the matrix elements via several for loops such
					// that we skip the elements already determined above

					// This for loop is not necessary for this algorithm, since
					// we have already zeroed the elements in the first row,
					// so these numbers stay appears to stay zeroed / at least
					// very close to zero (1e-8 or smaller when I checked).
					/*
					for(int i = 0; i<p; i++){
						double aip = A[i,p];
						double aiq = A[i,q];
						A[i,p] = c*aip - s*aiq;
						A[i,q] = c*aiq + s*aip;				
					}
					*/

					for(int i = p+1; i<q; i++){
						double api = A[p,i];
						double aiq = A[i,q];
						A[p,i] = c*api - s*aiq;
						A[i,q] = c*aiq + s*api;
					}
					for(int i = q+1; i<n; i++){
						double api = A[p,i];
						double aqi = A[q,i];
						A[p,i] = c*api - s*aqi;
						A[q,i] = c*aqi + s*api;
					}
					// At last we update the V matrix with the eigenvectors
					for(int i = 0; i<n; i++){
						double vip = V[i,p];
						double viq = V[i,q];
						V[i,p] = c*vip - s*viq;
						V[i,q] = c*viq + s*vip;
					}
				}
			} // end q for loop
		}while(changed);
	} // end p for loop
	return rotations;

} // end of function
} // end of class
