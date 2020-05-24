all: Plot.svg

Plot.svg: plot.gpi out.txt
	gnuplot $<

out.txt: main.exe
	mono $< 1>$@ 2>log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: vector.cs ode.cs
	mcs $^ -target:library -out:$@ 

clean:
	rm -f *.txt *.dll *.exe *.svg *log
