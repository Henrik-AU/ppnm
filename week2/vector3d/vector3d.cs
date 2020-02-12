using static System.Math;

public struct vector3d{
	
	// We know that the vector will be 3D, so it is faster to just use
	// normal double variables instead of arrays.
	private double x, y, z;

	public double xval{
		get{return x;}
		set{x = value;}
	}
	public double yval{
		get{return y;}
		set{y = value;}
	}
	public double zval{
		get{return z;}
		set{z = value;}
	}


	// The constructor - is called when vector3d(...) is used.
	public vector3d(double a, double b, double c){x = a; y = b; z = c;}


	// We can overwrite the ToString method, changing what is printed when
	// calling Write([name of vector3d object])
	public override string ToString(){
		return $"({this.x}, {this.y}, {this.z})\n";	
	}


	// Operators
	// Multiplication
	public static vector3d operator*(vector3d v, double c){
		return new vector3d(c*v.x, c*v.y, c*v.z);
	}

	// Multiplication in opposite order
	public static vector3d operator*(double c, vector3d v){
		return v*c;
	}
	
	// Addition
	public static vector3d operator+(vector3d u, vector3d v){
		double i = u.x + v.x;
		double j = u.y + v.y;
		double k = u.z + v.z;
		vector3d w = new vector3d(i, j, k);
		return w;
	}

	// Subtraction
	public static vector3d operator-(vector3d u, vector3d v){
		double i = u.x - v.x;
		double j = u.y - v.y;
		double k = u.z - v.z;
		vector3d w = new vector3d(i, j, k);
		return w;
	
	}


	// Methods
	// Dot product
	public static double dot_product(vector3d u, vector3d v){
		double dot_product_value = u.x*v.x + u.y*v.y + u.z*v.z;
		return dot_product_value;

	}


	// Cross product
	public static vector3d cross_product(vector3d u, vector3d v){
		double i = u.y*v.z - u.z*v.y;
		double j = u.z*v.x - u.x*v.z;
		double k = u.x*v.y - u.y*v.x;
		vector3d w = new vector3d(i, j, k);
		return w;

	}
	
	// Magnitude
	public static double magnitude(vector3d u){
		double magnitude = Sqrt(u.x*u.x + u.y*u.y + u.z*u.z);
		return magnitude;
	}	

}
