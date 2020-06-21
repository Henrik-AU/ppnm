all: outA-B.txt PlotA.svg PlotC.svg PlotCExtra.svg
	cat outA-B.txt
	

PlotCExtra.svg: plotCExtra.gpi fitData.txt originalData.txt
	gnuplot $<

PlotC.svg: plotC.gpi fitData.txt originalData.txt
	gnuplot $<

PlotA.svg: plotA.gpi fitData.txt originalData.txt
	gnuplot $<

outA-B.txt originalData.txt fitData.txt: main.exe
	mono $<

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: matrix.cs vector.cs gramschmidt.cs lsfit.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt *.svg
