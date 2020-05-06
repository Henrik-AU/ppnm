using System;
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
		}


		// Let's feed the data to a neural network with 3 hidden neurons, with x*exp(-x^2)
		// as the activation function
		Func<double, double> f = (x) => x*Exp(-x*x);
		int neurons = 3;
		var ann = new ann(neurons, f);

		// Next, let's train the network with our prepared data
		ann.training(xs, ys);

		// Now let's take 20 new data points and send it through the network and see how well
		// it does.
		int k = 20;
		Random rand = new Random();
		vector xTest = new vector(k);
		vector yTest = new vector(k);
		for(int i=0; i<k; i++){
			xTest[i] = 2*PI*rand.NextDouble();
			yTest[i] = ann.feedforward(xTest[i]);
			WriteLine("{0}\t{1}", xTest[i], yTest[i]);
		}
		
		Error.WriteLine("Printing final parameters");
		for(int i=0; i<3*neurons; i++){
			Error.WriteLine("{0}", ann.finalParams[i]);
		}

	} // end Main function
} // end class
