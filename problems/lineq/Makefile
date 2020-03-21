all: outA.txt outB.txt outC.txt

outC.txt: mainC.exe
	mono $< > $@

mainC.exe: mainC.cs math.dll
	mcs $< -reference:math.dll

outB.txt: mainB.exe
	mono $< > $@

mainB.exe: mainB.cs math.dll
	mcs $< -reference:math.dll


outA.txt: mainA.exe
	mono $< > $@

mainA.exe: mainA.cs math.dll
	mcs $< -reference:math.dll

math.dll: matrix.cs vector.cs gramschmidt.cs givens.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt
