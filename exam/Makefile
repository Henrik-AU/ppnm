all: out.txt

out.txt: main.exe
	mono $< >$@ 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: #Enter here
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt Log *.svg
