using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

public class ode{

public static vector rk23 // calls driver with rkstep23 stepper
(Func<double,vector,vector> F, // the ODE system to integrate
double a, // initial point
vector ya, // functions at the initial point
double b, // final point
List<double> xlist=null, // list of x's, if needed
List<vector> ylist=null, // list of y's, if needed
double acc=1e-3, // absolute accuracy goal
double eps=1e-3, // relative accuracy goal
double h=0.1, //initial step-size
int limit=999 // maximal number of steps
)
{
return driver(F,a,ya,b,acc,eps,h,rkstep23,xlist,ylist,limit);
}

public static
Func< Func<double,vector,vector>, double, vector, double, vector[]>
rkstep23 // Embedded Runge-Kutta stepper of the order 2-3
=delegate(Func<double,vector,vector> F, double x, vector y, double h)
{
// One of the Runge-Kutta 2-3 embedded methods
	const double s1=1.0/2,s2=3.0/4;
	const double a10=1.0/2;
	const double a20=0,a21=3.0/4;
	const double A0=2.0/9,A1=1.0/3,A2=4.0/9;
	const double B0=0,B1=1,B2=0;
/*
	const double s1=1.0/3,s2=2.0/3;
	const double a10=1.0/3;
	const double a20=0,a21=2.0/3;
	const double A0=1.0/4,A1=0,A2=3.0/4;
	const double B0=0,B1=1.0/2,B2=1.0/2;
*/

	vector k0 = F(x,y);
	vector k1 = F(x+s1*h,y+k0*(a10*h));
	vector k2 = F(x+s2*h,y+k0*(a20*h)+k1*(a21*h));
	vector ka = (k0*A0+k1*A1+k2*A2);
	vector kb = (k0*B0+k1*B1+k2*B2);
	vector yh = y+ka*h;
	vector er = (ka-kb)*h;
	vector[] result={yh,er};
	return result;
};

public static vector[] rkstep12
(Func<double,vector,vector> F, double x, vector y0, double h)
// Runge-Kutta mid-point step
{
	vector k0  = F(x,y0);
	vector k12 = F(x+h/2,y0+k0*(h/2));
	vector yh=y0+k12*h;
	vector er =(k12-k0)*h;
	vector[] result={yh, er};
	return result;
}

public static vector driver
(Func<double,vector,vector> F, double a, vector ya, double b,
double acc, double eps, double h, 
Func< Func<double,vector,vector>, double, vector, double, vector[] > stepper,
List<double> xlist=null, List<vector> ylist=null, int limit=999)
{// Generic ODE driver
	int nsteps=0; vector[] trial;
	if(xlist!=null) xlist.Add(a);
	if(ylist!=null) ylist.Add(ya);
	do{
		if(a>=b) return ya;
		if(nsteps>limit) {
			Error.Write($"ode.driver: nsteps>{limit}\n");
			return ya;
			}
		if(a+h>b) h=b-a;
		trial=stepper(F,a,ya,h);
/*
		if(nsteps==0) trial=rkstep23(F,a,ya,h);
		else trial=twostep(F,a,ya,h,yo,ho);
*/
		vector   yh=trial[0];
		vector   er=trial[1];
		vector tol = new vector(er.size);
		for(int i=0; i<tol.size; i++){
			tol[i]=Max(acc,Abs(yh[i])*eps)*Sqrt(h/(b-a));
			if(er[i]==0)er[i]=tol[i]/4;
			}
		double factor=tol[0]/Abs(er[0]);
		for(int i=1; i<tol.size; i++)
			if(tol[i]/Abs(er[i])<factor) factor=tol[i]/Abs(er[i]);
	double hnew=h*Pow(factor,0.25)*0.95;
	if(hnew>2*h) hnew=2*h;
	int ok=1; for(int i=0;i<tol.size;i++)if(Abs(er[i])>tol[i])ok=0;
	if(ok==1){
		a+=h; ya=yh; h=hnew; nsteps++;
		if(xlist!=null) xlist.Add(a);
		if(ylist!=null) ylist.Add(ya);
		}
	else {
		Error.WriteLine($"bad step: x={a}");
		h=hnew;
		}
	}while(true);
	}

public static vector[] twostep
(Func<double,vector,vector> F, double x, vector y, double h, vector yo, double ho)
{// Two step stepper
	vector k1=F(x,y);
	vector c =(yo-y+k1*ho)/(ho*ho);
	vector yp=y+k1*h+c*(h*h);
	vector k2=F(x+h,yp);
	vector d =(k2-k1-c*(2*h))/(2*h*(h+ho)-h*h);
	vector dy=d*(h*h*(h+ho));
	vector yc=yp+dy;
	vector[] result={yc,dy};
	return result;
}

}//ode
