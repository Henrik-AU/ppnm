all: Orbits.svg

Orbits.svg: plotOrbits.gpi out.txt
	gnuplot $<

out.txt: main.exe
	mono $< >$@ 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: vector.cs driver.cs rkstep45.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt *.svg Log
