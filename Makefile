include config.mk

.PHONY: vm

all: vm

vm:
	$(MAKE) -C vm

.PHONY: test
test: test-vm

.PHONY: test-vm
test-vm:
	$(MAKE) -C test/vm
