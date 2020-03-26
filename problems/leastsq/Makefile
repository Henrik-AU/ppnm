all: fit.txt FitPlot.svg
	cat fit.txt

FitPlot.svg: fitPlot.gpi fitData.txt originalData.txt
	gnuplot $<

fit.txt originalData.txt fitData.txt: main.exe
	mono $<

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: matrix.cs vector.cs gramschmidt.cs lsfit.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt *.svg
