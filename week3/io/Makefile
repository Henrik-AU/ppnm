all: out.txt stdout.txt

# Input file - we can make a small macro here, to help avoid typing errors and to easily
# change the input file name throughout the Makefile
INPUT = input.txt

stdout.txt: readstdin.exe $(INPUT) Makefile
	mono $< < $(INPUT) > $@

out.txt: readcmdline.exe Makefile
	mono $< 2 4 6 8 10 > $@

readcmdline.exe: readcmdline.cs Makefile
	mcs $<

readstdin.exe: readstdin.cs
	mcs $<
