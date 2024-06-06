#!/bin/bash

VOLUME_PATH=${1:-/home/cockpit/containers/nakama}

if [ ! -d "$VOLUME_PATH" ]; then
  mkdir -p "$VOLUME_PATH"
fi

VOLUME_PATH=$VOLUME_PATH podman-compose up --build --force-recreate -d