all: out.txt
	cat out.txt

out.txt : main.exe
	mono main.exe > out.txt

main.exe : main.cs approx.dll
	mcs -reference:approx.dll main.cs
	
approx.dll : approx.cs
	mcs -target:library -out:approx.dll approx.cs

clean:
	rm --force out.txt main.exe
