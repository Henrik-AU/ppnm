using static System.Math;
using static System.Console;
using static Approx; //Has to be spelled as the class, not the name of the DLL
		     //or the name of the function! So here with upper case A.

class main{
	static int Main(){

// part A

/*
	int i=1;
	while(i+1>1) {i++;}
	Write("My max int = {0}\n", i);

	Write("The int.MaxValue command says = {0}\n", int.MaxValue);


	i=1;
	while(i-1<1) {i--;}
	Write("My min int = {0}\n", i);

	Write("The int.MinValue command says = {0}\n", int.MinValue);

*/

// part B
//
	double x = 1;
	while(1+x!=1){x/=2;}

 // When the loop breaks x is so small that the computer cannot see any difference between 1 and 1+x. The smallest interval that
 // the computer could distinguish is thus the previous x-value. We therefore multiply it by two before printing it.	
	x*=2;

	Write("The machine epsilon with double is = {0}\n", x);

	float y=1F;
	while((float)(1F+y) != 1F){y/=2F;}

 // When the loop breaks y is so small that the computer cannot see any difference between 1 and 1+y. The smallest interval that
 // the computer could distinguish is thus the previous y-value. We therefore multiply it by two before printing it.	
       	y*=2F;

	Write("The machine epsilon with float is = {0}\n", y);

// According to Dimitry the machine epsilon for a double precision machince should be around System.Math.Pow(2, -52).
	
	double machineepsilon = Pow(2, -52);
	Write("Dimitry says the machince epsilon should be around = {0}\n", machineepsilon);




// part C

	int max = int.MaxValue/3; // I get the same value for 2 or 3

	float float_sum_up = 1F;
	for(int i = 2; i < max; i++) float_sum_up+=1F/i;
	Write("The sum going up becomes = {0}\n", float_sum_up);


	float float_sum_down = 1F/max;
	for(int i =max-1;i>0;i--) float_sum_down +=1F/i;
	Write("The sum going down becomes = {0}\n", float_sum_down);
	


	// Is the discrepancy in the sums due to rounding errors? In the sum going down I guess we are starting with very small numbers and then
	// doing rounding errors on these many more times than when summing in the other direction?
	

	// Using doubles

	double double_sum_up = 1D;

	for(int i = 2; i < max; i++) double_sum_up+= 1D/i;
	Write("The sum going up becomes = {0}\n", double_sum_up);


	double double_sum_down = 1D/max;
	for(int i = max-1; i > 0; i--) double_sum_down += 1D/i;
	Write("The sum going down becomes = {0}\n", double_sum_down);
	

// part D
//
	
	double a = 1;
       	double b = 1;
	bool truth = approx(a, b);
	Write("The truth value is {0}\n", truth);	



	return 0;

	}
}
