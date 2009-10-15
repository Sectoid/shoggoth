include ../config.mk

TARGET = CLnet.VM.dll
SRC = Cons.cs

all: $(TARGET)

$(TARGET): $(SRC)
	$(CSC) /target:library /out:$(TARGET) $(SRC)

.PHONY: clean
clean:
	$(RM) $(TARGET)