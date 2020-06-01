all: TimeScaling.svg ComparisonLowEig.svg ComparisonFullDiag.svg
	cat outB5.txt

ComparisonFullDiag.svg: plotFullDiag.gpi outB4.txt
	gnuplot $<

ComparisonLowEig.svg: plotLowEig.gpi outB3.txt
	gnuplot $<

TimeScaling.svg: plotTime.gpi time.txt
	gnuplot $< 2>LogTimePlot

# We will run the \time utility several times with different matrix dimensions. This is done in a small for
# loop
time.txt: mainTime.exe Makefile
	for dimension in `seq 40 20 160` ; do \
		\time --format "$$dimension %U" --append --output time.txt mono mainTime.exe $$dimension ; \
	done	

outB3.txt outB4.txt outB5.txt: main.exe
	mono $<

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

mainTime.exe: mainTime.cs math.dll
	mcs $< -reference:math.dll

math.dll: jacobi.cs findEigenvalue.cs matrix.cs vector.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.exe *.txt *.dll *.svg Log*
