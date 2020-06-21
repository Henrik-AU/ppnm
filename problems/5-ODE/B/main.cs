using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;


class main{
	static void Main(){

		// Population size in Denmark is around 5.8 million
		double N = 5.8e6;

		// The corona virus seems to take around Tr = 14 days of recoverytime
		// Before closing down the country, 1 infected person infected around 2.5 new ones.
		// This factor is Tr/Tc, so Tc = 14 days/2.5.
		double infectFactor = 2.5;
		double Tr = 14;
		double Tc = Tr/infectFactor;
		
		// Let's try to solve the SIR epidemic model
		Func<double, vector, vector> SIRmodel = delegate(double x, vector y){
			return new vector(-y[1]*y[0]/(N*Tc), y[1]*y[0]/(N*Tc) - y[1]/Tr, y[1]/Tr);
		};


		// Starting point
		int a = 0;
		// Everyone is apparently susceptible to the virus, so S=N in the beginning. Let's
		// assume we have only 200 infected in the start (people who came home from skiing
		// in Ischgl). Initially no one is immune or dead.
		vector ya = new vector(N, 200, 0);

		// Endpoint and tolerances
		// We will simulate 4 months (120 days)
		double b = 120;
		double acc = 1e-6;
		double eps = 1e-6;

		// Stepsize that we want to use for the ODE
		double stepsize = 0.5;

		// Create lists for the data
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();

		// Call the ODE solver to solve the system
		vector yb = ode.rk45(SIRmodel, a, ya, b, stepsize, acc, eps, xlist:xs, ylist:ys);

		for(int i=0; i<xs.Count; i++){
			WriteLine("{0,8:f8}\t{1,8:f8}\t{2,8:f8},\t{3,8:f8}",
			xs[i], ys[i][0], ys[i][1], ys[i][2]);
		}

		// Let's try a model with two months of unhindered spreading, and then two months
		// with 'social distancing' (lowering the amount of new infected by one infected
		// individual).

		b = 60;
		int c = 120;
	
		// Create extra lists for the data
		List<double> xsbc = new List<double>();
		List<vector> ysbc = new List<vector>();

		// Solve the system with unhindered spreading
		yb = ode.rk45(SIRmodel, a, ya, b, stepsize, acc, eps, xlist:xs, ylist:ys);

		// Solve the system with social distancing, with starting parameters being the end
		// result of the 2 months with unhindered spreading
		// The effective social distancing seems to reduce the infection factor to 0.6
		infectFactor = 0.6;	
		Tc = Tr/infectFactor;
		ode.rk45(SIRmodel, b, yb, c, stepsize, acc, eps, xlist:xsbc, ylist:ysbc);
		
		// Print the data
		Write("\n\n");

		for(int i=0; i<xs.Count; i++){
			WriteLine("{0,8:f8}\t{1,8:f8}\t{2,8:f8},\t{3,8:f8}",
			xs[i], ys[i][0], ys[i][1], ys[i][2]);
		}
		Write("\n\n");
		for(int i=0; i<xsbc.Count; i++){
			WriteLine("{0,8:f8}\t{1,8:f8}\t{2,8:f8},\t{3,8:f8}",
			xsbc[i], ysbc[i][0], ysbc[i][1], ysbc[i][2]);
		}
	}
}
