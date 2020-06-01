using System;
using System.IO;
using static System.Math;
using static System.Console;
using System.Collections.Generic;

class main{
	static void Main(string[] args){
		// Accuracy is for the root finding algorithm	
		double accuracy = 1e-5;
		double rmax = 8;
		bool printAnalytic = false;
		bool precise = false;
		
		// Allow the rmax value to be adjusted via input from the terminal
		if(args.Length>0){
			rmax=double.Parse(args[0]);
		}
		// If specified, switch the boolean to make the program write out the analytic
		// solution also.
		if(args.Length>1){
			if(args[1] == "print"){
				printAnalytic = true;
			}
		}
		/*
		If specified, use the more precise asymptotic requirement at rmax.
		It is not necessary to be able to specify this at the same time as print
		(analytic solution), since print only has to be done once.
		*/
		if(args.Length>1){
			if(args[1] == "precise"){
				precise = true;
			}
		}

		
		// Auxiliary function that we want to find roots for
		Func<vector, vector> M = delegate(vector e){
			// e is the energy
			// Solve the Schrödinger equation for hydrogen and find the function value
			// at the maximum radius.
			double frmax = hydrogen.solve(e[0], rmax);
			if(precise){
				double k = Sqrt(-2*e[0]);
				frmax = frmax - rmax*Exp(-k*rmax);
			}
			return new vector(frmax);
		};

		// The energy should be close to -1/2, so we start somewhere around that area
		var rand = new Random();
		vector start = new vector(-1-0.2*rand.NextDouble());	

		// Find the root for the auxiliary function
		vector root = roots.newton(M, start, accuracy);
		double energy = root[0];
		
		StreamWriter WriteRoot = new StreamWriter("outRootTextLog.txt", true);
		WriteRoot.WriteLine("Log entry for root finding for the rmax = {0}, print = {1}" + 
		", precise = {2} inputs to the Main function.", rmax, printAnalytic, precise);
		WriteRoot.WriteLine("Trying to find a root for the auxiliary function, M:");
		WriteRoot.WriteLine("Starting the search from energy: \t {0}.", start[0]);
		WriteRoot.WriteLine("Used rmax: \t\t\t\t {0}", rmax);
		WriteRoot.WriteLine("The analytic root is at energy: \t -1/2.");
		WriteRoot.WriteLine("A root has been found at energy: \t {0}", energy);
		WriteRoot.WriteLine("The deviation from the analytic root is: {0}", -0.5-energy);
		WriteRoot.WriteLine("Convergence criterium: ||M(root)|| < {0}\n\n", accuracy);

		WriteRoot.Close();

		// Write out the found root for a plot also, along with the used rmax
		if(precise){		
			StreamWriter WriteRootDataP = new StreamWriter("outRootDataP.txt", true);
			WriteRootDataP.WriteLine("{0}\t{1}", rmax, energy);
			WriteRootDataP.Close();
		}else{
			StreamWriter WriteRootData = new StreamWriter("outRootData.txt", true);
			WriteRootData.WriteLine("{0}\t{1}", rmax, energy);
			WriteRootData.Close();
		}

		// For simplicity we just solve the ODE again with the found energy to retrieve
		// the solution. Thus we avoid passing lots of lists back and forth, most of them
		// will just be constantly overwritten anyway until the end.

		// Create some lists to save the path of the found solution
		List<double> rs = new List<double>();
		List<vector> fs = new List<vector>();

		// Starting conditions and parameters
		double r0 = 1e-4;
		vector f0 = new vector(r0 - r0*r0, 1- 2*r0);
		double acc = 1e-3;
		double eps = 0;
		double h = 1e-3;
	
		ode.rk45(hydrogen.diffeq(energy), r0, f0, rmax, h, acc, eps, xlist:rs, ylist:fs);

		// Inspired by Rasmus Berg Jensen, let's spline the data points to get a nice curve.
		// This is a good strategy if the equations are expensive to solve, since you only
		// have few data points then - in the present case it is just easy, practical, and
		// serves as a good way to practice the curriculum.

		vector rPoints = new vector(rs.Count);
		vector fPoints = new vector(fs.Count);
		for(int i=0; i<rs.Count; i++){
			rPoints[i] = rs[i];
			fPoints[i] = fs[i][0];
		}

		var cspline = new cubicspline(rPoints, fPoints);		
		double rStart = rPoints[0];
		double rEnd = rPoints[rPoints.size - 1];

		// Print the data
		if(precise){
			StreamWriter writeDataP = new StreamWriter("outP.txt", true);
			for(double r=rStart; r<rEnd; r+=0.02){
				writeDataP.WriteLine("{0,16:f15} \t {1,16:f15}", r, cspline.eval(r));
			}
			// Do the last point outside the loop to avoid precision erros since
			// we loop over doubles
			writeDataP.WriteLine("{0,16:f15} \t {1,16:f15}", rEnd, cspline.eval(rEnd));
			writeDataP.Write("\n\n");
			writeDataP.Close();
		}else{
			StreamWriter writeData = new StreamWriter("out.txt", true);
			for(double r=rStart; r<rEnd; r+=0.02){
				writeData.WriteLine("{0,16:f15} \t {1,16:f15}", r, cspline.eval(r));
			}
			writeData.WriteLine("{0,16:f15} \t {1,16:f15}", rEnd, cspline.eval(rEnd));
			writeData.Write("\n\n");
			writeData.Close();
		}

		if(printAnalytic){
			// Print out the analytic solution from r=0 to r=12.
			StreamWriter printAnalyticSol = new StreamWriter("outAnalytic.txt");
			for(double r=0; r<12; r+=0.02){
				printAnalyticSol.WriteLine("{0,16:f15} \t {1,16:f15}", r, r*Exp(-r));
			}
			printAnalyticSol.Close();
		}

	} // end Main
} // end class
