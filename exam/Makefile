all: out.txt

out.txt: main.exe
	mono $< >$@ 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: subspline.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt Log *.svg
