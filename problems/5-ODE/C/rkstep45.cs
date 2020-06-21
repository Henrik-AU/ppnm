using System;

public partial class ode{

	public static vector[] 
	rkstep45(Func<double,vector,vector> f, double x, vector y, double h){
	// Let's go with the Dormand-Prince method, which is similar to the Runge-Kutta-Fehlberg	

	vector k0 = f(x, y);
	vector k1 = f(x+h/5, y + 1.0*k0*h/5);
	vector k2 = f(x+3*h/10, y + 3.0*k0*h/40 + 9.0*k1*h/40);
	vector k3 = f(x+4*h/5, y + 44.0*k0*h/45 - 56.0*k1*h/15 + 32.0*k2*h/9);
	vector k4 = f(x+8*h/9,
		y + 19372.0*k0*h/6561 - 25360.0*k1*h/2187 + 64448.0*k2*h/6561 -212.0*k3*h/729);
	vector k5 = f(x+h,
		y + 9017.0*k0*h/3168 - 355.0*k1*h/33 + 46732.0*k2*h/5247 + 49.0*k3*h/176
		- 5103.0*k4*h/18656);
	vector k6 = f(x+h,
		y + 35.0*k0*h/384 + 500.0*k2*h/1113 + 125.0*k3*h/192 - 2187.0*k4*h/6784 + 11.0*k5*h/84);

	vector ka = 35.0*k0/384 + 500.0*k2/1113 + 125.0*k3/192 - 2187.0*k4/6784 + 11.0*k5/84;
	vector kb = 5179.0*k0/57600 + 7571.0*k2/16695 + 393.0*k3/640 - 92097.0*k4/339200
		+ 187.0*k5/2100 + 1.0*k6/40;

	vector yh = y+ka*h;
	vector err = (ka - kb) * h;
	return new vector[] {yh, err};

	}
}
