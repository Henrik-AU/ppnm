using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

public partial class ode{

	public static vector rk45(
	Func<double, vector, vector> f,
	double a,
	vector ya,
	double b,
	double h,
	double acc,
	double eps,
	List<double> xlist = null,
	List<vector> ylist = null
	){
		return driver(f, a, ya, b, h, acc, eps, xlist, ylist, rkstep45);
	}


	public static vector driver(
	Func<double, vector, vector> f,
	double a,
	vector ya,
	double b,
	double h,
	double acc,
	double eps,
	List<double> xlist,
	List<vector> ylist,
	Func<Func<double,vector,vector>, double, vector, double, vector[]> stepper
	){
		int nsteps = 0;
		// If the function is provided with an xlist and/or an ylist then clear it and add
		// the starting point to the list(s)
		if(xlist!=null){
			xlist.Clear();
			xlist.Add(a);
		}		
		if(ylist!=null){
			ylist.Clear();
			ylist.Add(ya);
		}
		
		// Solve the ode in steps from a to b
		while(a<b){
			// Check if a step h would make us exceed the endpoint b. In that case, set the
			// stepsize h equal to the last stretch to the endpoint.
			if(a+h>b){
				h = b-a;
			}

			// Attempt to take a step
			vector[] attempt = stepper(f, a, ya, h);
			vector yh = attempt[0];
			vector err = attempt[1];

			// Calculate the local tolerance
			vector tauLocal = (eps*yh.abs() + acc)*Sqrt(h/(b-a));

			// Check if the step is acceptable and within tolerances
			vector tolRatios = new vector(err.size);
			bool acceptStep = true;
			for(int i = 0; i<tauLocal.size; i++){
				// Calculate the fraction of the tolerance ratios
				tolRatios[i] = Abs(tauLocal[i])/Abs(err[i]);
				if(tolRatios[i] < 1){
					// The estimated error is larger than the local tolerance
					acceptStep = false;
				}
			}

			// Find the smallest tolerance ratio - this is the factor we will use for the 
			// empirical step size adjustment
			double factor = tolRatios[0];
			for(int i = 1; i<tol.size; i++){
				factor = Min(factor, tolRatios[i]);
			}

			// If the step is accepted we update and progress
			if(acceptStep){
				a += h;
				ya = yh;
				nsteps++;
				if(xlist!=null){
					xlist.Add(a);
				}	
				if(ylist!=null){
					xlist.Add(ya);
				}	
			}else{
				Error.WriteLine("Bad step at {0}. Step rejected.", a);
			}

			// The next step size is adjusted according to the empirical formula, no matter
			// whether the step was accepted or not. We set an upper maximum for the increase
			// in step size, via maxStepFactor.
			double hFactor = Pow(factor, 0.25)*0.95;

			int maxStepFactor = 2;
			if(dh > maxStepFactor){
				// 
				hFactor = maxStepFactor;
			}
			h = h*hFactor;
			


		}
	Error.WriteLine("ODE solved in {nsteps} steps.", nsteps);
	return ya;
	} // driver function
} // class
