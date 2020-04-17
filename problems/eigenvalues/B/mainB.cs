using System;
using System.IO;
using static System.Console;
using static System.Math;

class mainB{
	public static void Main(){

		// part 3
		var rand = new Random();
		StreamWriter partIIIWriter = new StreamWriter("outB3.txt");

		int dn = 4;
		
		for(int n = 40; n<101; n+=dn){
			matrix A = new matrix(n,n);
			for(int i=0; i<n; i++){
				A[i,i] = 2 - 4*rand.NextDouble();
				for(int j=i+1; j<n; j++){
					A[i,j] = 2 - 4.17*rand.NextDouble();
					A[j,i] = A[i,j];
				}
			}
		
			matrix Acopy = A.copy();
		
			// Calculate the amount of matrix elements above the diagonal of the matrix
			// (total amount of elements minus the diagonal elements and then divided by two)
			// This value is the amount of elements being cycled over in a Jacobi cycle
			int entries = (n*n-n)/2;
	
			// Perform a cyclic sweep on the matrix A
			matrix V = new matrix(n,n);
			vector e = new vector(n);
			int sweeps = jacobi.cycle(A, e, V);
		
			// Calculate the lowest eigenvalue for A
			int nEigVal = 1;
			vector eRot = new vector(n);
			int rotations = jacobi.findEigenvalue(Acopy, eRot, V, nEigVal);
			partIIIWriter.WriteLine("{0} {1} {2}", n, sweeps*entries, rotations);
		}

		partIIIWriter.Close();


		// part 4
		// We now want to compare the time it takes to fully diagonalize a matrix A using the two
		// different routines. We might as well just continue using the matrix A from part 3, which
		// has already been diagonalized by the normal jacobi routine. We therefore diagonalize
		// it with the eigenvalue-by-eigenvalue algorithm.

		StreamWriter partIVWriter = new StreamWriter("outB4.txt");

		for(int n = 40; n<101; n+=dn){
			// Create a random matrix
			matrix A = new matrix(n,n);
			for(int i=0; i<n; i++){
				A[i,i] = 2 - 4*rand.NextDouble();
				for(int j=i+1; j<n; j++){
					A[i,j] = 2 - 3.32*rand.NextDouble();
					A[j,i] = A[i,j];
				}
			}

			// Diagonalize it via the two different methods
			matrix Acopy = A.copy();

			matrix V = new matrix(n,n);
			vector e = new vector(n);
			vector eRot = new vector(n);
			int sweeps = jacobi.cycle(A, e, V);
			int entries = (n*n - n)/2;
			int rotations = jacobi.findEigenvalue(Acopy, eRot, V, n);

			partIVWriter.WriteLine("{0} {1} {2}", n, sweeps*entries, rotations);
		}

		partIVWriter.Close();

	}		
}
