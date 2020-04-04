using System;
using static System.Console;

class main{
	public static void Main(){

		// Create a random symmetric matrix

		// We start out by creating a random matrix.
		var rand = new Random();

		// We can pull out new random numbers between 0 and 1 with rand.NextDouble() and
		// stuff it into a matrix
		int n = 4;
		matrix A = new matrix(n,n);
		for(int i=0; i<n; i++){
			A[i,i] = 2 - 4*rand.NextDouble();
			for(int j=i+1; j<n; j++){
				A[i,j] = 2 - 4*rand.NextDouble();
				A[j,i] = A[i,j];
			}
		}
	
		// Perform a cyclic sweep on the matrix A
		matrix V = new matrix(n,n);
		vector e = new vector(n);
		matrix Acopy = A.copy();
		int sweeps = jacobi.cycle(A, e, V);
		Write("Printing matrix A:");
		A.print();

		Write("\nPrinting matrix V found from the decomposition:");
		V.print();

		WriteLine("\nPrinting the found eigenvalues from the decomposition:");
		e.print();

		Write("\nPrinting V^T*A*V:");
		matrix VTAV = V.T*Acopy*V;
		VTAV.print();

		Write("\nThe decomposition was done in {0} sweeps", sweeps);

	}
}
