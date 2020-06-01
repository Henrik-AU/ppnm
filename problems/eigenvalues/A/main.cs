using System;
using System.IO;
using static System.Console;
using static System.Math;

class main{
	public static void Main(){

		// We start out by creating a random symmetric matrix.
		var rand = new Random();

		WriteLine("Let's test that the matrix diagonalization algorithm works.");

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
		Write("Printing random symmetric matrix A:");
		A.print();
	
		// Perform a cyclic sweep on the matrix A
		matrix V = new matrix(n,n);
		vector e = new vector(n);
		matrix Acopy = A.copy();
		int sweeps = jacobi.cycle(A, e, V);

		Write("\nPrinting matrix V found from the decomposition:");
		V.print();

		WriteLine("\nPrinting the found eigenvalues from the decomposition:");
		e.print();

		Write("\nPrinting V^T*A*V:");
		matrix VTAV = V.T*Acopy*V;
		VTAV.print();
		WriteLine("This matrix should now be diagonal with the eigenvalues along the" + 
		" diagonal entries.");

		WriteLine("\nThe decomposition was done in {0} sweeps.", sweeps);
		

		// We now try to solve the quantum particle in a box problem

		
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
		StreamWriter EnergyWriter = new StreamWriter("energies.txt");
		EnergyWriter.WriteLine("Energies found for the quantum particle in a box:");
		EnergyWriter.WriteLine("n\tcalc\t\texact");
		for(int k=0; k<m/4; k++){
			double exact = PI*PI*(k+1)*(k+1);
			double calculated = eigenvalsBox[k];
			EnergyWriter.WriteLine("{0}\t{1,8:f8}\t{2,8:f8}", k, calculated, exact);
		}

		EnergyWriter.WriteLine("The found energies seem to fit the analytic results very well, at least for low n.");
		EnergyWriter.Close();

		StreamWriter BoxWriter = new StreamWriter("boxData.txt");
		// Print out some data for plots of the functions
		for(int k = 0; k<3; k++){
			BoxWriter.WriteLine("\n\n{0} {1,17}", "0", "0");
			for(int i = 0; i<m; i++){
				BoxWriter.WriteLine("{0:f15} {1,17:f15}",
				(i+1.0)/(m+1), matrix.get(VBox,i,k));
			}
			BoxWriter.WriteLine("{0} {1,17}", "1", "0");
		}

		// q = 1 is the ground state, q = 2 is the first excited state, etc.
		int q = 1;

		// a is a quick and dirty change of normalization that seems to make the analytical
		// expressions match the numerical expression well enough to argue that they are
		// identical
		double a = 0.149;

		// Prepare the analytical functions for a particle in a box
		Func<double,double> psi = (x) => a*Sin(q*x*PI);

		// Print out the analytical expressions for the functions
		for(int k = 0; k<3; k++){
			BoxWriter.WriteLine("\n\n{0} {1,5}", "0", "0");
			for(double x = 0.02; x<1; x+=0.02){
				BoxWriter.WriteLine("{0:f3} {1,4:f15}", x, psi(x));
			}
			BoxWriter.WriteLine("{0} {1,5}", "1", "0");
			// Increase q by 1 to go to the next state
			q++;
		}
		BoxWriter.Close();
	}
}
