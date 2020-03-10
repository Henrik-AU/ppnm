using System;
using static System.Console;
using static System.Math;

class main{
	static void Main(){
	Func<double,double> f = (x) => Log(x)/Sqrt(x);
	double a = 0;
	double b = 1;
	double result;

	result = quad.o8av(f,a,b);

	WriteLine("{0}", result);



}

}
