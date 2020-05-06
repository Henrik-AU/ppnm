all: Plot.svg

Plot.svg: plot.gpi out.txt
	gnuplot $<

out.txt: main.exe
	mono $< >$@ 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: neural.cs activation.cs vector.cs matrix.cs qnewton.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.txt *.dll *.exe Log *.svg
