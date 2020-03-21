using System;
using static System.Console;

class mainA{
	static void Main(){

		// Part 1 of exercise A
		WriteLine("-------Part 1 of exercise A:-------\n");
		// We start out by creating a random matrix.
		var rand = new Random();

		// We can pull out new random numbers between 0 and 1 with rand.NextDouble() and
		// stuff it into a matrix
		int nA = 5;
		int mA = 4;
		matrix A = new matrix(nA, mA);
		for(int i=0; i<nA; i++){
			for(int j=0; j<mA; j++){
				A[i,j] = 2 - 4*rand.NextDouble();
			}
		}

		// Print A
		WriteLine("Printing matrix A:");
		A.print();

		// We can now try to factorize the matrix A into the two matríces Q and R
		var qrGSA = new qrDecompositionGS(A);
		matrix Q = qrGSA.Q;
		matrix R = qrGSA.R;

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
		matrix I = new matrix(mA, mA);
		I.set_identity();
		bool approx = QTQ.approx(I);
		if(approx == true){
			WriteLine("(Q^T)*Q is approximately equal to the identity matrix.");
		}else{
			WriteLine("(Q^T)*Q is not approximately equal to the identity matrix.");
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
			WriteLine("QR is not approximately equal to A.");
		}



		// Part 2 of exercise A
		WriteLine("\n\n-------Part 2 of exercise A:-------\n");	

		// At first we create a random square matrix - let's call it C now to avoid confusion
		// with part 1 above
		WriteLine("Setting up system C*x = b.");
		int nC = 4;
		matrix C = new matrix(nC, nC);
		for(int i=0; i<nC; i++){
			for(int j=0; j<nC; j++){
				C[i,j] = 2 - 4*rand.NextDouble();
			}
		}
		WriteLine("Printing matrix C:");
		C.print();
	
		// Next we generate a vector b of a length corresponding to the dimension of the
		// square matrix
		vector b = new vector(nC);
		for(int i=0; i<nC; i++){
			b[i] = 3*rand.NextDouble() - 1.5;
		}
		WriteLine("\nPrinting out b:");
		b.print();

		// Once again we factorize our matrix into two matrices Q and R
		var qrGSC = new qrDecompositionGS(C);
		Q = qrGSC.Q;
		R = qrGSC.R;

		vector x = qrGSC.solve(b);
	
		WriteLine("\nSolving by QR decomposition and back-substitution.");
		WriteLine("\nPrinting vector C*x :");
		vector Cx = C*x;
		Cx.print();

		// Let's check if C*x is approximately equal to b
		approx = Cx.approx(b);
		if(approx == true){
			WriteLine("Cx is approximately equal to b. The system has been solved.");
		}else{
			WriteLine("Cx is not approximately equal to b. The system has not been solved.");
		}	

		WriteLine("\nThe obtained x-vector from the routine is:");
		x.print();

	}
}
