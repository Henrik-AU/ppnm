all: Plot.svg

Plot.svg: plot.gpi out.txt tabVal.txt
	gnuplot $<

out.txt tabVal.txt: main.exe
	mono $< >out.txt 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: subspline.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt Log *.svg
