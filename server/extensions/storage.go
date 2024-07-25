package main

import (
	"context"
	"encoding/json"
	"fmt"

	"github.com/heroiclabs/nakama-common/runtime"
)

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

func (s *StorageInteractor) ReadStorageObject(key string, userId string) (string, error) {
	records, err := s.nk.StorageRead(s.ctx, []*runtime.StorageRead{
		{
			Collection: storageCollection,
			Key:        key,
			UserID:     userId,
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

func (s *StorageInteractor) WriteStorageObject(key string, data interface{}, readPermission int, writePermission int, userId string) error {
	object := StorageObject{
		Data: data,
	}

	value, err := json.Marshal(object)
	if err != nil {
		s.logger.Error("Could not marshal object")
		return err
	}

	_, err = s.nk.StorageWrite(s.ctx, []*runtime.StorageWrite{
		{
			Collection:      storageCollection,
			Key:             key,
			PermissionRead:  readPermission,
			PermissionWrite: writePermission,
			Value:           string(value),
			UserID:          userId,
		},
	})

	if err != nil {
		s.logger.Error("Could not write object to storage")
		return err
	}

	return nil
}
