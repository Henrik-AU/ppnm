using static System.Math;
using static System.Console;

// Has to be spelled as the class, not the name of the DLL or the name of the function!
// So here with upper case A.
using static Approx;

class main{
	static int Main(){

	// part A

	WriteLine("Part A:");
	int i=1;
	while(i+1>1) {i++;}
	Write("My max int = {0}\n", i);

	Write("The int.MaxValue command says = {0}\n", int.MaxValue);


	i=1;
	while(i-1<1) {i--;}
	Write("My min int = {0}\n", i);

	Write("The int.MinValue command says = {0}\n", int.MinValue);



	// part B

	WriteLine("\nPart B:");
	double x = 1;
	while(1+x!=1){x/=2;}

	// When the loop breaks x is so small that the computer cannot see any difference
	// between 1 and 1+x. The smallest interval that the computer could distinguish is
	// thus the previous x-value. We therefore multiply it by two before printing it.	
	x*=2;
	Write("The machine epsilon with double is = {0}\n", x);

	float y=1F;
	while((float)(1F+y) != 1F){y/=2F;}

	// When the loop breaks y is so small that the computer cannot see any difference
	// between 1 and 1+y. The smallest interval that the computer could distinguish is
	// thus the previous y-value. We therefore multiply it by two before printing it.	
       	y*=2F;
	Write("The machine epsilon with float is = {0}\n", y);

	// According to Dimitry the machine epsilon for a double precision machince should
	// be around System.Math.Pow(2, -52).
	
	double dmachineepsilon = Pow(2, -52);
	Write("Dimitry says the machince epsilon for doubles should be around = {0}\n", dmachineepsilon);

	double fmachineepsilon = Pow(2, -23);
	Write("Dimitry says the machince epsilon for floats should be around = {0}\n", fmachineepsilon);

	// part C
	WriteLine("\nPart C:");
	int max = int.MaxValue/3; // I get the same value for 2 or 3

	float float_sum_up = 1F;
	for(int j = 2; j < max; j++) float_sum_up+=1F/j;
	Write("The sum going up using floats becomes = {0}\n", float_sum_up);

	float float_sum_down = 1F/max;
	for(int j =max-1;j>0;j--) float_sum_down +=1F/j;
	Write("The sum going down using floats becomes = {0}\n", float_sum_down);
	

	// Using doubles

	double double_sum_up = 1D;

	for(int j = 2; j < max; j++) double_sum_up+= 1D/j;
	Write("The sum going up using doubles becomes = {0}\n", double_sum_up);


	double double_sum_down = 1D/max;
	for(int j = max-1; j > 0; j--) double_sum_down += 1D/j;
	Write("The sum going down using doubles becomes = {0}\n", double_sum_down);
	

	// part D
	WriteLine("\nPart D:");
	
	double a = 1.01;
       	double b = 1;
	bool truth = approx(a, b);
	Write("The truth value for {0} ~ {1} is {2}\n", a, b, truth);	


	double c = 1.999999999;
       	double d = 2;
	truth = approx(c, d);
	Write("The truth value for {0} ~ {1} is {2}\n", c, d, truth);	

	return 0;

	}
}
