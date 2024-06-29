package main

import (
	"context"
	"database/sql"

	"github.com/heroiclabs/nakama-common/runtime"
)

func GetAvailableCardsRpc(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {
	si := NewStorageInteractor(ctx, logger, nk)

	cards, err := si.ReadStorageObject(cardsStorageKey)
	if err != nil {
		logger.Error("Could not read cards from storage", err.Error())
		return "", err
	}

	return cards, nil
}
