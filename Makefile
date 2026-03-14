DEVICE_DIR := core

.PHONY: dev down build test coverage build-image build-image-dev prod-up prod-down

dev:
	$(MAKE) -C $(DEVICE_DIR) dev

down:
	$(MAKE) -C $(DEVICE_DIR) down

build:
	$(MAKE) -C $(DEVICE_DIR) build

test:
	$(MAKE) -C $(DEVICE_DIR) test

coverage:
	$(MAKE) -C $(DEVICE_DIR) coverage

build-image:
	$(MAKE) -C $(DEVICE_DIR) build-image

build-image-dev:
	$(MAKE) -C $(DEVICE_DIR) build-image-dev

prod-up:
	$(MAKE) -C $(DEVICE_DIR) prod-up

prod-down:
	$(MAKE) -C $(DEVICE_DIR) prod-down
