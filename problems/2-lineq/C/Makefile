all: out.txt
	cat out.txt

out.txt: main.exe
	mono $< > $@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: matrix.cs vector.cs gramschmidt.cs givens.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt
