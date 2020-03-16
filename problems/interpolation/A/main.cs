using static System.Console;
using static math;

class main{
	public static void Main(){

		// Generate som data points that we can test the linear spline on
		Func<double, double> f = (x) => x*x + x + 4;

		n = 5;
		xtabval = new double[n];
		ytabval = new double[n];
		for(int i=0; i<n; i++){
			xtabval[i] = i+1;
			ytabval[i] = f(i);
			WriteLine("{0}\t{1}", xtabval[i], ytabval[i]);
		}


		
}
}
