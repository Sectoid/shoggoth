include ../../config.mk

TARGET = ..\\..\\bin\\CLnet.VM.Tests.exe
SRC = PrognTest.cs Main.cs

all: $(TARGET) perform

$(TARGET): $(SRC)
	$(CSC) /target:exe /reference:..\\..\\bin\\CLnet.VM.dll /out:$(TARGET) $(SRC)

.PHONY: perform
perform:
	cd ..\\..\\bin
	$(TARGET)

.PHONY: clean
clean:
	$(RM) $(TARGET)
