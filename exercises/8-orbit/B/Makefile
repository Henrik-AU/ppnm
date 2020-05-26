all: circle.txt PlotB1.svg PlotB2.svg PlotB3.svg

PlotB3.svg: plotB3.gpi outB3.txt
	gnuplot $<

PlotB2.svg: plotB2.gpi outB2.txt
	gnuplot $<

PlotB1.svg: plotB1.gpi circle.txt outB1.txt
	gnuplot $<

circle.txt outB1.txt outB2.txt outB3.txt: main.exe
	mono $< > circle.txt 2> log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: vector.cs ode.cs
	mcs $^ -target:library -out:$@ 

clean:
	rm -f *.txt *.dll *.exe *.svg *log
