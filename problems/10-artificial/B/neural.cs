using System;
using System.Diagnostics;
using static System.Console;
using static System.Math;

public class annGauss{
	
	// n is the number of hidden neurons
	public int n;

	// Left end-point of the specified data interval
	private double startPoint;

	// Predefined activation function - it must be, if we want to be able to return the
	// derivative and integral
	static Func<double, double> gauss = (x) => Exp(-x*x);

	// Using the small errorfunction algorithm from exercise 6
	// On this form, it is evaluated correctly when the function is taken at (x-a)/b
	static Func<double, double> gaussDeriv = (x) => -2*x*Exp(-x*x);
	
	static Func<double, double> gaussInt = (x) => Sqrt(PI)/2*math.erf(x);

	// Vector of all parameters, ordered as a_0, b_0, w_0, a_1, b_1, w_1, a_2 ...
	public vector finalParams;

	// Constructor sets up the network
	public annGauss(int nNeurons){

		// Take the inputs and place them in the public variables for the class
		n = nNeurons;
		finalParams = null;

	} // end constructor


	public int training(vector xs, vector ys, double eps = 1e-3){
		// Create a vector with initial parameters, that spread out activation functions
		// somewhat evenly.
		// We set the a_i's to evenly span the distance from [c, d] where c and d are the
		// start and end-point of the interval along the x-axis, that we are interested in. 
		// I assume that the training data is provided in order, with the first point being at
		// c and the last at d.
		// We save the starting point in a variable, such that it can be accessed by the
		// integration function
		vector param = new vector(3*n);
		startPoint = xs[0];

		for(int i = 0; i<n; i++){
			param[0+3*i] = xs[0] + (xs[xs.size-1] - xs[0])*i/(n-1); 
			param[1+3*i] = 1; // b_i's start at 1
			param[2+3*i] = 1; // Weights start at 1
		}

		Func<vector, double> deviation = delegate(vector p){
			double sum = 0;
			// Go through the data points, and calculate the total deviation
			for(int i = 0; i<xs.size; i++){
				sum += Pow((feedforward(xs[i], p) - ys[i]), 2);
			}
			return sum;
		};

		// Perform the optimization
		int nsteps = minimization.qnewton(deviation, ref param, eps);

		// The vector param now contains the optimized parameters, so we now save this in
		// the finalParams vector that is available to all functions and outside the class
		finalParams = param;

		return nsteps;	
	} // end training


	public double feedforward(double x, vector parameters = null){
		Trace.Assert(finalParams == null, "The network has not been trained yet.");

		if(parameters == null){
			parameters = finalParams;
		}
		

		double sumNeuron = 0;
		for(int i=0; i<n; i++){
			double ai = parameters[0+3*i];
			double bi = parameters[1+3*i];
			double wi = parameters[2+3*i];
			sumNeuron += wi*gauss((x-ai)/bi);
		}
		return sumNeuron;
	} // end feedforward

		
	public double feedforwardDeriv(double x){
		double sumNeuron = 0;
		for(int i=0; i<n; i++){
			double ai = finalParams[0+3*i];
			double bi = finalParams[1+3*i];
			double wi = finalParams[2+3*i];
			sumNeuron += gaussDeriv((x-ai)/bi)*wi/bi;
		}
		return sumNeuron;
	} // end feed forward derivative

	public double feedforwardInt(double x){
		double sumNeuron = 0;
		for(int i=0; i<n; i++){
			double ai = finalParams[0+3*i];
			double bi = finalParams[1+3*i];
			double wi = finalParams[2+3*i];
			// Add the integral all the way from zero and up to the point x
			sumNeuron += gaussInt((x-ai)/bi)*bi*wi;
			// Subtract the part of the integral that lies before the leftmost interval
			// point
			sumNeuron -= gaussInt((startPoint-ai)/bi)*bi*wi;
		}
		// The sumNeuron now holds the value F(x) - F(startPoint). (startPoint being the
		// leftmost interval point)
		return sumNeuron;
	} // end feed forward integration
	
} // end class
