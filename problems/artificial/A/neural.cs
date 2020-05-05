using System;
using static System.Console;
using static System.Math;

public class ann{
	
	// n is the number of hidden neurons
	public int n;
	// Vectors for the tabulated data
	private vector xs;
	private vector ys;

	// Dummy initialization of ai, bi and wi
	private double ai = 1;
	private double bi = 1;
	private double wi = 1;
	
	// Vector of all parameters, ordered as a_0, b_0, w_0, a_1, b_1, w_1, a_2 ...
	public vector finalParams;

	// Constructor that does the training and sets up the network
	public ann(vector x, vector y, int nNeurons, double startVal = 0.01){
		// To train the network we minimize a system of n functions (hidden neurons), with
		// each 3 parameter

		// Make the tabulated data available for the other functions in the class also
		xs = x;
		ys = y;
		n = nNeurons;

		// Create a vector with initial parameters all being set to 0.1
		vector param = new vector(3*n);
		for(int i = 0; i<3*n; i++){
			param[i] = startVal;
		}

		// Perform the optimization
		int nsteps = minimization.qnewton(deviation, ref param);
		Error.WriteLine("Neural network optimized in {0} steps.", nsteps);

		// The vector param now contains the optimized parameters, so we now save this in
		// the finalParams vector that is available to all functions and outside the class
		finalParams = param;	
	} // end constructor

	public double deviation(vector param){
		double sum = 0;
		// Go through the data points one at a time, and calculate the total deviation
		for(int i = 0; i<xs.size; i++){
			sum += Pow((feedforward(xs[i], param) - ys[i]), 2);
		}
		return sum;
	} // end deviation

	public double feedforward(double x, vector parameters = null){
		if(parameters == null){
			parameters = finalParams;
		}

		double sumNeuron = 0;
		for(int i=0; i<n; i++){
			ai = parameters[0+3*i];
			bi = parameters[1+3*i];
			wi = parameters[2+3*i];
			sumNeuron += f2(x);	
		}
		return sumNeuron;
	} // end feedforward

	
	public double ffDeriv(double x){

		double sumNeuron = 0;
		for(int i=0; i<n; i++){
			ai = finalParams[0+3*i];
			bi = finalParams[1+3*i];
			wi = finalParams[2+3*i];
			sumNeuron += f2Deriv(x);	
		}
		return sumNeuron;
	} // end feed forward derivative

	// f is the activation function.
	private double f(double t){
		return (t-ai)/bi * Exp(-(t-ai)*(t-ai)/bi/bi)*wi;
	}

	// Different activation function
	private double f2(double t){
		return Cos(5*(t-ai)/bi) * Exp(-(t-ai)*(t-ai)/bi/bi)*wi;
	}

	// Derivative of f
	private double fDeriv(double t){
		return wi/bi * Exp(-(t-ai)*(t-ai)/bi/bi)*(1 - 2*(t-ai)*(t-ai)/bi/bi);

	}
	
	// Derivative of f2
	private double f2Deriv(double t){
		return -wi/bi * Exp(-(t-ai)*(t-ai)/bi/bi) * (5*Sin(5*(t-ai)/bi)
			+ 2*(t-ai)*Cos(5*(t-ai)/bi)/bi);
	}

	// Integral of f
	private double fInteg(double t){
		return wi*bi/2 *(Sqrt(PI)*ai*math.erf((t-ai)/bi) - b*Exp(-(t-ai)*(t-ai)/bi/bi));

	}

	// Integral of f2
	//private double f2Integ(double t){
	//	return -wi/bi * Exp(-(t-ai)*(t-ai)/bi/bi) * (5*Sin(5*(t-ai)/bi)
	//		+ 2*(t-ai)*Cos(5*(t-ai)/bi)/bi);
	//}
	
}
