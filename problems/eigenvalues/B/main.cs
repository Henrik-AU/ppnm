using System;
using System.IO;
using static System.Console;
using static System.Math;

class mainB{
	public static void Main(){

		// part 3
		var rand = new Random();
		StreamWriter partIIIWriter = new StreamWriter("outB3.txt");

		int dn = 10;
		
		for(int n = 50; n<301; n+=dn){
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
			int rotations = jacobi.findEigenvalue(Acopy, eRot, V, nEigVal, true);
			partIIIWriter.WriteLine("{0} {1} {2}", n, sweeps*entries, rotations);
		}

		partIIIWriter.Close();


		// part 4
		StreamWriter partIVWriter = new StreamWriter("outB4.txt");

		dn = 5;

		for(int n = 50; n<101; n+=dn){
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
			int rotations = jacobi.findEigenvalue(Acopy, eRot, V, n, true);

			partIVWriter.WriteLine("{0} {1} {2}", n, sweeps*entries, rotations);
		}

		partIVWriter.Close();


		// Part 5
		// We want to see if we can retrieve the eigenvalues one by one starting with the 
		// highest eigenvalue
		
		// Create a matrix
		int m = 5;
		matrix B = new matrix(m,m);
		for(int i=0; i<m; i++){
			B[i,i] = 2 - 4*rand.NextDouble();
			for(int j=i+1; j<m; j++){
				B[i,j] = 2 - 3.32*rand.NextDouble();
				B[j,i] = B[i,j];
			}
		}
		matrix Bcopy = B.copy();
		
		// Find the eigenvalues with the cyclic routine (for comparison of eigenvalue values)
		// and find them one at a time starting from the highest
		matrix VB = new matrix(m, m);
		vector eB = new vector(m);
		vector eBRot = new vector(m);

		int sweepsB = jacobi.cycle(B, eB, VB);
		int entriesB = (m*m-m)/2;
		int rotationsB = jacobi.findEigenvalue(Bcopy, eBRot, VB, m, false);
		
		// Print out the results
		StreamWriter partVWriter = new StreamWriter("outB5.txt");
		
		partVWriter.WriteLine("The eigenvalues found by the cyclic method are:");
		for(int i=0; i<m; i++){
			partVWriter.WriteLine("{0}", eB[i]);
		}

		partVWriter.WriteLine("\nThe eigenvalues found one by one starting from the highest" +
		" is (in order):");
		for(int i=0; i<m; i++){
			partVWriter.WriteLine("{0}", eBRot[i]);
		}

		partVWriter.WriteLine("\nThe cyclic routine used {0} operations and the value by value" +
		" routine used {1}.", sweepsB*entriesB, rotationsB);
		
		partVWriter.Close();

	}		
}
