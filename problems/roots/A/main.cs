using System;
using static System.Console;

class main{
	public static void Main(){
		
		var rand = new Random();
		double eps = 1e-6;
	
		// Let's try to find a root of a simple 1D parabola
		// Analytic roots at x = -2 and x = 0.5
		Func<vector, vector> parabola = delegate(vector x){
			vector z = new vector(1);
			z[0] = 2*x[0]*x[0] + 3*x[0] - 2;
			return z;
		};


		// Let's try to start somewhere lower than -2.
		vector x0 = new vector(1);
		x0[0] = -3.2;

		vector root = roots.newton(parabola, x0, eps);
		
		WriteLine("Attempting to find 2 roots for f(x) = 2x^2 + 3x - 2.");
		
		WriteLine();
		WriteLine("Looking for the first root");
		WriteLine("The analytic root is x = -2.");
		WriteLine("Starting search from x0 = {0}.", x0[0]);
		WriteLine("A root has been found at x = {0}.", root[0]);
		WriteLine("The function value at this point is {0}.", parabola(root)[0]);

		// Let's start somewhere higher than 0.5 now
		x0[0] = 2;	
		root = roots.newton(parabola, x0, eps);

		WriteLine();
		WriteLine("Looking for the second root:");
		WriteLine("The analytic root is x = 1/2.");
		WriteLine("Starting search from x0 = {0}.", x0[0]);
		WriteLine("A root has been found at x = {0}.", root[0]);
		WriteLine("The function value at this point is {0}.", parabola(root)[0]);
		
		WriteLine("---------------------------------------------");
		
		// Let's test the root finding algorithm with a few simple functions of two variables
		Func<vector, vector> f = delegate(vector x){
			vector z = new vector(2);
			z[0] = x[0]*x[0];
			z[1] = x[1]*x[1];
			return z;
		};

		// Let's pick a random starting point and see if the root can be found
		x0 = new vector(2+0.4*rand.NextDouble(), 5 - 1.2*rand.NextDouble());
		root = roots.newton(f, x0, eps);
		
		WriteLine();
		WriteLine("Attempting to find a root for the set of equations f(x) = x^2 and" +
		" g(y) = y^2:");
		WriteLine("The analytic roots are x = 0 and y = 0.");
		WriteLine("Starting search from x0 = {0} and y0 = {1}.", x0[0], x0[1]);
		WriteLine("A root has been found at x = {0}, y = {1}.", root[0], root[1]);
		WriteLine("f(x) = {0}", f(root)[0]);
		WriteLine("g(y) = {0}", f(root)[1]);


		WriteLine("---------------------------------------------");

		// Let's try to find the extremums for Rosenbrock's valley function
		// Apparently also called Rosenbrocks banana function, although a valley and a
		// banana look vastly different...
	
		// We define a function that returns gradient of the function. When the derivatives
		// become 0 we know that we have found an extremum
		Func<vector, vector> rosenbrockGrad = delegate(vector k){
			vector z = new vector(2);
			double x = k[0];
			double y = k[1];
			z[0] = -2*( (1-x) + 200*(y-x*x)*x );
			z[1] = 200*(y-x*x);
			return z;
		};

		// Function to evaluate the function value of the Rosenbrock function
		Func<double, double, double> rosenbrock = delegate(double x, double y){
			return (1-x)*(1-x) + 100*(y-x*x)*(y-x*x);
		};

		// Let's pick a random starting point and see if the root can be found
		x0 = new vector(3+0.6*rand.NextDouble(), 5 - 1.2*rand.NextDouble());
		root = roots.newton(rosenbrockGrad, x0, eps);


		WriteLine();
		WriteLine("Attempting to find extremums for Rosenbrocks function" +
		" f(x,y) = (1-x)^2 + 100*(y-x^2)^2:");
		WriteLine("The analytic extremum is at x = 1 and y = 1.");
		WriteLine();
		
		WriteLine("Searching for roots of the gradient:");
		WriteLine("Starting search from x0 = {0} and y0 = {1}.", x0[0], x0[1]);
		WriteLine("A root has been found at x = {0}, y = {1}.", root[0], root[1]);
		WriteLine("The x-derivative of the Rosenbrock function at the root is {0}.",
		rosenbrockGrad(root)[0]);
		WriteLine("The y-derivative of the Rosenbrock function at the root is {0}.",
		rosenbrockGrad(root)[1]);
		WriteLine("The Rosenbrock function should be 0 at the extremum.");
		WriteLine("The found function value is f(x,y) = {0}.", rosenbrock(root[0], root[1]));




	}
}
