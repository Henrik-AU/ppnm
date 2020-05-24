all: Erf.svg

Erf.svg: erf.gpi erf-data.txt erf-tableval.txt
	gnuplot $<

erf-data.txt: main.exe
	mono $< > $@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll 

math.dll: erf.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.exe *.txt *.dll *.svg


# We create a file with a few table values for the error function
erf-tableval.txt: Makefile
	echo '0.1\t0.112462916' >> $@
	echo '0.3\t0.328626759' >> $@
	echo '0.6\t0.603856091' >> $@
	echo '0.9\t0.796908212' >> $@
	echo '1.2\t0.910313978' >> $@
	echo '1.5\t0.966105146' >> $@
	echo '1.8\t0.989090502' >> $@
	echo '2.1\t0.997020533' >> $@
	echo '2.4\t0.999311486' >> $@
