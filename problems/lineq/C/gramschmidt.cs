using System.Diagnostics;

public class qrDecompositionGS{
	public matrix Q;
	public matrix R;

	public qrDecompositionGS(matrix A){
		// We create a copy of A and place it in Q. We then work directly on this in place
		int n = A.size1;
		int m = A.size2;		
		R = new matrix(m, m);
		Q = new matrix(n, m);
		Q = A.copy();
		for(int i=0; i<m; i++){
			R[i,i] = Q[i].norm();
			Q[i] = Q[i]/R[i,i];
			for(int j=i+1; j<m; j++){
				// % is a dot-product operator. The a.dot(b) method is only
				// implemented for vectors, but % also works with rows/columns from
				// matrices, as it is also implemented in the matrix class.
				R[i,j] = Q[i]%Q[j];
				Q[j] = Q[j] - Q[i]*R[i,j];
			}
		}
	}

	public vector solve(vector b){
		vector Rx = Q.transpose()*b;
		for(int i=Rx.size-1; i>=0; i--){
			double sum = 0;
			for(int k=i+1; k<Rx.size; k++){
				sum += R[i,k]*Rx[k];	
			}
			Rx[i] = (Rx[i] - sum)/R[i,i];
		}
		// Rx now just holds the x-vector
		return Rx;
	}

	public matrix inverse(){
		// The dimensions of Q is the same as for the original matrix A so we just use that
		int n = Q.size1;
		int m = Q.size2;
		// Important note: The matrix does not have to be square. The used routine can also
		// find left/right inverses
		matrix Ainv = new matrix(m, n);		

		// We create a vector that we can use as the unit vector. My initial idea was to
		// create an identity matrix and then just use each column one at a time, but that
		// is not efficient since it allocates a whole matrix to memory, instead of just
		// working with one single vector and then updating it smartly throughout the loop.
		vector e = new vector(n);

		for(int i=0; i<n; i++){
			e[i] = 1;
			Ainv[i] = solve(e);
			e[i] = 0;
		}
		return Ainv;
	}



}
