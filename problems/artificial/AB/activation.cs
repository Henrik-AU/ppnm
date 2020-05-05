using static System.Math;

public partial class ann{

	// f is the activation function.
	private double f(double t){
		return (t-ai)/bi * Exp(-(t-ai)*(t-ai)/bi/bi)*wi;
	}

	// Different activation function
	private double f2(double t){
		return Cos(5*(t-ai)/bi) * Exp(-(t-ai)*(t-ai)/bi/bi)*wi;
	}

	// Derivative of f
	private double fDeriv(double t){
		return wi/bi * Exp(-(t-ai)*(t-ai)/bi/bi)*(1 - 2*(t-ai)*(t-ai)/bi/bi);

	}
	
	// Derivative of f2
	private double f2Deriv(double t){
		return -wi/bi * Exp(-(t-ai)*(t-ai)/bi/bi) * (5*Sin(5*(t-ai)/bi)
			+ 2*(t-ai)*Cos(5*(t-ai)/bi)/bi);
	}

	// Integral of f
	private double fInteg(double t){
		//return wi*bi/2 *(Sqrt(PI)*ai*math.erf((t-ai)/bi) - bi*Exp(-(t-ai)*(t-ai)/bi/bi));
		return -wi*bi/2 * Exp(-(ai-t)*(ai-t)/bi/bi);
	}

	// Integral of f2
	//private double f2Integ(double t){
	//	return -wi/bi * Exp(-(t-ai)*(t-ai)/bi/bi) * (5*Sin(5*(t-ai)/bi)
	//		+ 2*(t-ai)*Cos(5*(t-ai)/bi)/bi);
	//}




}
