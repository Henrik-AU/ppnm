all: PlotA.svg PlotB.svg

PlotB.svg: plotB.gpi outB.txt
	gnuplot $<

PlotA.svg: plotA.gpi outA.txt
	gnuplot $<

outB.txt: mainB.exe
	mono $< >$@ 2>LogB

mainB.exe: mainB.cs math.dll
	mcs $< -reference:math.dll

outA.txt: mainA.exe
	mono $< >$@ 2>LogA

mainA.exe: mainA.cs math.dll
	mcs $< -reference:math.dll

math.dll: neural.cs activation.cs vector.cs matrix.cs qnewton.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.txt *.dll *.exe *Log *.svg
