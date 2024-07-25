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
	diceStorageKey    = "dice"
	userInventoryKey  = "user_inventory"
	userDeckKey       = "user_deck"
	userDiceKey       = "user_dice"

	serverUserId = "00000000-0000-0000-0000-000000000000"
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
	Image       string `json:"image"`
	Attack      int    `json:"attack"`
	Health      int    `json:"health"`
	Rarity      string `json:"rarity"`
	ManaCost    int    `json:"manaCost"`
	Description string `json:"description"`
}

type Dice struct {
	ID          int    `json:"id"`
	Name        string `json:"name"`
	Description string `json:"description"`
	Cost        int    `json:"cost"`
	Min         int    `json:"min"`
	Max         int    `json:"max"`
	Rarity      string `json:"rarity"`
}

type UserInventory struct {
	Cards []int `json:"cards"`
	Dice  []int `json:"dice"`
}

type UserDice struct {
	Id int `json:"id"`
}

type UserDeck struct {
	Cards []int `json:"cards"`
}

type SetUserDicePayload struct {
	Id int `json:"id"`
}

type ReplaceDeckCardPayload struct {
	Replace int `json:"replace"`
	With    int `json:"with"`
}
