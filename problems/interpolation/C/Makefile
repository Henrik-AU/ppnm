all: Quickspline.svg Cspline.svg Integralvalues.svg Deriv.svg

TABVAL = tabval.txt
OUT = out.txt
NDATA = 10
XSTART = 0

Quickspline.svg: quickspline.txt
	graph --output-format svg < quickspline.txt > Quickspline.svg

quickspline.txt: tabtest.txt
	spline tabtest.txt > quickspline.txt

Deriv.svg: plotderivs.gpi $(OUT) $(TABVAL)
	gnuplot $<

Integralvalues.svg: plotintegrals.gpi $(OUT) $(TABVAL)
	gnuplot $<

Cspline.svg: plotcspline.gpi $(OUT) $(TABVAL)
	gnuplot $<

$(OUT): main.exe $(TABVAL)
	mono $< $(TABVAL) $(NDATA) >$@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

$(TABVAL): createtabval.exe Makefile
	mono $< $(XSTART) $(NDATA) >$@

createtabval.exe: createtabval.cs
	mcs $<

math.dll: cubicspline.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.dll *.exe *.svg *.txt
