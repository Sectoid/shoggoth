include ../config.mk

TARGET = CLnet.VM.dll
SRC = Types\\BasicTypes.cs Types\\Scope.cs Exception.cs TypeResolver.cs

all: $(TARGET)

$(TARGET): $(SRC)
	$(CSC) /target:library /out:$(TARGET) $(SRC)

.PHONY: clean
clean:
	$(RM) $(TARGET)