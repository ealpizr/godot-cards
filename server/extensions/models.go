package main

import (
	"context"

	"github.com/heroiclabs/nakama-common/runtime"
)

const (
	PUBLIC_READ = 2
	OWNER_READ  = 1
	NO_READ     = 0
	OWNER_WRITE = 1
	NO_WRITE    = 0

	storageCollection = "server"
	cardsStorageKey   = "cards"
)

type StorageInteractor struct {
	ctx    context.Context
	logger runtime.Logger
	nk     runtime.NakamaModule
}

type StorageObject struct {
	Data interface{} `json:"data"`
}

type Card struct {
	ID          int    `json:"id"`
	Name        string `json:"name"`
	Cost        int    `json:"cost"`
	Type        string `json:"type"`
	Attack      int    `json:"attack"`
	Health      int    `json:"health"`
	Rarity      string `json:"rarity"`
	ManaCost    int    `json:"manaCost"`
	Description string `json:"description"`
}
