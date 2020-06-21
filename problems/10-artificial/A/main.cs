using System;
using System.IO;
using static System.Math;
using static System.Console;

class main{
	static void Main(){

		// Generate 30 data points for training, evenly spaced on the interval from a to b
		int m = 30;
		double a = 0;
		double b = 2*PI;
		vector xs = new vector(m);
		vector ys = new vector(m);
		for(int i=0; i<m; i++){
			xs[i] = a + (b-a)*i/(m-1);
			ys[i] = Cos(xs[i]);
			// Print out the tabulated data to the output file
			WriteLine("{0}\t{1}", xs[i], ys[i]);
		}


		// Let's feed the data to a neural network with 3 hidden neurons, with x*exp(-x^2)
		// as the activation function
		Func<double, double> f = (x) => x*Exp(-x*x);
		int neurons = 3;
		var ann = new ann(neurons, f);

		// Next, let's train the network with our prepared data
		double eps = 1e-3;
		int nsteps = ann.training(xs, ys, eps);

		StreamWriter writeOut = new StreamWriter("out.txt");
		writeOut.WriteLine("The networks {0} neurons have been trained with {1} points.",
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

		// The new data is to be printed in a new block in the output file.
		Write("\n\n");
		for(int i=0; i<points; i++){
			xTest[i] = 2*PI*i/(points-1);;
			yTest[i] = ann.feedforward(xTest[i]);
			WriteLine("{0}\t{1}", xTest[i], yTest[i]);
		}
		
		// Print the network parameters to the Log
		Error.WriteLine("Printing final parameters");
		for(int i=0; i<3*neurons; i++){
			Error.WriteLine("{0}", ann.finalParams[i]);
		}

	} // end Main function
} // end class
