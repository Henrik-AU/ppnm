using static cmath;
using static System.Console;
using static System.Math;

class main{
	static void Main(){
	
		double a = sqrt(2);
		WriteLine("sqrt(2) = {0}. \t\t\t\t Correct result: 1.41421", a);	
	
		complex i = new complex(0,1);

		complex ipowi = i.pow(i);
		WriteLine("i.pow(i) = {0}. \t\t\t Correct result: 0.20787", ipowi);

		complex sinipi = sin(i*PI);
		WriteLine("sin(i*pi) = {0}. \t\t\t Correct result: 11.54873*i", sinipi);

		complex epowi = exp(i);
		WriteLine("epowi = {0}. \t\t Correct result: 0.54030 + 0.84147*i", epowi);
	
		complex epowipi = exp(i*PI);
		WriteLine("epowipi = {0}. \t\t\t Correct result: -1", epowipi);

		complex sinhi = sinh(i);
		WriteLine("sinh(i) = {0}. \t\t\t Correct result: 0.84147*i", sinhi);

		complex coshi = cosh(i);
		WriteLine("cosh(i) = {0}. \t\t\t Correct result: 0.54030", coshi);

		complex minusOne = new complex(-1, 0);
		complex sqrtMinusOne = sqrt(minusOne);
		WriteLine("sqrt(-1) = {0}. \t\t\t Correct result: i", sqrtMinusOne);

		complex sqrti = sqrt(i);
		WriteLine("sqrt(i) = {0}. \t Correct result: 0.70710 + 0.70710*i", sqrti);
		
		



	}
}
