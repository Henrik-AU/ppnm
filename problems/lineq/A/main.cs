using System;
using static System.Console;

class main{
	static void Main(){

	// We start out by creating a random matrix.
	var rand = new Random();

	// We can pull out new random numbers between 0 and 1 with rand.NextDouble() and stuff it
	// into a matrix
	int n = 5;
	int m = 4;
	matrix A = new matrix(n, m);
	for(int i=0; i<n; i++){
		for(int j=0; j<m; j++){
			A[i,j] = 2 - 4*rand.NextDouble();
		}
	}

	// Print A
	WriteLine("Printing matrix A:");
	A.print();

	// We can now try to factorize the matrix A into the two matríces Q and R
	var qrGS = new qrDecompositionGS(A);
	matrix Q = qrGS.Q;
	matrix R = qrGS.R;

	// Let's check that R is upper triangular
	WriteLine("\nPrinting matrix R:");
	R.print();

	
	WriteLine("\nPrinting matrix Q:");
	Q.print();

	// Checking that Q^T * Q = 1
	WriteLine("\nPrinting (Q^T)*Q:");
	matrix QTQ = Q.transpose()*Q;
	QTQ.print();

	// Let's check if (Q^T)*Q is approximately the identity matrix
	matrix I = new matrix(m, m);
	I.set_identity();
	bool approx = QTQ.approx(I);
	if(approx == true){
		WriteLine("(Q^T)*Q is approximately equal to the identity matrix.");
	}else{
		WriteLine("(Q^T)*Q is not approximately equal to the identity matrix.\n");
	}
	

	// Checking that Q*R=A
	WriteLine("\nPrinting out Q*R:");
	matrix QR = Q*R;
	QR.print();

	// Let's check if Q*R is approximately equal to A
	approx = QR.approx(A);
	if(approx == true){
		WriteLine("QR is approximately equal to A.");
	}else{
		WriteLine("QR is not approximately equal to A.\n");
	}

	}
}
