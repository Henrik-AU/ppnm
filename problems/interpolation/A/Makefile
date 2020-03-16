all: $(TABVAL) Linterp.svg Integralvalues.svg

TABVAL = tabval.txt
OUT = out.txt
NDATA = 10
XSTART = 0

Integralvalues.svg: plotintegrals.gpi $(OUT) $(TABVAL)
	gnuplot $<

Linterp.svg: plotlinterp.gpi $(OUT) $(TABVAL)
	gnuplot $<

$(OUT): main.exe $(TABVAL)
	mono $< $(TABVAL) $(NDATA) >$@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

$(TABVAL): createtabval.exe Makefile
	mono $< $(XSTART) $(NDATA) >$@

createtabval.exe: createtabval.cs
	mcs $<

math.dll: vector.cs linspline.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.svg *.txt
