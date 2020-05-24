all: out.txt

out.txt: main.exe
	mono $< > $@

main.exe: main.cs vector3d.dll
	mcs $< -reference:$(word 2, $^)

vector3d.dll: vector3d.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.txt *.exe *.dll
