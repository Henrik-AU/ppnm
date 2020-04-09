using System;
using static System.Console;
using static System.Math;

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
		

		// We now try to solve the quantum particle in a box problem
		// First we build the Hamiltonian, three point finite difference formula approximation
		WriteLine("\n\nAttempt to solve the quantum particle in a box problem:\n");
		int m = 80;
		double s = 1.0/(m+1);
		matrix H = new matrix(m,m);
		for(int i  = 0; i<m-1;i++){
			matrix.set(H,i,i,-2);
			matrix.set(H,i,i+1,1);
			matrix.set(H,i+1,i,1);
		}
		matrix.set(H,m-1,m-1,-2);
		matrix.scale(H,-1/(s*s));

		// Next we diagonalize it with out Jabobi routine,
		matrix VBox = new matrix(m,m);
		vector eigenvalsBox = new vector(m);
		int sweepsBox = jacobi.cycle(H,eigenvalsBox,VBox);

		// Let's check if the energies are correct
		WriteLine("n\t calculated E\t exact E");
		for(int k=0; k<m/3; k++){
			double exact = PI*PI*(k+1)*(k+1);
			double calculated = eigenvalsBox[k];
			WriteLine("{0}\t{1,8:f8}\t{2,8:f8}", k, calculated, exact);
		}

	}
}
