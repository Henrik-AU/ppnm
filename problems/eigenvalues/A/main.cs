using System;
using static System.Console;
using static System.Math;
using System.IO;

class main{
	public static void Main(){

		// We start out by creating a random symmetric matrix.
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

		StreamWriter BoxWriter = new StreamWriter("boxData.txt");
		
		// First we build the Hamiltonian, three point finite difference formula approximation
		int m = 90;
		double s = 1.0/(m+1);
		matrix H = new matrix(m,m);
		for(int i  = 0; i<m-1;i++){
			matrix.set(H,i,i,-2);
			matrix.set(H,i,i+1,1);
			matrix.set(H,i+1,i,1);
		}
		matrix.set(H,m-1,m-1,-2);
		matrix.scale(H,-1/(s*s));

		// Next we diagonalize it with out Jacobi routine,
		matrix VBox = new matrix(m,m);
		vector eigenvalsBox = new vector(m);
		int sweepsBox = jacobi.cycle(H,eigenvalsBox,VBox);
		Error.WriteLine("The Jacobi cycling for the particle in a box problem was completed" +
		" in {0} sweeps.", sweepsBox);

		// Let's check if the energies seem to be correct
		for(int k=0; k<m/4; k++){
			double exact = PI*PI*(k+1)*(k+1);
			double calculated = eigenvalsBox[k];
			BoxWriter.WriteLine("{0} {1,8:f8} {2,8:f8}", k, calculated, exact);
		}

		WriteLine();
		// Print out some data for plots of the functions
		for(int k = 0; k<3; k++){
			BoxWriter.WriteLine("\n\n{0} {1,17}", "0", "0");
			for(int i = 0; i<m; i++){
				BoxWriter.WriteLine("{0:f15} {1,17:f15}", (i+1.0)/(m+1), matrix.get(VBox,i,k));
			}
			BoxWriter.WriteLine("{0} {1,17}", "1", "0");
		}

		// Print out the analytical expressions for the functions
		
		int q = 1;		
		Func<double,double> psi = (x) => Sin(q*x*PI);

		for(int k = 0; k<3; k++){
			BoxWriter.WriteLine("\n\n{0} {1,5}", "0", "0");
			for(double x = 0.02; x<1; x+=0.02){
				BoxWriter.WriteLine("{0:f3} {1,4:f15}", x, psi(x));
			}
			q++;
			BoxWriter.WriteLine("{0} {1,5}", "1", "0");
		
		}

		BoxWriter.Close();

	}
}
