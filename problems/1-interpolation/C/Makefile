all: Quickspline.svg Cspline.svg Integrated.svg Deriv.svg

TABVAL = tabval.txt
OUT = out.txt
EXACTFUNCS = exactfuncs.txt
NDATA = 10
XSTART = 0

Quickspline.svg: plotquick.gpi quickspline.txt $(OUT)
	gnuplot $<

quickspline.txt: $(TABVAL)
	spline $(TABVAL) > quickspline.txt

Deriv.svg: plotderivs.gpi $(OUT) $(EXACTFUNCS)
	gnuplot $<

Integrated.svg: plotintegrals.gpi $(OUT) $(EXACTFUNCS)
	gnuplot $<

Cspline.svg: plotcspline.gpi $(OUT) $(TABVAL) $(EXACTFUNCS)
	gnuplot $<

$(OUT): main.exe $(TABVAL)
	mono $< $(TABVAL) $(NDATA) >$@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

$(TABVAL) $(EXACTFUNCS): createtabval.exe Makefile
	mono $< $(TABVAL) $(EXACTFUNCS) $(XSTART) $(NDATA) 

createtabval.exe: createtabval.cs
	mcs $<

math.dll: cubicspline.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.dll *.exe *.svg *.txt
