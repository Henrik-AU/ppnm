using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;


class main{
	static void Main(){

		// Let's try to solve the three-body system in the planar "8" shape as proved
		// existing in the article by Alain Chenciner and Richard Montgomery
		
		// We set the masses and the gravitational constant to 1, and thus the product to 1
		double mG = 1;

		// Starting point
		int a = 0;
		// Initial positions
		
		vector r10 = new vector(new double[] {0.97000436, -0.24308753});
		vector r20 = (-1)*r10.copy();
		vector r30 = new vector(new double[] {0, 0});
		// Initial first derivatives (velocities)
		vector dr30 = new vector(new double[] {-0.93240737, -0.86473146});
		vector dr10 = (-1)*dr30.copy()/2;
		vector dr20 = dr10.copy();

		// Initial positions and first derivatives collected in one large vector
		vector ya = new vector(new double[] {r10[0], r10[1], r20[0], r20[1], r30[0], r30[1],
			dr10[0], dr10[1], dr20[0], dr20[1], dr30[0], dr30[1]});

		// Endpoint and tolerances
		double b = 4*PI;
		double acc = 1e-7;
		double eps = 1e-7;

		// Stepsize that we want to use for the ODE
		double stepsize = 0.1;

		// Create lists for the data
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();
		
		// Define a function that returns the first and second derivatives
		Func<double, vector, vector> threeBody = delegate(double x, vector y){
			// We have the first derivatives as the last 6 entries in vector y
			// The second derivatives can be calculated via Newtons law of gravitation
			vector r1 = new vector(new double[] {y[0], y[1]});
			vector r2 = new vector(new double[] {y[2], y[3]});
			vector r3 = new vector(new double[] {y[4], y[5]});
		
			vector ddr1 = (-1)*mG*(r1-r2)/Pow((r1-r2).norm(), 3) - 
				mG*(r1-r3)/Pow((r1-r3).norm(), 3);	
			vector ddr2 = (-1)*mG*(r2-r3)/Pow((r2-r3).norm(), 3) - 
				mG*(r2-r1)/Pow((r2-r1).norm(), 3);	
			vector ddr3 = (-1)*mG*(r3-r1)/Pow((r3-r1).norm(), 3) - 
				mG*(r3-r2)/Pow((r3-r2).norm(), 3);	
			// Vector components y[6] to y[11] contains the first derivatives
			vector result = new vector(new double[] {y[6], y[7], y[8], y[9], y[10], y[11],
			ddr1[0], ddr1[1], ddr2[0], ddr2[1], ddr3[0], ddr3[1]});
			return result;
		};

		// Call the ODE solver to solve the system
		vector yb = ode.rk45(threeBody, a, ya, b, stepsize, acc, eps, xlist:xs, ylist:ys);

		// Print out the data
		for(int i=0; i<xs.Count; i++){
			Write("{0,3} {1,10:f8}", i, xs[i]);
			for(int j=0; j<6; j++){
				// Prints out the 3*2 position vector components for each evaluation i
				Write(" {0,10:f8}", ys[i][j]);
			}
			WriteLine();
		}
	}
}
