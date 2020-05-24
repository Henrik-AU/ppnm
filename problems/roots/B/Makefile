all: Swave.svg

Swave.svg: plot.gpi out.txt
	gnuplot $<

out.txt outRootText.txt Log: main.exe
	mono $< >$@ 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: vector.cs gramschmidt.cs newton.cs matrix.cs rkstep45.cs driver.cs hydrogen.cs cubicspline.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt Log *.svg
