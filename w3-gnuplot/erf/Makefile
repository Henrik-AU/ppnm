all: erf-data.txt erf-tableval.txt

erf-data.txt: main.exe
	mono $< > $@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll 

math.dll: erf.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.exe *.txt *.dll


# We create a file with a few table values for the error function
erf-tableval.txt: Makefile
	echo '0.6\t0.603856091' >> $@
	echo '1.2\t0.910313978' >> $@
	echo '1.8\t0.989090502' >> $@
	echo '2.4\t0.999311486' >> $@
