all: out.txt

out.txt: main.exe
	mono $< >$@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: jacobi.cs matrix.cs vector.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.exe *.txt *.dll *.svg
