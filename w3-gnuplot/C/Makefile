all: cgamma-out.txt ComplexGamma.png

ComplexGamma.png: cgamma.gpi cgamma-out.txt
	gnuplot $<

cgamma-out.txt: main.exe
	mono $< > $@

main.exe: main.cs cmath.dll
	mcs $< -reference:cmath.dll

cmath.dll: cgamma.cs cmath.cs complex.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.txt *.dll *.exe *.png
