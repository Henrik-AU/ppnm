all: out.txt Eigenfunctions.svg
	cat out.txt
	cat energies.txt

Eigenfunctions.svg: plotEigenfuncs.gpi boxData.txt
	gnuplot $<

out.txt energies.txt boxData.txt: main.exe
	mono $< >out.txt 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: jacobi.cs matrix.cs vector.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.exe *.txt *.dll *.svg Log
