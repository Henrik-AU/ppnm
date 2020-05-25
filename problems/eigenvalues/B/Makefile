all: TimeScaling.svg ComparisonIII.svg ComparisonIV.svg

ComparisonIV.svg: compPlotIV.gpi outB4.txt
	gnuplot $<

ComparisonIII.svg: compPlotIII.gpi outB3.txt
	gnuplot $<

TimeScaling.svg: timePlot.gpi time.txt
	gnuplot $<

# We will run the \time utility several times with different matrix dimensions. This is done in a small for
# loop
time.txt: mainTime.exe Makefile
	for dimension in `seq 40 20 160` ; do \
		\time --format "$$dimension %U" --append --output time.txt mono mainTime.exe $$dimension ; \
	done	

outB3.txt outB4.txt outB5.txt: mainB.exe
	mono $<

mainB.exe: mainB.cs math.dll
	mcs $< -reference:math.dll

mainTime.exe: mainTime.cs math.dll
	mcs $< -reference:math.dll

math.dll: jacobi.cs findEigenvalue.cs matrix.cs vector.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.exe *.txt *.dll *.svg Log
