using System;
using System.IO;
using static System.Console;
using static System.Math;

class mainC{
	public static void Main(string[] args){

		int n = int.Parse(args[0]);
		string method = args[1];


		var rand = new Random();

		// Create a random matrix
		matrix A = new matrix(n,n);
		for(int i=0; i<n; i++){
			A[i,i] = 2 - 4*rand.NextDouble();
			for(int j=i+1; j<n; j++){
				A[i,j] = 2 - 3.32*rand.NextDouble();
				A[j,i] = A[i,j];
			}
		}
		// Diagonalize it via one of the three different methods as specified by the input argument
	
		matrix V = new matrix(n,n);
		vector e = new vector(n);

		if(method == "cyclic"){
			int sweeps = jacobi.cycle(A, e, V);
			int entries = (n*n - n)/2;
			StreamWriter writeData = new StreamWriter("outCyclic.txt", append: true);
			writeData.WriteLine("{0} {1}", n, sweeps*entries);
			writeData.Close();	
		}
		
		if(method == "value"){
			int rotations = jacobi.findEigenvalue(A, e, V, n, true);
			StreamWriter writeData = new StreamWriter("outValue.txt", append: true);
			writeData.WriteLine("{0} {1}", n, rotations);
			writeData.Close();	
		}

		if(method == "classic"){
			int rotations = jacobi.classic(A, e, V);
			StreamWriter writeData = new StreamWriter("outClassic.txt", append: true);
			writeData.WriteLine("{0} {1}", n, rotations);
			writeData.Close();	
		}

	}		
}
