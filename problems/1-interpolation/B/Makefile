all: Qspline.svg Integrated.svg Deriv.svg

TABVAL = tabval.txt
OUT = out.txt
EXACTFUNCS = exactfuncs.txt
NDATA = 10
XSTART = 0

Deriv.svg: plotderivs.gpi $(OUT) $(EXACTFUNCS)
	gnuplot $<

Integrated.svg: plotintegrals.gpi $(OUT) $(EXACTFUNCS)
	gnuplot $<

Qspline.svg: plotqspline.gpi $(OUT) $(TABVAL) $(EXACTFUNCS)
	gnuplot $<

$(OUT): main.exe $(TABVAL)
	mono $< $(TABVAL) $(NDATA) >$@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

$(TABVAL) $(EXACTFUNCS): createtabval.exe Makefile
	mono $< $(TABVAL) $(EXACTFUNCS) $(XSTART) $(NDATA) >$@

createtabval.exe: createtabval.cs
	mcs $<

math.dll: quadspline.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.dll *.exe *.svg *.txt
