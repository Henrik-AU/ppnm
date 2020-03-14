using static cmath;
using static System.Console;
using static System.Math;

class main{
	static int Main(){
	
	double a = sqrt(2);
	Write("sqrt2 = {0}\n", a);	
	
	
	complex i = new complex (0,1);

	complex ipowi = i.pow(i);
	Write($"i.pow(i) = {ipowi}\n");

	complex sinipi = sin(i*PI);
	Write($"sin(i*pi) = {sinipi}\n");

	complex epowi = exp(i);
	Write($"epowi = {epowi}\n");
	
	complex epowipi = exp(i*PI);
	Write($"epowipi = {epowipi}\n");

	return 0;
	}
}
