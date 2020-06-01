using System;
using System.IO;
using static System.Math;
using static System.Console;
using System.Collections.Generic;



class main{

	static List<double> energy;
	static List<double> signal;
	static List<double> error;
	
	static void Main(string[] args){
	
		// The input argument is the name of the file with the Higgs data
		string HiggsData = args[0];

		// Load the experimental data into lists
		energy = new List<double>();
		signal = new List<double>();
		error = new List<double>();

		StreamReader filein = new StreamReader(HiggsData);
		do{
			// Read the lines one at a time
			string s = filein.ReadLine();
		
			// When we get an empty line we are done
			if(s==null){
				break;
			}

			// Split the data into individual strings
			string[] data = s.Split(' ');
			energy.Add(double.Parse(data[0]));
			signal.Add(double.Parse(data[1]));
			error.Add(double.Parse(data[2]));

		}while(true);
		
		filein.Close();

		// Next step is to do minimization of the chi-square fit. We create a start guess
		// based on what is roughly expected for the Higgs particle.
		// First two entries are the expected mass and width, while the third entry is the A
		// proportionality constant in the Breit-Wigner formula
		vector x = new vector(130, 1, 1);

		int nsteps = minimization.qnewton(chi2, ref x);

		double m = x[0];
		double w = x[1];
		double A = x[2];

		WriteLine("Fitting Higgs data to Breit-Wigner formula via chi^2.");
		WriteLine("Found mass: \t\t\t{0}", m);
		WriteLine("Found width: \t\t\t{0}", w);
		WriteLine("Found A-constant: \t\t{0}", A);
		WriteLine("Reduced chi^2 value: \t\t{0}", chi2(x)/energy.Count);
		WriteLine("Minimization steps: \t\t{0}", nsteps);
		WriteLine("\nA reduced chi^2 value below 1 indicates that it is a good fit.");

		// Write out data for the fitted curve, such that it can be plotted
		StreamWriter writeFit = new StreamWriter("higgsFit.txt");
		double de = 0.25;
		for(double e = energy[0]; e<=energy[energy.Count - 1]; e+=de){
			writeFit.WriteLine("{0} {1}", e, A*breitwigner(e, m, w));
		}
		writeFit.Close();

	} // end Main

	// Breit-Wigner formula without the proportionality constant A (we will use that one
	// as a fitting parameter)
	static double breitwigner(double energy, double mass, double width){
		return 1/(Pow(energy-mass, 2) + width*width/4);
	} // end Breit Wigner

	// Chi-square function
	static double chi2(vector x){
		// Chi^2 is defined as a sum over the square of the difference between measurement
		// and the assumed distribution, divided by the error
		double mass = x[0];
		double width = x[1];
		double A = x[2];
		double sum = 0;
		int n = energy.Count;
		for(int i=0; i<n; i++){
			double e = energy[i];
			double s = signal[i];
			double err = error[i];
			sum += Pow((s - A*breitwigner(e, mass, width))/err ,2);
		}
		Error.WriteLine("The reduced chi^2 is: {0}", sum/n);
		return sum;
	} // end chi^2		
} // end class
