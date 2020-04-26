all: Convergence.svg

Convergence.svg: plot.gpi out.txt outP.txt
	gnuplot $<

outP.txt: main.exe
	mono $< 2 precise 2>Log
	mono $< 4 precise 2>Log
	mono $< 6 precise 2>Log
	mono $< 8 precise 2>Log

out.txt: main.exe
	mono $< 2 print 2>Log
	mono $< 4 2>Log
	mono $< 6 2>Log
	mono $< 8 2>Log
	mono $< 10 2>Log
	mono $< 12 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: vector.cs gramschmidt.cs newton.cs matrix.cs rkstep45.cs driver.cs hydrogen.cs cubicspline.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt Log
