all: main.exe tableval.txt

out.txt: main.exe
	mono $< >$@

main.exe: main.cs math.dll
	mcs $< -reference:math.dll

math.dll: vector.cs linspline.cs
	mcs $^ -target:library -out:$@


tableval.txt: Makefile
	# Data will be from the function f(x) = x^2 + x + 4	
	echo '1\t5' >> $@
	echo '2\t10' >> $@
	echo '3\t16' >> $@
	echo '4\t24' >> $@
	echo '5\t34' >> $@

clean:
	rm -f *.dll *.exe *.svg *.txt
