out.txt: main.exe
	mono $< > $@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll 

math.dll: quado8.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.exe *.dll *.txt
