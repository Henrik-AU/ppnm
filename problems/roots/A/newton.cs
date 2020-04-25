using System;

public class roots{
	public static vector newton(Func<vector, vector> f, vector x, double epsilon = 1e-3,
	double dx = 1e-7){
		
		int n = x.size;
		vector fx = f(x);
		vector x1;
		vector fx1;

		// Run a loop to find the roots
		while(true){
			// Create the Jacobian
			matrix J = jacobian(f, x, fx);
			
			// Run a QR-decomposition algorithm on J, and find it's inverse
			var qrJ = new qrDecompositionGS(J);
			matrix B = qrJ.inverse();
			
			// Find the Newton stepsize
			vector deltaX = -B*fx;

			// Lambda factor to take a more conservative step
			double lambda = 1;
			
			// Backtracking linesearch algorithm
			while(true){
				// Attempt a step and check if we arrive at a point closer to a root
				x1 = x + deltaX*lambda;
				fx1 = f(x1);
				if(fx1.norm() < fx.norm() * (1 - lambda/2)){
					// This should be a good step, so we accept it by breaking
					// the loop.
					break;
				}
				if(lambda < 1.0/64){
					// This is not a good step, but we surrender and accept it
					// anyway, hoping that the next step will be better.
					break;
				}
				
				// Reduce lambda by a factor of 2 before next attempt
				lambda/=2;	
			}

			// Move us to the newfound better positon before attempting the next step
			x = x1;
			fx = fx1;

			// Check if we have converged to a value closer to zero than the given
			// accuracy epsilon. If yes, we are done, so we break the loop and return
			// the position of the root, x.
			if(fx.norm() < epsilon){
				break;
			}
		}
		return x;
	} // end newton

	public static matrix jacobian(Func<vector, vector> f, vector x, vector fx, double dx= 1e-7){
		// Calculate the Jacobian
		int n = x.size;
		matrix J = new matrix(n, n);

		for(int j=0; j<n; j++){
			// We make an infinitesimally small step in one dimension at a time only
			x[j] += dx;
			vector df = f(x) - fx;
			for(int i=0; i<n; i++){
				J[i,j] = df[i]/dx;
			}
			x[j] -= dx;
		}	
		return J;
	} // end jacobian

}
