using static System.Console;
using static vector3d;

class main{
	static void Main(){
		double a = 2;
		double b = 3;
		double c = 4;
		double d = 8;

		vector3d u = new vector3d(a, b, c);
		vector3d v = new vector3d(2*a, b, c);
		vector3d w = u + v;
		vector3d y = u - v;
		vector3d x = d*u;

		Write("The vector u is: ");
		Write(u);
		Write("The vector v is: ");
		Write(v);
		Write("The vector w = u + v is: ");
		Write(w);
		Write("The vector y = u - v is: ");
		Write(y);
		Write("The vector x = 8*u is: ");
		Write(x);


		WriteLine("\nAttempt at dot product:");
		double dot = dot_product(u, v);
		WriteLine("The dot product of u and v is = {0}", dot);
		WriteLine("Correct dot product: 33\n");

		vector3d cross = new vector3d(0, 0, 0);
		WriteLine("Attempt at cross product:");
		cross = cross_product(u, v);
		Write("The cross product of u and v is = {0}", cross);
		WriteLine("Correct cross product: (0, 8, -6)\n");

		WriteLine("Attempt at calculating the magnitude:");
		double magni = magnitude(u);
		double magniCorrect = 5.385;
		WriteLine("The magnitude of u is {0:f3}", magni);
		WriteLine("Correct magnitude: {0}\n", magniCorrect);


		WriteLine("Let's try to change the x-value of the u vector to 5");
		u.xval = 5;
		Write("The vector u is: ");
		Write(u);
		

		WriteLine("\nLet's try to read the z-value of the u vector");
		double zvalue = u.zval;
		Write("The z-value of the u-vector is: ");
		WriteLine("{0}", zvalue);
}

}
