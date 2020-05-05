using System;
using static System.Math;
using static System.Console;

class main{
	static void Main(){

		// NOTES			
		// It appears that the activation function, x*exp(-x^2) has difficulties
		// with periodic functions - thus it fails if more than a quarter of a period of
		// cosine is used for the training
		// It works better with the activation function cos(5*x)*exp(-x^2).

		// Let's test the simple neural network
		// First we create a set of tabulated data to feed to the network. Let's try with
		// a simple cosine function first, with m random points.
		int m = 300;
		Random rand = new Random(1);
		vector xs = new vector(m);
		vector ys = new vector(m);
		for(int i=0; i<m; i++){
			xs[i] = 2*PI*rand.NextDouble();
			ys[i] = Cos(xs[i]);
		}


		// Let's feed the data to a neural network with 3 hidden neurons
		int neurons = 3;
		var ann = new ann(xs, ys, neurons);

		// Now let's take 40 new data points and send it through the network and see how well
		// it does.
		int k = 40;
		vector xTest = new vector(k);
		vector yTest = new vector(k);
		for(int i=0; i<k; i++){
			xTest[i] = 2*PI*rand.NextDouble();
			yTest[i] = ann.feedforward(xTest[i]);
			WriteLine("{0}\t{1}\t{2}", xTest[i], yTest[i], Abs(yTest[i]-Cos(xTest[i])));
		}
		
		Error.WriteLine("Printing final parameters");
		for(int i=0; i<3*neurons; i++){
			Error.WriteLine("{0}", ann.finalParams[i]);
		}



		// Part B

		// Now let's check if we can get out the derivative also
		Write("\n\n");
		vector yDerivTest = new vector(k);
		for(int i=0; i<k; i++){
			yDerivTest[i] = ann.ffDeriv(xTest[i]);
			WriteLine("{0}\t{1}", xTest[i], yDerivTest[i]);
		}

		// The integral is not easy to get, since the activation function is difficult to
		// integrate. Instead let's try a new neural network and use the easier to integrate
		// activation function x*Exp(-x^2).
		




	
	}
}
