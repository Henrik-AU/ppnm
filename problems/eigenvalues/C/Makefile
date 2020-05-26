all: out.txt CompareTime.svg CompareOperations.svg
	cat out.txt

CompareTime.svg: timePlot.gpi timeCyclic.txt timeValue.txt timeClassic.txt
	gnuplot $<

CompareOperations.svg: operationsPlot.gpi outCyclic.txt outValue.txt outClassic.txt
	gnuplot $<

outCyclic.txt timeCyclic.txt: main.exe
	for dim in `seq 20 5 100`; do \
		\time -f "$$dim %U" -ao timeCyclic.txt mono main.exe $$dim cyclic; \
	done

outValue.txt timeValue.txt: main.exe
	for dim in `seq 20 5 100`; do \
		\time -f "$$dim %U" -ao timeValue.txt mono main.exe $$dim value; \
	done

outClassic.txt timeClassic.txt: main.exe
	for dim in `seq 20 5 100`; do \
		\time -f "$$dim %U" -ao timeClassic.txt mono main.exe $$dim classic; \
	done


main.exe: main.cs math.dll
	mcs $< -reference:math.dll

out.txt: mainTest.exe
	mono $< >$@

mainTest.exe: mainTest.cs math.dll
	mcs $< -reference:math.dll

math.dll: jacobi.cs findEigenvalue.cs matrix.cs vector.cs jacobiClassic.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.exe *.txt *.dll *.svg Log
