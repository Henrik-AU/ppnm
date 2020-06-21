using System;
using System.IO;
using static System.Console;
using static System.Math;

class mainCTest{
	public static void Main(){

		WriteLine();
		WriteLine("Create a matrix and diagonalize it via the cyclic, value-by-value and" +
		" classic method.");

		var rand = new Random();
		int n = 5;

		// Create a random matrix
		matrix A = new matrix(n,n);
		for(int i=0; i<n; i++){
			A[i,i] = 2 - 4*rand.NextDouble();
			for(int j=i+1; j<n; j++){
				A[i,j] = 2 - 3.32*rand.NextDouble();
				A[j,i] = A[i,j];
			}
		}
		// Print the random matrix.
		WriteLine("\nSetting up a random symmetric matrix:");
		A.print();
		WriteLine();

		// Diagonalize it via the three different methods
		matrix Acopy = A.copy();
		matrix AcopyII = A.copy();
	
		matrix V = new matrix(n,n);
		vector e = new vector(n);
		vector eRot = new vector(n);
		vector eClassic = new vector(n);
		int sweeps = jacobi.cycle(A, e, V);
		int entries = (n*n - n)/2;
		int rotations = jacobi.findEigenvalue(Acopy, eRot, V, n, true);
		int rotationsClassic = jacobi.classic(AcopyII, eClassic, V);
		WriteLine("The cyclic method used {0} operations", sweeps*entries);
		WriteLine("The value-by-value method used {0} operations", rotations);
		WriteLine("The classic method used {0} operations", rotationsClassic);

		WriteLine("\nAfter cyclic diagonalization the matrix looks like:");
		A.print();
		WriteLine("\nAfter value-by-value diagonalization the matrix looks like:");
		Acopy.print();
		WriteLine("\nAfter classic Jacobi diagonalization the matrix looks like:");
		AcopyII.print();
		
		Write("\n\n");
		WriteLine("The found eigenvalues are:");
		WriteLine("E(cyclic)\t\tE(val_by_val)\t\tE(classic)");	
		for(int j=0; j<n; j++){
			WriteLine("{0}\t{1}\t{2}", e[j], eRot[j], eClassic[j]);
		}
		
	}		
}
