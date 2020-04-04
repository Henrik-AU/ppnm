using System;
using static System.Math;

public class jacobi{

	public static int cycle(matrix A, vector e, matrix V){
		
	bool changed;
	int sweeps = 0;
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

	do{
		changed = false;
		sweeps++;
		int p;
		int q;

		for(p=0; p<n; p++){
			for(q=p+1; q<n; q++){
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

				// If the app and aqq values have changed since the last run then set
				// changed to true and calculate the other matrix elements
				if(appu!=app || aqqu!=aqq){
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
			}
		}

	}while(changed);
	return sweeps;

} // end of function
} // end of class
