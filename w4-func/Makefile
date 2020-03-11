outA-and-B.txt: main.exe
	mono $< > $@ 2> log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll 

math.dll: quado8.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.exe *.dll *.txt log
