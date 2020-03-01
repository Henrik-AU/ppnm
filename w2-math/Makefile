# CSFLAGS = -optimize+ -platform:arm (extra optimizations for a specific processor - Dimitrys case is 'arm'. $(CSFLAGS) then has to be used with mcs also, as seen in the comment below)
.PHONEY:all
all: out.txt	#This ensures that 'make' prints out the file in the console
	cat out.txt

out.txt : main.exe
	mono main.exe > out.txt
main.exe : main.cs cmath.dll
	mcs -reference:cmath.dll main.cs
	# mcs $(CSFLAGS) -out:main.exe main.cs
	
cmath.dll : cmath.cs complex.cs
	mcs -target:library -out:cmath.dll cmath.cs complex.cs

#.PHONEY:clean
# Phoney:clean avoids confusion if there is also a file called clean
clean:
	rm --force out.txt main.exe
