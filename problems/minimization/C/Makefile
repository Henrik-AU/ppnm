all: out.txt
	cat out.txt

out.txt: main.exe
	mono $< >$@ 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: vector.cs dsimplex.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt
