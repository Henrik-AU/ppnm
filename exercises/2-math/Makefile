all: out.txt

out.txt : main.exe
	mono $< > $@

main.exe : main.cs cmath.dll
	mcs $< -reference:cmath.dll
	
cmath.dll : cmath.cs complex.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.txt *.exe *.dll
