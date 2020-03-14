all: lngamma-data.txt Gamma.svg

Gamma.svg: lngamma.gpi lngamma-data.txt
	gnuplot $<

lngamma-data.txt: main.exe
	mono $< > $@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: lngamma.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.txt *.dll *.exe *.svg
