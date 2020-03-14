using System;
using static System.Console;
using static System.Math;

class main{

const double inf = double.PositiveInfinity;


	static void Main(){
		WriteLine("Printing out exercise A:");

		Func<double,double> f = (x) => Log(x)/Sqrt(x);
		double fresult;

		fresult = quad.o8av(f,0,1);

		WriteLine("The integral from 0 to 1 of ln(x)/sqrt(x) gives {0}", fresult);
		WriteLine("The result of the integral should be -4");		
		WriteLine();


		Func<double,double> g = (x) => Exp(-Pow(x,2));
		double gresult;

		gresult = quad.o8av(g,-inf,inf);

		WriteLine("The integral from -inf to inf of exp(-x^2) gives {0}", gresult);
		WriteLine("The integral should be equal to sqrt(pi) = {0}", Sqrt(PI));
		WriteLine();
		

		for(double p=1;p<=5; p +=1){
			Func<double,double> h = (x) => Pow((Log(1/x)),p);
			double hresult;

			hresult = quad.o8av(h,0,1);

			WriteLine("The integral from 0 to 1 of ln(1/x)^p with p = {0} gives {1}", p, hresult);
		}

		WriteLine("The results of the integrals should be equal to gamma(p+1) = p!");
		WriteLine();
	
		
		WriteLine("Printing out exercise B:");

		Func<double,double> j = (x) => Sqrt(x) * Exp(-x);
		double jresult;
		jresult = quad.o8av(j,0,inf);
		WriteLine("The integral from 0 to inf of sqrt(x)*exp(-x) gives = {0}", jresult);
		WriteLine("According to Wikipedia the result should be sqrt(pi)/2 = {0}", Sqrt(PI)/2);
		WriteLine();

		
		Func<double,double> k = (x) => Pow(x,3) / (Exp(x)-1);
		double kresult;
		kresult = quad.o8av(k,0,inf);
		WriteLine("The integral from 0 to inf of x^3/(exp(x)-1) gives = {0}", kresult);
		WriteLine("According to Wikipedia the result should be pi^4/15 = {0}", Pow(PI,4)/15);
		WriteLine();

		
		Func<double,double> l = (x) => Pow(Sin(x),2) / Pow(x,2);
		double lresult;
		lresult = quad.o8av(l,0,inf);
		WriteLine("The integral from 0 to inf of sin^2(x)/x^2 gives = {0}", lresult);
		WriteLine("According to Wikipedia the result should be pi/2 = {0}", PI/2);
		WriteLine();
}

}
