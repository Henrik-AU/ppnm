all: out.txt
	cat out.txt

out.txt: main.exe
	mono $< >$@ 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: integration.cs clenshaw.cs quado8.cs vector.cs

	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.txt *.exe Log
