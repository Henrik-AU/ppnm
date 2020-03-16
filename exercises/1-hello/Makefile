all: out.txt
	cat out.txt

out.txt : main.exe
	mono main.exe > out.txt
main.exe : main.cs
	mcs -out:main.exe main.cs
	
clean:
	rm --force out.txt main.exe
