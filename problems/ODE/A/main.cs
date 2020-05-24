using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;


class main{
	static void Main(){
		
		// Let's try to solve the differential equation u'' = -u.
		Func<double, vector, vector> diffeq = delegate(double x, vector y){
			return new vector(y[1], -y[0]);
		};


		// Starting point
		int a = 0;
		vector ya = new vector(0, 1);

		// Endpoint and tolerances
		double b = 3*PI;
		double acc = 1e-3;
		double eps = 1e-3;

		// Stepsize that we want to use for the ODE
		double stepsize = 0.1;

		// Create lists for the data
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();

		// Call the ODE solver to solve the system
		ode.rk45(diffeq, a, ya, b, stepsize, acc, eps, xlist:xs, ylist:ys);

		for(int i=0; i<xs.Count; i++){
			WriteLine("{0,8:f8}\t{1,8:f8}\t{2,8:f8}", xs[i], ys[i][0], ys[i][1]);
		}

	}
}
