using System;
using static System.Math;
using static System.Console;

class main{
	static void Main(){

		// First we create a set of tabulated data to feed to the network. Let's try with
		// a simple sine function first, with evenly spaced points.
		int m = 30;
		double a = 0;
		double b = 2*PI;
		vector xs = new vector(m);
		vector ys = new vector(m);
		for(int i=0; i<m; i++){
			xs[i] = a + (b-a)*i/(m-1);
			ys[i] = Sin(xs[i]);
		}

		// Let's feed the data to a neural network with 3 hidden neurons, with the activation
		// function f(x) = x*exp(-x^2)
		Func<double, double> f = (x) => x*Exp(-x*x);
		int neurons = 3;
		var ann = new ann(neurons, f);

		// Next, let's train the network with the prepared data
		ann.training(xs, ys);

		// Now let's take 30 new data points and send it through the network and see how well
		// it does. We will also try to find the derivative and the integral
		int k = 30;
		Random rand = new Random();
		vector xTest = new vector(k);
		vector yTest = new vector(k);
		//vector yDerivTest = new vector(k);
		//vector yIntegTest = new vector(k);
		for(int i=0; i<k; i++){
			xTest[i] = 2*PI*rand.NextDouble();
			yTest[i] = ann.feedforward(xTest[i]);
			//yDerivTest[i] = ann.ffDeriv(xTest[i]);
			//yIntegTest[i] = ann.ffInteg(xTest[i]);
			WriteLine("{0}\t{1}", xTest[i], yTest[i]);
			//WriteLine("{0}\t{1}\t{2}\t{3}", xTest[i], yTest[i], yDerivTest[i],
			//yIntegTest[i]);
		}

		


	}
}
