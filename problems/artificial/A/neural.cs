using System;
using static System.Math;

public class ann{
	
	// n is the number of hidden neurons
	public int n;
	// Variables for current set of parameters
	private double ai;
	private double bi;
	private double wi; 
	// f is the activation function, here chosen to be a simple Gaussian
	public Func<double, double> f = (x) => Exp(-(x-ai)*(x-ai)/bi)*wi;
	// Vector of all parameters, ordered as a_0, b_0, w_0, a_1, b_1, w_1, a_2 ...
	public vector param;

	// Constructor that does the training and sets up the network
	public ann(vector x, vector y, int n){
		// To train the network we minimize a system of n functions (hidden neurons), with
		// each 3 parameters
		vector param = new vector(3*n);
		for(int i = 0; i<3*n; i++){
			param[i] = 1;
		}

		// Perform the optimization
		int nsteps = minimization.qnewton(deviation, ref param);	
	}

	public double deviation(ref vector z){
		// z is the guess for the parameters. It is not actually used since it is a public
		// variable and thus already available?
		double sum = 0;
		// Go through the data points one at a time, and calculate the total deviation
		for(int i = 0; i<xs.size; i++){
			sum += Pow(feedforward(x[i]) - y[i], 2);
		}
		return sum;
	}

	public double feedforward(double x){
		double sumNeuron = 0;
		for(int j=0; j<n; j++){
			ai = param[0+3*j];
			bi = param[1+3*j];
			wi = param[2+3*j];
			sumNeuron += f(x);	
		}
		return sumNeuron;
	}

	
}
