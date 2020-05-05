all: out.txt

out.txt: main.exe
	mono $< >$@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: neural.cs vector.cs qnewton.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.txt *.dll *.exe
