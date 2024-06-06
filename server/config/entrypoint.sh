#!/bin/sh

# docker-compose bind volumes overwrite the binaries built by the go-builder container.
# Simplest hack I found was creating an entrypoint script that copies files on start-up.
# Maybe there's a better way to do this?

set -ex

cp /config/backend.so /nakama/data/modules/backend.so
cp /config/config.yml /nakama/data/config.yml

/nakama/nakama migrate up --database.address postgres:localdb@postgres:5432/nakama
exec /nakama/nakama --config /nakama/data/config.yml