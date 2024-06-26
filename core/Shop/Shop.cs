using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nakama;

public class CardData
{
	[JsonPropertyName("id")]
	public int ID { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("cost")]
	public int Cost { get; set; }
	
	[JsonPropertyName("attack")]
	public int Attack { get; set; }
	
	[JsonPropertyName("health")]
	public int Health { get; set; }
	
	[JsonPropertyName("description")]
	public string Description { get; set; }
	
	[JsonPropertyName("rarity")]
	public string Rarity { get; set; }
	
	[JsonPropertyName("manacost")]
	public int ManaCost { get; set; }
	
	[JsonPropertyName("type")]
	public string Type { get; set; }
}

public class CardPackData
{
	[JsonPropertyName("id")]
	public int ID { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("cost")]
	public int Cost { get; set; }

	[JsonPropertyName("cards")]
	public List<CardData> Cards { get; set; }
}

public partial class Shop : Control
{
	private int playerCoins;
	private List<CardData> availableCards;
	private List<CardPackData> availableCardPacks;
	private List<string> upgrades;
	private List<string> specialDice;

	private HBoxContainer cardHBoxContainer;
	private HBoxContainer packsHBoxContainer;
	private HBoxContainer diceHBoxContainer;
	private HBoxContainer upgradesHBoxContainer;

	public override void _Ready()
	{
		InitializeShop();
		SetupContainers();
		LoadAvailableCards();
		LoadAvailableCardPacks();
		LoadAvailableUpgrades();
		LoadAvailableSpecialDice();
	}

	private void InitializeShop()
	{
		playerCoins = 100;
		upgrades = new List<string> { "Energy Bar Upgrade", "Shield Upgrade" };
		specialDice = new List<string> { "Fire Dice", "Water Dice" };

		GD.Print("Shop is ready with initial setup.");
	}

	private void SetupContainers()
	{
		cardHBoxContainer = GetNode<HBoxContainer>("TabContainer/CARDS/ScrollContainer/cardHBoxContainer");
		packsHBoxContainer = GetNode<HBoxContainer>("TabContainer/PACKS/ScrollContainer/packsHBoxContainer");
		diceHBoxContainer = GetNode<HBoxContainer>("TabContainer/DICE/ScrollContainer/diceHBoxContainer");
		upgradesHBoxContainer = GetNode<HBoxContainer>("TabContainer/UPGRADES/ScrollContainer/upgradesHBoxContainer");
	}

	private async void LoadAvailableCards()
	{
		Nakama.Client client = GlobalState.Instance.NakamaClient;
		Nakama.ISession session = GlobalState.Instance.Session;

		Nakama.IApiRpc response = await client.RpcAsync(session, "GetAvailableCards");
		availableCards = JsonSerializer.Deserialize<List<CardData>>(response.Payload);

		foreach (CardData card in availableCards)
		{
			PackedScene cardScene = GD.Load<PackedScene>("res://scenes/Shop_card.tscn");
			ShopCard cardNode = (ShopCard)cardScene.Instantiate();
			((ShopCard)cardNode).SetCardData(card.ID, card.Name, card.Cost, card.Attack, card.Health, card.Description, card.Rarity, card.ManaCost, card.Type);
			cardHBoxContainer.AddChild(cardNode);
		}
	}

	private async void LoadAvailableCardPacks()
	{
		Nakama.Client client = GlobalState.Instance.NakamaClient;
		Nakama.ISession session = GlobalState.Instance.Session;

		Nakama.IApiRpc response = await client.RpcAsync(session, "GetAvailableCards");
		availableCards = JsonSerializer.Deserialize<List<CardData>>(response.Payload);

		availableCardPacks = new List<CardPackData>();

		if (availableCards.Count >= 2)
		{
			CardPackData cardPack1 = new CardPackData
			{
				ID = 1,
				Name = "Starter Pack",
				Cost = 150,
				Cards = new List<CardData>
				{
					availableCards[0],
					availableCards[1]
				}
			};
			availableCardPacks.Add(cardPack1);
		}

		if (availableCards.Count >= 4)
		{
			CardPackData cardPack2 = new CardPackData
			{
				ID = 2,
				Name = "Advanced Pack",
				Cost = 300,
				Cards = new List<CardData>
				{
					availableCards[2],
					availableCards[3]
				}
			};
			availableCardPacks.Add(cardPack2);
		}

		foreach (CardPackData cardPack in availableCardPacks)
		{
			PackedScene cardPackScene = GD.Load<PackedScene>("res://scenes/ShopCardPack.tscn");
			ShopCardPack cardPackNode = (ShopCardPack)cardPackScene.Instantiate();
			cardPackNode.SetCardPackData(cardPack.ID, cardPack.Name, cardPack.Cost, cardPack.Cards);
			
			packsHBoxContainer.AddChild(cardPackNode);
		}
	}

	private void OnBuyCardPack(int id)
	{
		CardPackData cardPack = availableCardPacks.Find(pack => pack.ID == id);
		if (cardPack != null)
		{
			if (PurchaseCardPack(cardPack))
			{
				GD.Print("Successfully purchased card pack: " + cardPack.Name);
				// Actualiza la interfaz de usuario o maneja la compra exitosa
			}
			else
			{
				GD.Print("Failed to purchase card pack: " + cardPack.Name);
			}
		}
	}

	public bool PurchaseCardPack(CardPackData cardPack)
	{
		if (playerCoins >= cardPack.Cost)
		{
			playerCoins -= cardPack.Cost;
			availableCardPacks.Remove(cardPack);
			GD.Print("Purchased card pack: " + cardPack.Name);
			return true;
		}
		GD.Print("Not enough coins to purchase card pack: " + cardPack.Name);
		return false;
	}

	private void LoadAvailableUpgrades()
	{
		foreach (string upgrade in upgrades)
		{
			PackedScene upgradeScene = GD.Load<PackedScene>("res://scenes/ShopUpgrade.tscn");
			ShopUpgrade upgradeNode = (ShopUpgrade)upgradeScene.Instantiate();
			upgradeNode.SetUpgradeData(upgrade, 50);
			upgradesHBoxContainer.AddChild(upgradeNode);
		}
	}

	private void LoadAvailableSpecialDice()
	{
		foreach (string dice in specialDice)
		{
			PackedScene diceScene = GD.Load<PackedScene>("res://scenes/ShopDice.tscn");
			ShopDice diceNode = (ShopDice)diceScene.Instantiate();
			diceNode.SetDiceData(dice, 30);
			diceHBoxContainer.AddChild(diceNode);
		}
	}

	public bool PurchaseCard(CardData card)
	{
		if (playerCoins >= card.Cost)
		{
			playerCoins -= card.Cost;
			availableCards.Remove(card);
			GD.Print("Purchased card: " + card.Name);
			return true;
		}
		GD.Print("Not enough coins to purchase card: " + card.Name);
		return false;
	}

	public bool PurchaseUpgrade(string upgrade)
	{
		int upgradeCost = 50;
		if (playerCoins >= upgradeCost)
		{
			playerCoins -= upgradeCost;
			upgrades.Remove(upgrade);
			GD.Print("Purchased upgrade: " + upgrade);
			return true;
		}
		GD.Print("Not enough coins to purchase upgrade: " + upgrade);
		return false;
	}

	public bool PurchaseSpecialDice(string dice)
	{
		int diceCost = 30;
		if (playerCoins >= diceCost)
		{
			playerCoins -= diceCost;
			specialDice.Remove(dice);
			GD.Print("Purchased special dice: " + dice);
			return true;
		}
		GD.Print("Not enough coins to purchase special dice: " + dice);
		return false;
	}
}
