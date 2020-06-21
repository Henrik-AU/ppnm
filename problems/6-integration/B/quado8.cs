// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System; using static System.Math; using static System.Double;
public static partial class quad{

public static double o8av
(Func<double,double> f, double a, double b, double acc=1e-6, double eps=1e-6)
{
/// Variable substitutions to treat infinite limits
/// and singularities at end-points.
if(a>b) return -o8av(f,b,a,acc,eps);
if(IsNegativeInfinity(a) && !IsInfinity(b))
	return o8av(t=>f(b-(1-t)/t)/t/t,0,1,acc,eps);
if(!IsInfinity(a) && IsPositiveInfinity(b))
	return o8av(t=>f(a+(1-t)/t)/t/t,0,1,acc,eps);
if(IsNegativeInfinity(a) && IsPositiveInfinity(b))
	return o8av(f,a,0,acc,eps)+o8av(f,0,b,acc,eps);
return o8a(t=>f(a+(b-a)*(3*t*t-2*t*t*t))*(b-a)*6*(t-t*t) ,0,1,acc,eps);
//return o8a(t=>f((a+b)/2+(b-a)/2*Cos(t))*Sin(t)*(b-a)/2,0,PI,acc,eps);
}//o8av

public static double o8a
(Func<double,double> f,double a,double b,
double acc=1e-6,double eps=1e-6,
double f2=NaN,double f3=NaN, double f6=NaN,double f7=NaN,
int nrec=0,int limit=10)
{
/// Open 8-point Aadaptive integrator with reuse of points.
double h=b-a,sqr2=Sqrt(2);
double f1=f(a+1*h/12),f4=f(a+5*h/12),f5=f(a+7*h/12),f8=f(a+11*h/12);
if(IsNaN(f2))
      {f2=f(a+2*h/12);f3=f(a+4*h/12);f6=f(a+8*h/12);f7=f(a+10*h/12);nrec=0;}
const double
w1=4738.0/19845,w2=-59.0/567,w3=5869.0/13230,w4=-74.0/945, 
u1=208.0/945,u2=-7.0/135,u3=209.0/630,u4=0,
w5=w4,w6=w3,w7=w2,w8=w1,
u5=u4,u6=u3,u7=u2,u8=u1;
double integral= (w1*f1+w2*f2+w3*f3+w4*f4+w5*f5+w6*f6+w7*f7+w8*f8)*h;
double approx  = (u1*f1+u2*f2+u3*f3+u4*f4+u5*f5+u6*f6+u7*f7+u8*f8)*h;
double error=Abs(integral-approx)/1.5;
double tolerance=acc+eps*Abs(integral);
if(error<tolerance) return integral;
else if(++nrec>limit){
	Console.Error.Write($"o8a: nrec>{limit} a={a} b={b}\n");
	return integral;
	}
else return o8a(f,a,(a+b)/2,acc/sqr2,eps,f1,f2,f3,f4,nrec,limit)+
		o8a(f,(a+b)/2,b,acc/sqr2,eps,f5,f6,f7,f8,nrec,limit);
}//o8a

public static double o8acc
(Func<double,double> f, double a, double b, double acc=1e-3, double eps=1e-3)
{
/// Clenshaw-Curtis variable substitution
        Func<double,double> F = t => f((a+b)/2+(b-a)/2*Cos(t))*Sin(t)*(b-a)/2;
        return o8a(F,0,PI,acc,eps);
}//o8acc

}//quad

/*
const double
w1 = 0.2424792139077853, w2 =-0.1171075837742504, w3 = 0.499546485260771,
w4 =-0.1566137566137566, w5=0,
w6 = 0.3876795162509448, w7 =-0.091005291005291, w8 = 0.2350214159737969;
const double
u1 = 0.2350214159737969, u2 =-0.091005291005291, u3 = 0.3876795162509448,
u4=0, u5 = -0.1566137566137566,
u6 = 0.499546485260771, u7 = -0.1171075837742504, u8 = 0.2424792139077853;
double approx1= (w1*f1+w2*f2+w3*f3+w4*f4+w5*f5+w6*f6+w7*f7+w8*f8)*h;
double approx2= (u1*f1+u2*f2+u3*f3+u4*f4+u5*f5+u6*f6+u7*f7+u8*f8)*h;
double integral=(approx1+approx2)/2;
double error=Abs(approx1-approx2);
double tolerance=acc+eps*Abs(integral);
*/
