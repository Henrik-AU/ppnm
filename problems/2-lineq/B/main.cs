using System;
using static System.Console;

class mainB{
	static void Main(){

		var rand  = new Random();
	
		// We can pull out new random numbers between 0 and 1 with rand.NextDouble() and
		// stuff it into a matrix
		int n = 4;
		matrix A = new matrix(n, n);
		for(int i=0; i<n; i++){
			for(int j=0; j<n; j++){
				A[i,j] = 2 - 4*rand.NextDouble();
			}
		}
		WriteLine("Set up a random matrix, and try to find the inverse matrix via QR" +
		" decomposition.\n");
		WriteLine("Printing random matrix A:");
		A.print();

		// Factorize the matrix into two matrices Q and R, and calculate the inverse matrix B
		var qrGS = new qrDecompositionGS(A);
		matrix B = qrGS.inverse();
		matrix Q = qrGS.Q;
		matrix R = qrGS.R;

		WriteLine("\nPrinting matrix R:");
		R.print();

		WriteLine("\nPrinting matrix Q:");
		Q.print();
	
		WriteLine("\nPrinting matrix B (inverse to A):");
		B.print();

		// Let's check if B is actually the inverse by calculating A*B which then should
		// give the identity matrix
		matrix AB = A*B;

		WriteLine("\nPrinting matrix A*B:");
		AB.print();

		matrix I = new matrix(n, n);
		I.set_identity();
		bool approx = AB.approx(I);
		if(approx == true){
			WriteLine("A*B is approximately equal to the identity matrix.");
		}else{
			WriteLine("A*B is not approximately equal to the identity matrix.");
		}
	}
}
