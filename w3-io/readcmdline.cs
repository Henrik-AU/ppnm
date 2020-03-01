// This is part A of the exercise "input/output".
using System;

class readcmdline{
	// We let the main function take an array of strings as input
	static int Main(string[] args){

	// We loop over all the strings in the input array, convert the string
	// to doubles using Parse, and then print out the value and the sine and cosine of
	// them also.
	Console.WriteLine("x \t sin(x) \t cos(x)");
	foreach(var s in args){
		double x = double.Parse(s);
		Console.WriteLine("{0} {1} {2}", x, Math.Sin(x), Math.Cos(x));
	}
	

	return 0;	
}


}
