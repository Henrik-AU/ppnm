all: Animation.gif

Animation.gif: plotAnimation.gpi out.txt rosenbrock.txt animation.txt
	gnuplot $< 2> LogAnimation

out.txt: main.exe
	mono $< >$@ 2>Log

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: vector.cs dsimplex.cs
	mcs $^ -target:library -out:$@

clean:
	rm -f *.dll *.exe *.txt *Log *.gif
