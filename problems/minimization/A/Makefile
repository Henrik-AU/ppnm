all: out.txt
	cat out.txt

out.txt: main.exe
	mono $< >$@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: vector.cs qnewton.cs matrix.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt
