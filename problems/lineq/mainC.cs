using System;
using static System.Console;

class main{
	static void Main(){

		WriteLine("Setting up a random system of linear equations, Ax=b:");

		// We construct a random square matrix
		var rand = new Random();
		int n = 4;
		matrix A = new matrix(n, n);
		for(int i=0; i<n; i++){
			for(int j=0; j<n; j++){
				A[i,j] = 2 - 4*rand.NextDouble();
			}
		}

		WriteLine("Printing random matrix A:");
		A.print();

		// Let's create a random vector b also, that we can use as the right side in Ax=b.
		vector b = new vector(n);
		for(int i=0; i<n; i++){
			b[i] = 3*rand.NextDouble() - 1.5;
		}
		WriteLine("\nPrinting random vector b:");
		b.print();

		// We can now attempt to solve for x using back substitution with the Givens rotations
		WriteLine("\nAttept to solve by the use of Givens rotations:");

		// Next we try to factorize A using the Givens algorithm - this gives us a matrix R,
		// which is "upper triangular", but with the Givens angles stored in the lower part
		var givens = new givens(A);
		matrix R = givens.R;
	
		WriteLine("\nPrinting calculated matrix R (with Givens angles in the left" +
		" triangular part):");
		R.print();

		vector x = givens.solve(b);
	
		WriteLine("\nThe calculated x-vector is:");
		x.print();

		WriteLine("\nLet's check if this x-vector actually solves the set of linear equations:");
		vector Ax = A*x;
	
		WriteLine("\nA*x gives:");
		Ax.print();

		bool approx = Ax.approx(b);
		if(approx == true){
			WriteLine("A*x is approximately equal to b. The system is solved.");
		}else{
			WriteLine("A*x is not approximately equal to b. The system is not solved.");
		}

	}
}
