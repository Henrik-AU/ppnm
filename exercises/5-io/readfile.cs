// This is part C of the exercise "input/output".
using System;
using System.IO;

class readfile{
	// Takes two inputs - the input file name and the output file name
	static int Main(string[] args){
	if (args.Length < 2) return 1;
	
	// The first argument will be the input file, while the second argument will be the
	// output file
	string filein = args[0];
	string fileout = args[1];
	
	// We create two new streams attached to the input file and the output file respectively
	StreamReader streamin = new StreamReader(filein);
	StreamWriter streamout = new StreamWriter(fileout);

	// We write an initial line to the output file specifying the content of the columns
	streamout.WriteLine("x \t sin(x) \t cos(x)");

	// We now do a loop to go through all the input values, calculating the corresponding
	// sine and cosine
	do{
		string line = streamin.ReadLine();
		// Cancel loop if the line is empty
		if(line==null) break;
		
		// Split the string into an array of strings with each number in the line,
		// assuming that they are sepatated by normal spaces if more that one number per		// line is provided.
		string[] numbers = line.Split(' ');
		foreach(var number in numbers){
			// We convert each number from string to double, and then write out the
			// number along with the sine and cosine
			double x = double.Parse(number);
			streamout.WriteLine("{0} {1} {2}", x, Math.Sin(x), Math.Cos(x));
			}
		
	
	}while(true);
	
	// We close the input and output streams for good measure.
	streamin.Close();
	streamout.Close();

	return 0;
}

}
