package main

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"time"

	"github.com/heroiclabs/nakama-common/api"
	"github.com/heroiclabs/nakama-common/runtime"
)

// For now we will use a global storage object to store game data
// such as cards, items, etc. This might work for the whole project scope
const (
	storageCollection = "server"
	storageKey        = "game-data"
)

type GameData struct {
	Cards []Card `json:"cards"`
}

type Card struct {
	ID   int    `json:"id"`
	Name string `json:"name"`
	Cost int    `json:"cost"`
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
	object, err := nk.StorageRead(ctx, []*runtime.StorageRead{
		{
			Collection: storageCollection,
			Key:        storageKey,
		},
	})

	if err != nil {
		logger.Error("Error reading storage object: %v", err)
		panic(err)
	}

	// If the object doesn't exist, create it
	if len(object) == 0 {
		_, err = nk.StorageWrite(ctx, []*runtime.StorageWrite{
			{
				Collection:      storageCollection,
				Key:             storageKey,
				PermissionRead:  2, // Public read
				PermissionWrite: 1, // Private write
				Value:           "{}",
			},
		})

		if err != nil {
			logger.Error("Error writing storage object")
			panic(err)
		}

		logger.Info("Created storage object")
	}
}

func GetAvailableCardsRpc(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {
	records, err := nk.StorageRead(ctx, []*runtime.StorageRead{&runtime.StorageRead{
		Collection: storageCollection,
		Key:        storageKey,
	}})
	if err != nil || len(records) == 0 {
		logger.Error("Could not read cards from storage")
	}

	gameData := GameData{}
	if err := json.Unmarshal([]byte(records[0].Value), &gameData); err != nil {
		logger.Error("Could not unmarshal game data")
		logger.Error(err.Error())
	}

	response, err := json.Marshal(gameData.Cards)
	if err != nil {
		logger.Error("Could not marshal cards")
		logger.Error(err.Error())
	}

	return string(response), nil
}
