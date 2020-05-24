all: Plot.svg

Plot.svg: plot.gpi outData.txt
	gnuplot $<

outData.txt: main.exe
	mono $< >$@ 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: neural.cs vector.cs matrix.cs qnewton.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.txt *.dll *.exe Log *.svg
