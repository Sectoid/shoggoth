include config.mk

all: vm

.PHONY: clean
clean: vm-clean test-clean

.PHONY: vm vm-clean
vm:
	$(MAKE) -C vm
vm-clean:
	$(MAKE) -C vm clean

.PHONY: test test-clean
test: test-vm
test-clean: test-vm-clean

.PHONY: test-vm test-vm-clean
test-vm: vm
	$(MAKE) -C test/vm
test-vm-clean:
	$(MAKE) -C test/vm clean
