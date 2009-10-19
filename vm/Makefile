include ../config.mk

TARGET = ..\\bin\\CLnet.VM.dll
SRC = ..\\common\\AssemblyInfo.cs \
	Types\\BasicTypes.cs Types\\Scope.cs Exception.cs TypeResolver.cs Eval.cs

all: $(TARGET)

$(TARGET): $(SRC)
	$(CSC) /target:library /out:$(TARGET) $(SRC)

.PHONY: clean
clean:
	$(RM) $(TARGET)
