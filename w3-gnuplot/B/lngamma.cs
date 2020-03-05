using static System.Math;
public static partial class math{

	public static double lngamma(double x){
		// Single precision gamma function (Gergo Nemes, from Wikipedia)
		
		// For negative x-values we can use Eulers reflection formula to calculate lngamma via
		// the positive x-values.
		if(x<0) return Log(PI) - Log(Sin(PI*x)) - lngamma(1-x);

		// Our formula is best for high values of x, so for small x we have to shift the
		// calculation to a higher x-value. We do this via a ln-gamma property from wiki.
		if(x<9) return lngamma(x+1) - Log(x);

		return x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;

}

}
