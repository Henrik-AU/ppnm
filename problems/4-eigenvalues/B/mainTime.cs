using System;
using System.IO;
using static System.Console;
using static System.Math;

class main{
	public static int Main(string[] args){
		// Require exactly 1 input parameter
		if (args.Length != 1) return 1;

		// The input parameter is the dimension of the matrix
		int n = int.Parse(args[0]);

		// We start out by creating a random symmetric matrix.
		var rand = new Random();

		// We can pull out new random numbers between 0 and 1 with rand.NextDouble() and
		// stuff it into a matrix
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
		jacobi.cycle(A, e, V);

		return 0;	
	}		
}
