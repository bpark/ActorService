#!/usr/bin/env sh

sudo groupadd docker

sudo usermod -aG docker vagrant

sudo snap install docker

sudo snap start docker
