// This is part B of the exercise "input/output".
using System;
using System.IO;

class readstdin{
	static int Main(){
	
	// We connect stdin to the input stream and stdout to the output stream
	TextReader stdin = Console.In;
	TextWriter stdout = Console.Out;
	
	// We can use a do-while loop to read a line from the input stream, calculate the
	// sine and cosine, display the values, and then repeat until an empty line is read
	// A do-while loop checks the condition at the end of each loop.

	stdout.WriteLine("x \t sin(x) \t cos(x)");
	do{
		// Read a string from the input stream
		string s = stdin.ReadLine();
		if (s == null) break;
		
		// Create an array of strings with the entries (numbers) from the input
		// stream. 'Split' splits the string into new strings as specified by the
		// delimiter/delimiters.
		string[] entries = s.Split(' ');
		
		// Convert each entry to a double and calculate Sin, Cos, and print it.
		foreach(var entry in entries){
			double x = double.Parse(entry);
			stdout.WriteLine("{0} {1} {2}", x, Math.Sin(x), Math.Cos(x));
		}

	}
	while(true);
	
	return 0;
}

}
