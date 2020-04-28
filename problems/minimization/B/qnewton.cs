using System;
using static System.Math;

public class minimization{

	public static int qnewton(Func<vector, double> f, ref vector x, double eps = 1e-3){

		// At the beginning we approximate the inverse Hessian matrix with the identity
		// matrix
		int n = x.size;
		matrix B = new matrix(n, n);
		B.set_identity();

		double alpha = 1e-4;
		int nsteps = 0;
			
		vector gradx = gradient(f, x);
		double fx = f(x); 
		
		while(true){
			nsteps++;
			
			// Calculate the Newton step
			vector deltax = -B*gradx;

			double lambda = 1;
			vector x1;
			double fx1;
			double Armijo;

			// Backtracking algorithm
			while(true){
				x1 = x + lambda*deltax;
				fx1 = f(x1);

				/*
				The Armijo condition denotes when it is a good step. The new function
				value should be smaller than this value
				*/
				Armijo = fx + alpha*lambda*deltax.dot(gradx);
				if(fx1 < Armijo){
					// Accept the step
					break;
				}
				if(lambda<1e-8){
					// Bad step but we accept it. Reset the Hessian matrix to
					// the identity
					B.set_identity();
					break;
				}

				lambda/=2;
			}

			vector s = lambda*deltax;

			// Update the inverse Hessian matrix via the symmetric Broyden update
			vector gradx1 = gradient(f, x1);
			vector y = gradx1 - gradx;
			vector u = s - B*y;
			double gamma = (u.dot(y))/(2*s.dot(y));
			vector a = (u - gamma*s)/(s.dot(y));

			// We only update if the calculated a-value is 'numerically safe', that is,
			// if s^T*y is large enough so we do not divide by a too small number
			if(Abs(s.dot(y)) > 1e-6){
				// We do not really have a way to multiply two vectors and get out
				// a matrix, but it can be handled with the update function that
				// we have in the matrix class
				B.update(a, s, 1);
				B.update(s, a, 1); 
			}
			
			// Move to the new position before running the full loop again
			x = x1;
			fx = fx1;
			gradx = gradx1;

			// When the gradient is close enough to zero we break the loop
			if(gradx.norm()<eps){
				break;
			}
		}

	
		return nsteps;

	}

	public static vector gradient(Func<vector, double> f, vector x){
		
		int n = x.size;
		vector grad = new vector(n);
		double fx = f(x);

		// (Almost) Infinitesimally small element used for calculating the gradient
		double dx = 1e-7;

		for(int i=0; i<n; i++){
			// We make an infinitesimally small step

			x[i] += dx;
			grad[i] = (f(x) - fx)/dx;
			x[i] -= dx;
		}

		return grad;
	} // end gradient
} // end class
