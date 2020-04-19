

public partial class ode{

	public static vector[] 
	rkstep45(Func<double,vector,vector> f, double t, vector yt, double h){
	// Let's go with the Dormand Prince method, which is similar to the Runge-Kutta-Fehlberg	
	vector k0 = f(x, y);
	vector k1 = f(x+h/5, y + k0/5);
	vector k2 = f(x+3*h/10, y + 3*k0/40 + 9*k1/40);
	vector k3 = f(x+4*h/5, y + 44*k0/45 - 56*k1/15 + 32*k2/9);
	vector k4 = f(x+8*h/9, y + 19372*k0/6561 - 25360*k1/2187 + 64448*k2/6561 -212*k3/729);
	vector k5 = f(x+h, y + 9017*k0/3168 - 355*k1/33 + 46732*k2/5247 + 49*k3/176 -5103*k4/18656);
	vector k6 = f(x+h, y + 35*k0/384 + 500*k2/1113 + 125*k3/192 - 2187*k4/6784 + 11*k5/84);
	vector ka = 35*k0/384 + 500*k2/1113 + 125*k3/192 - 2187*k4/6784 + 11*k5/84;
	vector kb = 5179*k0/57600 + 7571*k2/16695 + 393*k3/640 - 92097*k4/339200 + 187*k5/2100 + k6/40;
	vector yh = y+ka*h;
	vector err = (ka - kb) * h;
	return new vector[] {yh, err};

	}
}
