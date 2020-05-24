all: Gamma.svg

Gamma.svg: gamma.gpi gamma-data.txt gamma-tableval.txt
	gnuplot $<

gamma-data.txt: main.exe
	mono $< > $@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: gamma.cs
	mcs $< -target:library -out:$@

clean:
	rm -f *.txt *.dll *.exe *.svg

# We create a file with a few table values for the gamma function
gamma-tableval.txt: Makefile
	echo '-4.3\t-0.101981' >> $@
	echo '-3.6\t0.246857' >> $@
	echo '-2.7\t-0.931083' >> $@
	echo '-1.7\t2.51392' >> $@
	echo '-0.7\t-4.27367' >> $@
	echo '0.7\t1.29806' >> $@
	echo '1.7\t0.908639' >> $@
	echo '2.7\t1.54469' >> $@
