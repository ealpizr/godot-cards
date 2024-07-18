FROM docker.io/heroiclabs/nakama-pluginbuilder:3.22.0-arm AS go-builder

ENV GO111MODULE on
ENV CGO_ENABLED 1

WORKDIR /extensions
COPY extensions /extensions

RUN go build --trimpath --mod=vendor --buildmode=plugin -o ./build/backend.so

FROM docker.io/heroiclabs/nakama:3.22.0-arm

COPY config /config
COPY --from=go-builder /extensions/build/backend.so /config/backend.so

RUN chmod +x /config/entrypoint.sh