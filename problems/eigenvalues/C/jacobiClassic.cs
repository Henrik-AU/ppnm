using System;
using static System.Console;
using static System.Math;

public partial class jacobi{
	
	public static int classic(matrix A, vector e, matrix V){
		
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

	// Create an array with the index of the largest element in each row. Element i denotes
	// that the largest element in row i is in that given column
	int[] maxIndices = new int[n-1];

	for(int i=0; i<n-1; i++){
		int m = i+1;
		for(int k=i+2; k<n; k++){
			if(Abs(A[i,k]) > Abs(A[i,m])){
				m = k;
			}
		}
		maxIndices[i] = m;
	}

	do{
		
		changed = false;
		int p;
		int q;

		// Find the largest off-diagonal element. This is the one we want to zero.
		// Assume it's in the first row, then loop through to check if it's actually in a 
		// different row
		int m = 0;
		for(int k=1; k<n-1; k++){
			if(Abs(A[k,maxIndices[k]]) > Abs(A[m,maxIndices[m]])){
				m = k;
			}
		}

		// Convert to the notation used in the course
		p = m;
		q = maxIndices[m];

		double app = e[p];
		double aqq = e[q];
		double apq = A[p,q];
		// Calculate the angle phi that zeros the new apq element.
		double phi = Atan2(2*apq,aqq-app)/2;

		double c = Cos(phi);
		double s = Sin(phi);
		// Calculate the updated app and aqq values
		double appu = c*c*app - 2*s*c*apq + s*s*aqq;
		double aqqu = s*s*app + 2*s*c*apq + c*c*aqq;


		// If the app and/or aqq values have changed since the last run then set
		// changed to true and calculate the other matrix elements
		if(appu!=app || aqqu!=aqq){
			rotations++;
			changed = true;
			e[p] = appu;
			e[q] = aqqu;
			A[p,q] = 0.0;

			// We update the matrix elements via several for loops such
			// that we skip the elements already determined above
			for(int i = 0; i<p; i++){
				double aip = A[i,p];
				double aiq = A[i,q];
				A[i,p] = c*aip - s*aiq;
				A[i,q] = c*aiq + s*aip;
			}
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

		// Update the array of indices. First we find the maximum in row p and q anew
		m = p+1;
		for(int k=p+2; k<n; k++){
			if(Abs(A[p,k]) > Abs(A[p,m])){
				m = k;
			}
		}
		maxIndices[p] = m;

		// Update row q (but not if it is the bottom row of the matrix)
		if(q<n-1){
			m = q+1;
			for(int k=q+2; k<n; k++){
				if(Abs(A[q,k]) > Abs(A[q,m])){
					m = k;
				}
			}
			maxIndices[q] = m;
		}
		// Next we go through the other rows. Here only column p and q changes. If neither
		// of the two changed elements in the column is the largest, we have to recalculate
		// the maximum element in the row anew. If (i,p) or (i,q) is not in the upper half
		// triangle then we also recalculate.
		for(int i=0; i<n-1; i++){
			if(i!=p && i!=q){
				bool newMax = false;
				if(p > i && Abs(A[i,p]) > Abs(A[i,maxIndices[i]])){
					maxIndices[i] = p;
					newMax = true;
				}
				if(q > i && Abs(A[i,q]) > Abs(A[i,maxIndices[i]])){
					maxIndices[i] = q;
					newMax = true;
				}
				if(newMax == false){
					// Do a full recalculation
					m = i+1;
					for(int k=i+2; k<n; k++){
						if(Abs(A[i,k]) > Abs(A[i,m])){
							m = k;
						}
					}
					maxIndices[i] = m;
				}
			}
		}

	}while(changed);
	return rotations;

} // end of function
} // end of class
