using System;
using static System.Math;
using static System.Console;

class main{
	static void Main(){

		// First we create a set of tabulated data to feed to the network. Let's try with
		// a simple sine function first, with m random points.
		int m = 300;
		Random rand = new Random(1);
		vector xs = new vector(m);
		vector ys = new vector(m);
		for(int i=0; i<m; i++){
			xs[i] = 2*PI*rand.NextDouble();
			ys[i] = Sin(xs[i]);
		}

		// Let's feed the data to a neural network with 3 hidden neurons
		int neurons = 3;
		var ann = new ann(xs, ys, neurons, "f");

		// Now let's take 40 new data points and send it through the network and see how well
		// it does. We will also try to find the derivative and the integral
		int k = 40;
		vector xTest = new vector(k);
		vector yTest = new vector(k);
		vector yDerivTest = new vector(k);
		//vector yIntegTest = new vector(k);
		for(int i=0; i<k; i++){
			xTest[i] = 2*PI*rand.NextDouble();
			yTest[i] = ann.feedforward(xTest[i]);
			yDerivTest[i] = ann.ffDeriv(xTest[i]);
			//yIntegTest[i] = ann.ffInteg(xTest[i]);
			WriteLine("{0}\t{1}\t{2}", xTest[i], yTest[i], yDerivTest[i]);
			//WriteLine("{0}\t{1}\t{2}\t{3}", xTest[i], yTest[i], yDerivTest[i],
			//yIntegTest[i]);
		}

		


	}
}
