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
		Write("The vector w is: ");
		Write(w);
		Write("The vector y is: ");
		Write(y);
		Write("The vector x is: ");
		Write(x);



		Write("Attempt at dot product:\n");
		double dot = dot_product(u, v);
		Write("The dot product of u and v is = {0}\n", dot);

		vector3d cross = new vector3d(a, b, c);
		Write("Attempt at cross product:\n");
		cross = cross_product(u, v);
		Write("The cross product of u and v is = {0}\n", cross);

		Write("Attempt at calculating the magnitude:\n");
		double magni = magnitude(u);
		Write("The magnitude of u is = {0}\n", magni);


		Write("Let's try to change the x-value of the u vector to 5\n");
		u.xval = 5;
		Write("The vector u is: ");
		Write(u);
		

		Write("Let's try to read the z-value of the u vector\n");
		double zvalue = u.zval;
		Write("The z-value of the u-vector is: ");
		Write("{0}\n", zvalue);


}

}
