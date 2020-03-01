all: out-cmd.txt out-std.txt out-file.txt

# Input file - we can make a small macro here, to help avoid typing errors and to easily
# change the input file name throughout the Makefile
INPUT = input.txt

out-file.txt: readfile.exe $(INPUT) Makefile
	mono $< $(INPUT) $@

readfile.exe: readfile.cs
	mcs $<	

out-std.txt: readstdin.exe $(INPUT) Makefile
	mono $< < $(INPUT) > $@

readstdin.exe: readstdin.cs
	mcs $<

out-cmd.txt: readcmdline.exe Makefile
	mono $< 2 4 6 8 10 > $@

readcmdline.exe: readcmdline.cs Makefile
	mcs $<

clean: 
	rm -f *.exe out* input.txt

$(INPUT): Makefile
	/bin/echo -e '1\n3\n5\n7\n9' > $@
