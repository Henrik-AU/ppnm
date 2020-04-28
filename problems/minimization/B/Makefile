all: PlotHiggs.svg

PlotHiggs.svg: plot.gpi higgsFit.txt higgsData.txt
	gnuplot $<

out.txt higgsFit.txt: main.exe
	mono $< higgsData.txt >out.txt 2> Log

main.exe: main.cs math.dll higgsData.txt
	mcs $< -reference:math.dll

math.dll: vector.cs qnewton.cs matrix.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe higgsFit.txt out.txt Log
