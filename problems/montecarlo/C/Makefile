all: out.txt PlotDistribution.svg

PlotDistribution.svg: plot.gpi stratPoints.txt
	gnuplot $<

out.txt stratPoints.txt: main.exe
	mono $< >out.txt

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: stratified.cs vector.cs montecarlo.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.txt *.exe *.dll *.svg
