package main

import (
	"context"
	"database/sql"
	"time"

	"github.com/heroiclabs/nakama-common/runtime"
)

// This is the entry point used by Nakama's runtime
func InitModule(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, initializer runtime.Initializer) error {
	// Used to track performance
	initStart := time.Now()

	InitializeStorageObjects(NewStorageInteractor(ctx, logger, nk))

	initializer.RegisterAfterAuthenticateEmail(InitializeUser)
	initializer.RegisterRpc("GetAvailableCards", GetAvailableCardsRpc)
	initializer.RegisterRpc("GetAvailableDice", GetAvailableDiceRpc)
	initializer.RegisterRpc("GetUserInventory", GetUserInventoryRpc)
	initializer.RegisterRpc("GetUserDeck", GetUserDeckRpc)
	initializer.RegisterRpc("GetUserDice", GetUserDiceRpc)
	initializer.RegisterRpc("ReplaceDeckCardRpc", ReplaceUserDeckCardRpc)
	initializer.RegisterRpc("SetUserDice", SetUserDiceRpc)
	initializer.RegisterRpc("GetAllCards", GetAllCardsRpc)
	initializer.RegisterRpc("BuyCard", BuyCardRpc)

	logger.Info("Module loaded in %dms", time.Since(initStart).Milliseconds())
	return nil
}
