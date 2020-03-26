all: fit.txt originalData.txt
	cat fit.txt

fit.txt originalData.txt: main.exe
	mono $<

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: matrix.cs vector.cs gramschmidt.cs lsfit.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt
