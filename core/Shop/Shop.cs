using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nakama;
using System.Security.Cryptography.X509Certificates;

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
	
	[JsonPropertyName("defense")]
	public int Defense { get; set; }
	
	[JsonPropertyName("health")]
	public int Health { get; set; }
	
	[JsonPropertyName("description")]
	public string Description { get; set; }
	
	[JsonPropertyName("rarity")]
	public string Rarity { get; set; }
	
	[JsonPropertyName("manaCost")]
	public int ManaCost { get; set; }
	
	[JsonPropertyName("type")]
	public string Type { get; set; }
	
	[JsonPropertyName("image")]
	public string Image { get; set; }
}
public class DiceData
{
	[JsonPropertyName("id")]
	public int ID { get; set; }

	[JsonPropertyName("max")]
	public int Max { get; set; }

	[JsonPropertyName("min")]
	public int Min { get; set; }

	[JsonPropertyName("cost")]
	public int Cost { get; set; }
	
	[JsonPropertyName("name")]
	public string Name { get; set; }
	
	[JsonPropertyName("rarity")]
	public string Rarity { get; set; }
	
	[JsonPropertyName("description")]
	public string Description { get; set; }
	
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
	private List<DiceData> availableDice;
	private List<CardPackData> availableCardPacks;
	private List<string> upgrades;

	private HBoxContainer cardHBoxContainer;
	private HBoxContainer packsHBoxContainer;
	private HBoxContainer diceHBoxContainer;
	private HBoxContainer upgradesHBoxContainer;
	
	private int cCoins;
	private Coins coinsUI;

	public override void _Ready()
	{
		InitializeShop();
		SetupContainers();
		LoadAvailableCards();
		LoadAvailableDice();
		LoadAvailableUpgrades();
		
		Button exitButton = GetNode<Button>("ExitToMenuButton");
		exitButton.Connect("pressed", new Callable(this, nameof(OnExitToMenuButtonPressed)));
		
		cCoins = 1000;

		
		coinsUI = GetNode<Coins>("CoinsUI"); 
		UpdateUI();
	}

	private void InitializeShop()
	{
		playerCoins = 1000;
		upgrades = new List<string> { "Energy Bar Upgrade", "Shield Upgrade" };

		GD.Print("Shop is ready with initial setup.");
	}
	
	public bool CanAfford(int cost)
	{
		return cCoins >= cost;
	}

	public void BuyCard(int cardID, int cost)
	{
		if (CanAfford(cost))
		{
			CardData cardSelected = null;

			foreach (CardData card in availableCards)
			{
				if (card.ID == cardID)
				{
					cardSelected = card;
					break;
				}
			}

			cCoins -= cost;
			GD.Print($"Card with ID {cardID} purchased for {cost} C-Coins.");

			GlobalState globalState = GlobalState.Instance;

			aCard purchasedCard = new Card(cardSelected.Name, cardSelected.Description, cardSelected.ManaCost, cardSelected.Attack, 100, cardSelected.Health);

			GD.Print(cardSelected.Rarity);	
			switch (cardSelected.Rarity)
			{
				case "Common":
					purchasedCard = new NormalCard(purchasedCard);
					break;
				case "Legendaria":
					purchasedCard = new LegendariaCard(purchasedCard);
					break;
					
			}

			GD.Print(purchasedCard.Puntos);
			globalState.purchasedCards.Add((Card) purchasedCard);
			UpdateUI();
		}
	}

	private void UpdateUI()
	{
		if (coinsUI != null)
		{
			coinsUI.UpdateUI(cCoins);
		}
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
			((ShopCard)cardNode).SetCardData(card.ID, card.Name, card.Cost, card.Attack, card.Defense, card.Health, card.Description, card.Rarity, card.ManaCost, card.Image);
			cardNode.SetShopReference(this);
			cardHBoxContainer.AddChild(cardNode);
		}
	}
	
	private async void LoadAvailableDice()
	{
		Nakama.Client client = GlobalState.Instance.NakamaClient;
		Nakama.ISession session = GlobalState.Instance.Session;

		Nakama.IApiRpc response = await client.RpcAsync(session, "GetAvailableDice");
		availableDice = JsonSerializer.Deserialize<List<DiceData>>(response.Payload);

		foreach (DiceData dice in availableDice)
		{
			PackedScene diceScene = GD.Load<PackedScene>("res://scenes/ShopDice.tscn");
			ShopDice diceNode = (ShopDice)diceScene.Instantiate();
			diceNode.SetDiceData(dice.ID, dice.Max ,dice.Min, dice.Cost, dice.Name, dice.Rarity, dice.Description);
			diceNode.SetShopReference(this);
			diceHBoxContainer.AddChild(diceNode);
		}
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

	private void OnExitToMenuButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/menu.tscn");
	}
}
