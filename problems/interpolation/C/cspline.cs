using System;
using System.Diagnostics;
public class cspline{

double[] x,y,b,c,d;

public static int binsearch(double[] x, double z){
	int i=0, j=x.Length-1;/* locate the interval for z by bisection */ 
	while(j-i>1){ int mid=(i+j)/2; if(z>x[mid]) i=mid; else j=mid;}
	return i;
	}

public cspline(double[] xs,double[] ys){
	int n=xs.Length; Trace.Assert(ys.Length>=n);
	x=new double[n];
	y=new double[n];
	b=new double[n];
	c=new double[n-1];
	d=new double[n-1];
	for(int i=0;i<n;i++){x[i]=xs[i];y[i]=ys[i];}
	var D = new double[n];
	var Q = new double[n-1];
	var B = new double[n];
	var h = new double[n-1];
	var p = new double[n-1];
	for(int i=0;i<n-1;i++) { h[i]=x[i+1]-x[i]; Trace.Assert(h[i]>0);}
	for(int i=0;i<n-1;i++) { p[i]=(y[i+1]-y[i])/h[i]; }

	D[0]=2; Q[0]=1; B[0]=3*p[0]; D[n-1]=2; B[n-1]=3*p[n-2];
	for(int i=0;i<n-2;i++){
   		D[i+1]=2*h[i]/h[i+1]+2;
   		Q[i+1]=h[i]/h[i+1];
   		B[i+1]=3*(p[i]+p[i+1]*h[i]/h[i+1]);
		}

	for(int i=1;i<n;i++){
		D[i]-=Q[i-1]/D[i-1];
		B[i]-=B[i-1]/D[i-1];
		}

	b[n-1]=B[n-1]/D[n-1];
	for(int i=n-2;i>=0;i--)
		b[i]=(B[i]-Q[i]*b[i+1])/D[i];

	for(int i=0;i<n-1;i++){
   		c[i]=(-2*b[i]-b[i+1]+3*p[i])/h[i];
   		d[i]=(b[i]+b[i+1]-2*p[i])/h[i]/h[i];
		}
}//construcntor

public double eval(double z){/* evaluation of the spline at point z */
	Trace.Assert(z>=x[0] && z<=x[x.Length-1]);
	int i=binsearch(x,z);
	double dx=z-x[i];/* calculate the inerpolating spline : */
	return y[i]+dx*(b[i]+dx*(c[i]+dx*d[i]));
	}

public double deriv(double z){/* derivative of the spline at point z */
	Trace.Assert(z>=x[0] && z<=x[x.Length-1]);
	int i=binsearch(x,z);
	double dx=z-x[i];
	return b[i]+dx*(2*c[i]+dx*3*d[i]);
	}

public double integrate(double z){/* derivative of the spline at point z */
	Trace.Assert(z>=x[0] && z<=x[x.Length-1]);
	int iz=binsearch(x,z);
	double sum=0,dx;
	for(int i=0;i<iz;i++){
		dx=x[i+1]-x[i];
		sum+=dx*(y[i]+dx*(b[i]/2+dx*(c[i]/3+dx*d[i]/4)));
		}
	dx=z-x[iz];
	sum+=dx*(y[iz]+dx*(b[iz]/2+dx*(c[iz]/3+dx*d[iz]/4)));
	return sum;
	}

}//cspline
