all: out.txt

out.txt: main.exe
	mono $< >$@

main.exe: main.cs
	mcs $<

math.dll: vector.cs rkstep45.cs driver.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt *.svg
