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
		// Rx[n-1] = b[n-1]/R[n-1,n-1];
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




}
