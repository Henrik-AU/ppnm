all: out.txt

out.txt : main.exe
	mono $< > $@

main.exe : main.cs approx.dll
	mcs $< -reference:approx.dll
	
approx.dll : approx.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.txt *.exe *.dll
