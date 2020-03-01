// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System;
public static class cmath{

public static void print(this double x,string s){ System.Console.WriteLine(s+x); }
public static void print(this double x)         { x.print(""); }
public static void print(this complex z)
	{System.Console.WriteLine("{0} {1}",z.Re,z.Im);}
public static void print(this complex z, string s)
	{System.Console.WriteLine(s+z);}
public static void printf(this complex z,string s)
	{System.Console.WriteLine(s,z.Re,z.Im);}


public static double  exp(double x) {return Math.Exp(x);}
public static complex exp(complex z){
	return exp(z.Re)*(new complex(cos(z.Im),sin(z.Im)));
	}

public static double  sin(double x ){return Math.Sin (x);}
public static complex sin(complex z){
	var I=new complex(0,1);
	return (exp(I*z)-exp(-I*z))/2/I;
	}

public static double  cos(double x){return Math.Cos (x);}
public static complex cos(complex z){
	var I = complex.I;
	return (exp(I*z)+exp(-I*z))/2;
	}

public static double abs (double x ){return Math.Abs (x);}
public static double abs (complex z){
	double x=abs(z.Re),y=abs(z.Im),t;
	if(x>y){ t=y/x; return x*sqrt(1+t*t); }
	else   { t=x/y; return y*sqrt(t*t+1); }
	}

public static double  log(double x ){return Math.Log (x);}
public static complex log(complex z){
	return new complex( log(abs(z)), arg(z) ); }

public static double sqrt(double x){return Math.Sqrt(x);}
public static double arg(complex z){return Math.Atan2(z.Im,z.Re);}

public static double  pow (this double x, double y){return Math.Pow (x,y);}
public static double  pow (this double x, int n   ){return Math.Pow (x,n);}
public static complex pow (this complex a, double x){
	return exp(x*log(a)); }
public static complex pow (this complex a, complex b){
	return exp(b*log(a)); }

}// cmath
