all: out.txt Plot.svg

Plot.svg: plot.gpi stratPoints.txt
	gnuplot $<

out.txt stratPoints.txt: main.exe
	mono $< >out.txt

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: stratified.cs vector.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.txt *.exe *.dll
