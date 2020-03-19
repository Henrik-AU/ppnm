public class qrDecompositionGS{
	public matrix Q;
	public matrix R;

	public qrDecompositionGS(matrix A){
		// We create a copy of A to work with
		matrix Acopy = A.copy();
		int n = A.size1;
		int m = A.size2;		
		R = new matrix(m, m);
		Q = new matrix(n, m);
		for(int i=0; i<m; i++){
			R[i,i] = Acopy[i].norm();
			Q[i] = Acopy[i]/R[i,i];
			for(int j=i+1; j<m; j++){
				// % is a dot-product operator. The a.dot(b) method is only
				// implemented for vectors, but % also works with rows/columns from
				// matrices, as it is also implemented in the matrix class.
				R[i,j] = Q[i]%Acopy[j];
				Acopy[j] = Acopy[j] - Q[i]*R[i,j];
			}
		}
	}
}
