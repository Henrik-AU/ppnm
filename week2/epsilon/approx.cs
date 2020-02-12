using static System.Math;

public class Approx{
	public static bool approx(double a, double b, double tau=1e-9, double epsilon=1e-9){
	if(Abs(a-b)<tau) return true;
	else if (Abs(a-b)/(Abs(a) + Abs(b)) < epsilon/2) return true;
	else return false;
	}
}
