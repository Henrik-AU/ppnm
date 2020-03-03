using static System.Math;

public static partial class math{
	public static double gamma(double x){
	/// single precision gamma function (Gergo Nemes, from Wikipedia)
	if(x<0)return PI/Sin(PI*x)/gamma(1-x);
	if(x<9)return gamma(x+1)/x;
	double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
	return Exp(lngamma);
}
}
