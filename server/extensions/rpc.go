package main

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"

	"github.com/heroiclabs/nakama-common/runtime"
)

func GetAvailableCardsRpc(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {
	si := NewStorageInteractor(ctx, logger, nk)

	userID, ok := ctx.Value(runtime.RUNTIME_CTX_USER_ID).(string)
	if !ok {
		return "", errors.New("invalid context")
	}

	allCardsJson, err := si.ReadStorageObject(cardsStorageKey, serverUserId)
	if err != nil {
		logger.Error("Could not read cards from storage", err.Error())
		return "", err
	}

	userCardsJson, err := si.ReadStorageObject(userInventoryKey, userID)
	if err != nil {
		logger.Error("Could not read user inventory from storage", err.Error())
		return "", err
	}

	allCards := []Card{}
	err = json.Unmarshal([]byte(allCardsJson), &allCards)
	if err != nil {
		logger.Error("Could not unmarshal all cards", err.Error())
		return "", err
	}

	userCards := UserInventory{}
	err = json.Unmarshal([]byte(userCardsJson), &userCards)
	if err != nil {
		logger.Error("Could not unmarshal user cards", err.Error())
		return "", err
	}

	userCardsMap := map[int]bool{}
	for _, cardID := range userCards.Cards {
		userCardsMap[cardID] = true
	}

	availableCards := []Card{}
	for _, card := range allCards {
		if _, ok := userCardsMap[card.ID]; !ok {
			availableCards = append(availableCards, card)
		}
	}

	availableCardsJson, err := json.Marshal(availableCards)
	if err != nil {
		logger.Error("Could not marshal available cards", err.Error())
		return "", err
	}

	return string(availableCardsJson), nil
}

func GetAvailableDiceRpc(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {
	si := NewStorageInteractor(ctx, logger, nk)

	userID, ok := ctx.Value(runtime.RUNTIME_CTX_USER_ID).(string)
	if !ok {
		return "", errors.New("invalid context")
	}

	allDiceJson, err := si.ReadStorageObject(diceStorageKey, serverUserId)
	if err != nil {
		logger.Error("Could not read dice from storage", err.Error())
		return "", err
	}

	userDiceJson, err := si.ReadStorageObject(userInventoryKey, userID)
	if err != nil {
		logger.Error("Could not read user inventory from storage", err.Error())
		return "", err
	}

	allDice := []Dice{}
	err = json.Unmarshal([]byte(allDiceJson), &allDice)
	if err != nil {
		logger.Info(allDiceJson)
		logger.Error("Could not unmarshal all dice", err.Error())
		return "", err
	}

	userDice := UserInventory{}
	err = json.Unmarshal([]byte(userDiceJson), &userDice)
	if err != nil {
		logger.Error("Could not unmarshal user dice", err.Error())
		return "", err
	}

	userDiceMap := map[int]bool{}
	for _, diceID := range userDice.Dice {
		userDiceMap[diceID] = true
	}

	availableDice := []Dice{}
	for _, dice := range allDice {
		if _, ok := userDiceMap[dice.ID]; !ok {
			availableDice = append(availableDice, dice)
		}
	}

	availableDiceJson, err := json.Marshal(availableDice)
	if err != nil {
		logger.Error("Could not marshal available dice", err.Error())
		return "", err
	}

	return string(availableDiceJson), nil
}

func GetUserInventoryRpc(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {
	si := NewStorageInteractor(ctx, logger, nk)

	userID, ok := ctx.Value(runtime.RUNTIME_CTX_USER_ID).(string)
	if !ok {
		return "", errors.New("invalid context")
	}

	userInventoryJson, err := si.ReadStorageObject(userInventoryKey, userID)
	if err != nil {
		logger.Error("Could not read user inventory from storage", err.Error())
		return "", err
	}

	allCardsJson, err := si.ReadStorageObject(cardsStorageKey, serverUserId)
	if err != nil {
		logger.Error("Could not read cards from storage", err.Error())
		return "", err
	}

	allDiceJson, err := si.ReadStorageObject(diceStorageKey, serverUserId)
	if err != nil {
		logger.Error("Could not read dice from storage", err.Error())
		return "", err
	}

	userInventory := UserInventory{}
	err = json.Unmarshal([]byte(userInventoryJson), &userInventory)
	if err != nil {
		logger.Error("Could not unmarshal user inventory", err.Error())
		return "", err
	}

	allCards := []Card{}
	err = json.Unmarshal([]byte(allCardsJson), &allCards)
	if err != nil {
		logger.Error("Could not unmarshal all cards", err.Error())
		return "", err
	}

	allDice := []Dice{}
	err = json.Unmarshal([]byte(allDiceJson), &allDice)
	if err != nil {
		logger.Error("Could not unmarshal all dice", err.Error())
		return "", err
	}

	allCardsMap := map[int]Card{}
	for _, card := range allCards {
		allCardsMap[card.ID] = card
	}

	allDiceMap := map[int]Dice{}
	for _, dice := range allDice {
		allDiceMap[dice.ID] = dice
	}

	userInventoryData := map[string]interface{}{
		"cards": []Card{},
		"dice":  []Dice{},
	}
	for _, cardID := range userInventory.Cards {
		userInventoryData["cards"] = append(userInventoryData["cards"].([]Card), allCardsMap[cardID])
	}
	for _, diceID := range userInventory.Dice {
		userInventoryData["dice"] = append(userInventoryData["dice"].([]Dice), allDiceMap[diceID])
	}

	userInventoryDataJson, err := json.Marshal(userInventoryData)
	if err != nil {
		logger.Error("Could not marshal user inventory data", err.Error())
		return "", err
	}

	return string(userInventoryDataJson), nil
}

func GetUserDeckRpc(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {
	si := NewStorageInteractor(ctx, logger, nk)

	userID, ok := ctx.Value(runtime.RUNTIME_CTX_USER_ID).(string)
	if !ok {
		return "", errors.New("invalid context")
	}

	userDeckJson, err := si.ReadStorageObject(userDeckKey, userID)
	if err != nil {
		logger.Error("Could not read user deck from storage", err.Error())
		return "", err
	}

	allCardsJson, err := si.ReadStorageObject(cardsStorageKey, serverUserId)
	if err != nil {
		logger.Error("Could not read cards from storage", err.Error())
		return "", err
	}

	userDeck := UserDeck{}
	err = json.Unmarshal([]byte(userDeckJson), &userDeck)
	if err != nil {
		logger.Error("Could not unmarshal user deck", err.Error())
		return "", err
	}

	allCards := []Card{}
	err = json.Unmarshal([]byte(allCardsJson), &allCards)
	if err != nil {
		logger.Error("Could not unmarshal all cards", err.Error())
		return "", err
	}

	allCardsMap := map[int]Card{}
	for _, card := range allCards {
		allCardsMap[card.ID] = card
	}

	userDeckData := []Card{}
	for _, cardID := range userDeck.Cards {
		userDeckData = append(userDeckData, allCardsMap[cardID])
	}

	userDeckDataJson, err := json.Marshal(userDeckData)
	if err != nil {
		logger.Error("Could not marshal user deck data", err.Error())
		return "", err
	}

	return string(userDeckDataJson), nil
}

func GetUserDiceRpc(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {
	si := NewStorageInteractor(ctx, logger, nk)

	userID, ok := ctx.Value(runtime.RUNTIME_CTX_USER_ID).(string)
	if !ok {
		return "", errors.New("invalid context")
	}

	userDiceJson, err := si.ReadStorageObject(userDiceKey, userID)
	if err != nil {
		logger.Error("Could not read user dice from storage", err.Error())
		return "", err
	}

	allDiceJson, err := si.ReadStorageObject(diceStorageKey, serverUserId)
	if err != nil {
		logger.Error("Could not read dice from storage", err.Error())
		return "", err
	}

	allDice := []Dice{}
	err = json.Unmarshal([]byte(allDiceJson), &allDice)
	if err != nil {
		logger.Error("Could not unmarshal all dice", err.Error())
		return "", err
	}

	userDice := &UserDice{}
	err = json.Unmarshal([]byte(userDiceJson), &userDice)
	if err != nil {
		logger.Error("Could not unmarshal user dice", err.Error())
		return "", err
	}

	for _, dice := range allDice {
		if dice.ID == userDice.Id {
			userDiceDataJson, err := json.Marshal(dice)
			if err != nil {
				logger.Error("Could not marshal user dice data", err.Error())
				return "", err
			}

			return string(userDiceDataJson), nil
		}
	}

	return "", errors.New("could not find user dice")
}

func ReplaceUserDeckCardRpc(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {
	si := NewStorageInteractor(ctx, logger, nk)

	userID, ok := ctx.Value(runtime.RUNTIME_CTX_USER_ID).(string)
	if !ok {
		return "", errors.New("invalid context")
	}

	replaceDeckCardPayload := &ReplaceDeckCardPayload{}
	err := json.Unmarshal([]byte(payload), &replaceDeckCardPayload)
	if err != nil {
		logger.Error("Could not unmarshal replace deck card payload", err.Error())
		return "", err
	}

	userDeckJson, err := si.ReadStorageObject(userDeckKey, userID)
	if err != nil {
		logger.Error("Could not read user deck from storage", err.Error())
		return "", err
	}

	userDeck := UserInventory{}
	err = json.Unmarshal([]byte(userDeckJson), &userDeck)
	if err != nil {
		logger.Error("Could not unmarshal user deck", err.Error())
		return "", err
	}

	cardFound := false
	for i, cardID := range userDeck.Cards {
		if cardID == replaceDeckCardPayload.Replace {
			userDeck.Cards[i] = replaceDeckCardPayload.With
			cardFound = true
			break
		}
	}

	if !cardFound {
		logger.Error("Could not find card to replace in user deck")
		return "", errors.New("could not find card to replace in user deck")
	}

	err = si.WriteStorageObject(userDeckKey, userDeck, PUBLIC_READ, NO_WRITE, userID)
	if err != nil {
		logger.Error("Could not write user deck to storage", err.Error())
		return "", err
	}

	return payload, nil
}

func SetUserDiceRpc(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {
	si := NewStorageInteractor(ctx, logger, nk)

	userID, ok := ctx.Value(runtime.RUNTIME_CTX_USER_ID).(string)
	if !ok {
		return "", errors.New("invalid context")
	}

	setUserDicePayload := &SetUserDicePayload{}
	err := json.Unmarshal([]byte(payload), &setUserDicePayload)
	if err != nil {
		logger.Error("Could not unmarshal set user dice payload", err.Error())
		return "", err
	}

	userDice := UserDice{
		Id: setUserDicePayload.Id,
	}

	err = si.WriteStorageObject(userDiceKey, userDice, PUBLIC_READ, NO_WRITE, userID)
	if err != nil {
		logger.Error("Could not write user dice to storage", err.Error())
		return "", err
	}

	return payload, nil
}
