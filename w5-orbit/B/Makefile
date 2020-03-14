all: out PlotBpartI.svg PlotBpartII.svg PlotBpartIII.svg

PlotBpartIII.svg: plotBpartIII.gpi outBpartIII.txt
	gnuplot $<

PlotBpartII.svg: plotBpartII.gpi outBpartII.txt
	gnuplot $<

PlotBpartI.svg: plotBpartI.gpi outBpartI.txt
	gnuplot $<

out: main.exe
	mono $< 2> log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: vector.cs ode.cs
	mcs $^ -target:library -out:$@ 

clean:
	rm -f *.txt *.dll *.exe *.svg *log
