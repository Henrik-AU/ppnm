all: SinPlot.svg BesselPlot.svg report.pdf

report.pdf: report.tex
	pdflatex $<
	pdflatex $<


BesselPlot.svg: plotBessel.gpi splineBessel.txt tabValBessel.txt tabValJ0Int.txt
	gnuplot $<

SinPlot.svg: plotSin.gpi splineSin.txt tabValSin.txt
	gnuplot $<

splineSin.txt tabValSin.txt splineBessel.txt tabValBessel.txt: main.exe
	mono $< >splineSin.txt 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: subspline.cs driver.cs rkstep45.cs vector.cs
	mcs $^ -target:library -out:$@

# Table values pulled directly from WolframAlpha for Si(x) (defined as the integral of sin(x)/x from 
# 0 to x.
tabValJ0Int.txt:
	echo '1\t0.946083' >> $@
	echo '2\t1.605413' >> $@
	echo '3\t1.848653' >> $@
	echo '4\t1.758203' >> $@
	echo '5\t1.549931' >> $@
	echo '6\t1.424688' >> $@
	echo '7\t1.454596' >> $@
	echo '8\t1.574187' >> $@
	echo '9\t1.665040' >> $@

clean:
	rm -f *.dll *.exe *.txt Log *.svg *.log *.aux *.pdf *.out
