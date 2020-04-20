all: OutbreakUnhindered.svg OutbreakHindered.svg

OutbreakHindered.svg: plotHind.gpi out.txt
	gnuplot $<

OutbreakUnhindered.svg: plotUnhind.gpi out.txt
	gnuplot $<

out.txt: main.exe
	mono $< >$@ 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: vector.cs driver.cs rkstep45.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt *.svg Log
