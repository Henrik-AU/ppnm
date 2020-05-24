all: ms.pdf

ms.pdf: ms.tex Sqrt.tex
	pdflatex $<
	pdflatex $<

Sqrt.tex: plot.gpi out.txt
	gnuplot $<

out.txt: main.exe
	mono $< 1>$@ 2>log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: vector.cs ode.cs
	mcs $^ -target:library -out:$@ 

clean:
	rm -f *.txt *.dll *.exe *.svg *.log *.aux *.pdf *.eps out Sqrt.tex log *.bbl *.blg
