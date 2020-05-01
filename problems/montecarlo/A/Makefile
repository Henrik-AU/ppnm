all: out.txt

out.txt: main.exe
	mono $< >$@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: montecarlo.cs vector.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.txt *.exe *.dll
