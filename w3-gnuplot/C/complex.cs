// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System;
using SM=System.Math;
using System.Globalization;

public struct complex{

// data
private double re,im;
// getters
public double Re {get{return re;}}
public double Im {get{return im;}}
// constructors
public complex(double x){this.re = x; this.im = 0;}
public complex(double x, double y){ this.re=x; this.im=y; }
public static implicit operator complex(double x){return new complex(x);}
// useful numbers
public static readonly complex Zero = new complex(0,0);
public static readonly complex One  = new complex(1,0);
public static readonly complex I    = new complex(0,1);
// operator~
public static complex operator ~(complex a){
	return new complex(a.Re,-a.Im);}
// operator+
public static complex operator +(complex a){return a;}
public static complex operator +(complex a, double b){
	return new complex(a.Re+b,a.Im);}
public static complex operator +(double a, complex b){
	return new complex(a+b.Re,b.Im);}
public static complex operator +(complex a, complex b){
	return new complex(a.Re+b.Re,a.Im+b.Im);
	}
// operator-
public static complex operator-(complex a)
   { return new complex(-a.Re,-a.Im); }
public static complex operator-(complex a, double b)
   { return new complex(a.Re-b, a.Im); }
public static complex operator-(double a, complex b)
   { return new complex(a-b.Re, -b.Im); }
public static complex operator-(complex a, complex b)
   { return new complex(a.Re-b.Re, a.Im-b.Im); }
// operator*
public static complex operator*(complex a, double b)
   { return new complex(a.Re*b, a.Im*b); }
public static complex operator*(double a, complex b)
   { return new complex(a*b.Re, a*b.Im); }
public static complex operator*(complex a, complex b)
   { return new complex(a.Re*b.Re-a.Im*b.Im, a.Re*b.Im+a.Im*b.Re); }
// operator/
public static complex operator / (complex a, complex b){
	double ar=a.Re,ai=a.Im,br=b.Re,bi=b.Im;
	double t,absb;
	if(br>bi){t=bi/br; absb=br*Math.Sqrt(1+t*t);}
	else{t=br/bi; absb=bi*Math.Sqrt(1+t*t);}
	double s = 1.0/absb;
	double sbr = s*br, sbi=s*bi;
	double zr = (ar * sbr + ai * sbi) * s;
	double zi = (ai * sbr - ar * sbi) * s;
	return new complex(zr,zi);
	}

public static complex operator / (complex a, double x){
	return new complex(a.Re/x,a.Im/x);
	}
// ToString
public override string ToString() {
return String.Format(
	CultureInfo.CurrentCulture,
	"({0},{1})", this.Re, this.Im
	);
        }
public string ToString(string format) {
return String.Format(
	CultureInfo.CurrentCulture,
	"({0}, {1})",
	this.Re.ToString(format, CultureInfo.CurrentCulture),
	this.Im.ToString(format, CultureInfo.CurrentCulture)
	);
        }
public string ToString(IFormatProvider provider) {
return String.Format(
	provider,
	"({0}, {1})", this.Re, this.Im
	);
        }
public string ToString(string format, IFormatProvider provider) {
return String.Format(
	provider,
	"({0}, {1})",
	this.Re.ToString(format, provider),
	this.Im.ToString(format, provider)
	);
        }

// bool
public bool Equals(complex b){
	return this.Re.Equals(b.Re) && this.Im.Equals(b.Im);}
public override bool Equals(System.Object obj) {
      if (obj is complex)
      {
         complex b = (complex)obj;
         return this.Equals(b);
      }
      else { return false; }
   }
public override int GetHashCode(){
   throw new System.NotImplementedException(
         "complex.GetHashCode() is not implemented." );}

}// complex
