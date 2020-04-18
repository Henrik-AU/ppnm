all: outCTest.txt CompareTime.svg CompareOperations.svg

CompareTime.svg: timePlot.gpi timeCyclic.txt timeValue.txt timeClassic.txt
	gnuplot $<

CompareOperations.svg: operationsPlot.gpi outCyclic.txt outValue.txt outClassic.txt
	gnuplot $<

outCyclic.txt timeCyclic.txt: mainC.exe
	for dim in `seq 20 5 100`; do \
		\time -f "$$dim %U" -ao timeCyclic.txt mono mainC.exe $$dim cyclic; \
	done

outValue.txt timeValue.txt: mainC.exe
	for dim in `seq 20 5 100`; do \
		\time -f "$$dim %U" -ao timeValue.txt mono mainC.exe $$dim value; \
	done

outClassic.txt timeClassic.txt: mainC.exe
	for dim in `seq 20 5 100`; do \
		\time -f "$$dim %U" -ao timeClassic.txt mono mainC.exe $$dim classic; \
	done


mainC.exe: mainC.cs math.dll
	mcs $< -reference:math.dll

outCTest.txt: mainCTest.exe
	mono $< >$@

mainCTest.exe: mainCTest.cs math.dll
	mcs $< -reference:math.dll

math.dll: jacobi.cs findEigenvalue.cs matrix.cs vector.cs jacobiClassic.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.exe *.txt *.dll *.svg Log
