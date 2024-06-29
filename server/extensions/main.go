package main

import (
	"context"
	"database/sql"
	"errors"
	"time"

	"github.com/heroiclabs/nakama-common/api"
	"github.com/heroiclabs/nakama-common/runtime"
)

const (
	cardsStorageKey = "cards"
)

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

// This is the entry point used by Nakama's runtime
func InitModule(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, initializer runtime.Initializer) error {
	// Used to track performance
	initStart := time.Now()

	initializer.RegisterAfterAuthenticateEmail(InitializeUser)
	initializer.RegisterRpc("GetAvailableCards", GetAvailableCardsRpc)

	InitializeStorageObject(ctx, logger, nk)

	logger.Info("Module loaded in %dms", time.Since(initStart).Milliseconds())
	return nil
}

// Hook that executes when a new user signs up and gives them a few coins :)
func InitializeUser(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, out *api.Session, in *api.AuthenticateEmailRequest) error {
	if out.Created {
		// runtime.RUNTIME_CTX_USER_ID is just a constant for "user_id"
		// We can get its value by calling ctx.Value
		userID, ok := ctx.Value(runtime.RUNTIME_CTX_USER_ID).(string)
		if !ok {
			return errors.New("invalid context")
		}

		changeset := map[string]int64{
			"c-coins": 100,
		}
		metadata := map[string]interface{}{
			"motive": "Welcome bonus",
		}

		if _, _, err := nk.WalletUpdate(ctx, userID, changeset, metadata, true); err != nil {
			return err
		}
	}

	return nil
}

// We want to make sure that the storage object exists and is initialized so that it can be modified
// through the Nakama web dashboard. It's just easier to work with.
func InitializeStorageObject(ctx context.Context, logger runtime.Logger, nk runtime.NakamaModule) {
	si := NewStorageInteractor(ctx, logger, nk)
	si.CreateStorageObjectIfNotExists(cardsStorageKey, "[]", PUBLIC_READ, NO_WRITE)
}

func GetAvailableCardsRpc(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {
	si := NewStorageInteractor(ctx, logger, nk)

	cards, err := si.ReadStorageObject(cardsStorageKey)
	if err != nil {
		logger.Error("Could not read cards from storage", err.Error())
		return "", err
	}

	return cards, nil
}
