using static System.Math;

public class givens{
	public matrix R;
	public int n;
	public int m;

	public givens(matrix A){
		R = A.copy();
		n = A.size1;
		m = A.size2;	

		// Loop over the columns of the matrix
		for(int q=0; q<m; q++){
			// Loop over the rows of the matrix that lie under the diagonal
			for(int p=q+1; p<n; p++){
				double theta = Atan2(R[p,q], R[q,q]);
				// Loop to recalculate the relevant entries in the matrix
				for(int k=q; k<m; k++){
					double xq = R[q,k];
					double xp = R[p,k];
					R[q,k] = xq*Cos(theta) + xp*Sin(theta);
					R[p,k] = -xq*Sin(theta) + xp*Cos(theta);
				}
				R[p,q] = theta;
			}

		}
	}

	public vector solve(vector b){
		vector bcopy = b.copy();
		// Apply G (via many small subsequent rotations) to b to get Rx, since G = Q^T
		// so Gb = GAx = GQRx = Q^T*QRx = Rx
		for(int q=0; q<m; q++){
			for(int p=q+1; p<n; p++){
				double theta = R[p,q];
				double bq = bcopy[q];
				double bp = bcopy[p];
				bcopy[q] = bq*Cos(theta) + bp*Sin(theta);
				bcopy[p] = -bq*Sin(theta) + bp*Cos(theta);
			}
		}
		// bcopy now holds R*x.

		// Solve for x via back substitution,
		// R contains both the upper triagonal matrix known from QR factorization
		// and the Givens angle in the lower triagonal matrix. Here we use the upper
		// triagonal part for the back substitution.
		for(int i=bcopy.size-1; i>=0; i--){
			double sum = 0;
			for(int k=i+1; k<bcopy.size; k++){
				sum += R[i,k]*bcopy[k];	
			}
			bcopy[i] = (bcopy[i] - sum)/R[i,i];
		}
		// bcopy now just holds the x-vector
		return bcopy;	
	}
}
