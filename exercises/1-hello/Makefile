all: out.txt

out.txt : main.exe
	mono $< >$@

main.exe : main.cs
	mcs $<
	
clean:
	rm -f *.txt *.exe
