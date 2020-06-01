all: out.txt
	cat out.txt

out.txt: main.exe
	mono $< >$@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: integration.cs clenshaw.cs infinite.cs quado8.cs vector.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.txt *.exe
