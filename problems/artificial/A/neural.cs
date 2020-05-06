using System;
using static System.Console;
using static System.Math;

public partial class ann{
	
	// n is the number of hidden neurons
	public int n;

	// Activation function
	public Func<double, double> f;

	// Vector of all parameters, ordered as a_0, b_0, w_0, a_1, b_1, w_1, a_2 ...
	public vector finalParams;

	// Constructor sets up the network
	public ann(int nNeurons, Func<double, double> acFunc){

		// Take the inputs and place them in the public variables for the class
		n = nNeurons;
		f = acFunc;

	} // end constructor


	public void training(vector xs, vector ys){
		// Create a vector with initial parameters, that spread out activation functions
		// somewhat evenly.
		// We set the a_i's to evenly span the distance from [c, d] where c and d are the
		// start and end-point of the interval along the x-axis, that we are interested in. 
		// I assume that the training data is provided in order, with the first point being at
		// c and the last at d
		vector param = new vector(3*n);

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
		int nsteps = minimization.qnewton(deviation, ref param);
		Error.WriteLine("Neural network optimized in {0} steps.", nsteps);

		// The vector param now contains the optimized parameters, so we now save this in
		// the finalParams vector that is available to all functions and outside the class
		finalParams = param;	
	} // end training


	public double feedforward(double x, vector parameters = null){
		if(parameters == null){
			parameters = finalParams;
		}

		double sumNeuron = 0;
		for(int i=0; i<n; i++){
			double ai = parameters[0+3*i];
			double bi = parameters[1+3*i];
			double wi = parameters[2+3*i];
			sumNeuron += wi*f((x-ai)/bi);
		}
		return sumNeuron;
	} // end feedforward

	/*	
	public double ffDeriv(double x){
		double sumNeuron = 0;
		for(int i=0; i<n; i++){
			double ai = finalParams[0+3*i];
			double bi = finalParams[1+3*i];
			double wi = finalParams[2+3*i];
			sumNeuron += wi*fDeriv((x-ai)/bi);
		}
		return sumNeuron;
	} // end feed forward derivative

	public double ffInteg(double x){
		double sumNeuron = 0;
		for(int i=0; i<n; i++){
			double ai = finalParams[0+3*i];
			double bi = finalParams[1+3*i];
			double wi = finalParams[2+3*i];
			sumNeuron += fInteg(x);
		}
		return sumNeuron;
	} // end feed forward integration
	*/
} // end class
