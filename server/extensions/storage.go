package main

import (
	"context"
	"encoding/json"
	"fmt"

	"github.com/heroiclabs/nakama-common/runtime"
)

const (
	PUBLIC_READ = 2
	OWNER_READ  = 1
	NO_READ     = 0
	OWNER_WRITE = 1
	NO_WRITE    = 0

	storageCollection = "server"
)

type StorageInteractor struct {
	ctx    context.Context
	logger runtime.Logger
	nk     runtime.NakamaModule
}

type StorageObject struct {
	Data interface{} `json:"data"`
}

func NewStorageInteractor(ctx context.Context, logger runtime.Logger, nk runtime.NakamaModule) *StorageInteractor {
	return &StorageInteractor{
		ctx:    ctx,
		logger: logger,
		nk:     nk,
	}
}

func (s *StorageInteractor) CreateStorageObjectIfNotExists(key string, defaultValue string, readPermission int, writePermission int) {
	object, err := s.nk.StorageRead(s.ctx, []*runtime.StorageRead{
		{
			Collection: storageCollection,
			Key:        key,
		},
	})

	if err != nil {
		s.logger.Error("Error reading storage object: %v", err)
		panic(err)
	}

	// If the object doesn't exist, create it
	if len(object) == 0 {
		_, err = s.nk.StorageWrite(s.ctx, []*runtime.StorageWrite{
			{
				Collection:      storageCollection,
				Key:             key,
				PermissionRead:  readPermission,
				PermissionWrite: writePermission,
				Value:           fmt.Sprintf("{\"data\": %s}", defaultValue),
			},
		})

		if err != nil {
			s.logger.Error("Error writing storage object")
			panic(err)
		}

		s.logger.Info("Created storage object")
	}
}

func (s *StorageInteractor) ReadStorageObject(key string) (string, error) {
	records, err := s.nk.StorageRead(s.ctx, []*runtime.StorageRead{
		{
			Collection: storageCollection,
			Key:        key,
		},
	})

	if err != nil || len(records) == 0 {
		s.logger.Error("Could not read object from storage")
		return "", err
	}

	object := StorageObject{}
	if err := json.Unmarshal([]byte(records[0].Value), &object); err != nil {
		s.logger.Error("Could not unmarshal object from storage")
		return "", err
	}

	data, err := json.Marshal(object.Data)
	if err != nil {
		s.logger.Error("Could not marshal object data")
		return "", err
	}

	return string(data), nil
}
