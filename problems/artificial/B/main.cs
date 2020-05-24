using System;
using System.IO;
using static System.Math;
using static System.Console;

class main{
	static void Main(){

		// First we create a set of tabulated data to feed to the network. Let's try with
		// a simple sine function first, with evenly spaced points.
		int m = 40;
		double a = 0;
		double b = 2*PI;
		vector xs = new vector(m);
		vector ys = new vector(m);
		for(int i=0; i<m; i++){
			xs[i] = a + (b-a)*i/(m-1);
			ys[i] = Sin(xs[i]);
			// Print out the tabulated data to the output file
			WriteLine("{0}\t{1}", xs[i], ys[i]);
		}

		// Let's feed the data to a neural network with 5 hidden neurons, with the activation
		// being a predefined Gaussian, exp(-x^2).
		int neurons = 5;
		var annGauss = new annGauss(neurons);

		// Next, let's train the network with the prepared data
		double eps = 1e-3;
		int nsteps = annGauss.training(xs, ys, eps);

		StreamWriter writeOut = new StreamWriter("out.txt");
		writeOut.WriteLine("The networks {0} neurons has been trained with {1} points.",
		neurons, m);
		writeOut.WriteLine("The network parameters were minimized to an accuracy of {0}.", eps);
		writeOut.WriteLine("The training (minimization) was done in {0} steps", nsteps);
		writeOut.WriteLine("The final parameters for the network are available in the Log.");
		writeOut.Close();

		// Now let's take 200 new data points and send it through the network and see how well
		// it does. We will also try to find the derivative and the integral
		int points = 200;
		vector xTest = new vector(points);
		vector yTest = new vector(points);
		vector yDerivTest = new vector(points);
		vector yIntTest = new vector(points);

		// The new data is to be printed in a new block in the output file.
		Write("\n\n");
		for(int i=0; i<points; i++){
			xTest[i] = 0+2*PI*i/(points-1);
			yTest[i] = annGauss.feedforward(xTest[i]);
			yDerivTest[i] = annGauss.feedforwardDeriv(xTest[i]);
			yIntTest[i] = annGauss.feedforwardInt(xTest[i]);
			WriteLine("{0}\t{1}\t{2}\t{3}", xTest[i], yTest[i], yDerivTest[i],
			yIntTest[i]);
		}


		// Print the network parameters to the Log
		Error.WriteLine("Printing final parameters");
		for(int i=0; i<3*neurons; i++){
			Error.WriteLine("{0}", annGauss.finalParams[i]);
		}

	}
}
