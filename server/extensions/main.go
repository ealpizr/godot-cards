package main

import (
	"context"
	"database/sql"
	"errors"
	"time"

	"github.com/heroiclabs/nakama-common/api"
	"github.com/heroiclabs/nakama-common/runtime"
)

// This is the entry point used by Nakama's runtime
func InitModule(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, initializer runtime.Initializer) error {
    // Used to track performance
    initStart := time.Now()
    
    initializer.RegisterAfterAuthenticateEmail(InitializeUser)

    logger.Info("Module loaded in %dms", time.Since(initStart).Milliseconds())
    return nil
}

// Hook that executes when a new user signs up and gives them a few coins :)
func InitializeUser(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, out *api.Session, in *api.AuthenticateEmailRequest) error {
    if(out.Created) {
        // runtime.RUNTIME_CTX_USER_ID is just a constant for "user_id"
        // We can get its value by calling ctx.Value
        userID, ok := ctx.Value(runtime.RUNTIME_CTX_USER_ID).(string)
        if(!ok) {
            return errors.New("invalid context")
        }

        changeset := map[string]int64{
            "c-coins": 100,
          }
        metadata := map[string]interface{}{
            "motive": "Welcome bonus",
        }

        if _, _, err := nk.WalletUpdate(ctx, userID, changeset, metadata, true); err != nil {
            return err;
        }
    }

    return nil;
}