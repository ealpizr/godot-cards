using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CardData
{
	[JsonPropertyName("id")]
	public int ID { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("cost")]
	public int Cost { get; set; }
}

public partial class Shop : Control
{
	private int playerCoins;
	private List<CardData> availableCards;
	private List<string> upgrades;
	private List<string> specialDice;

	public override void _Ready()
	{
		InitializeShop();
		LoadAvailableCards();
	}

	private void InitializeShop()
	{
		playerCoins = 100; // Ejemplo: El jugador comienza con 100 monedas
		upgrades = new List<string> { "Energy Bar Upgrade", "Shield Upgrade" };
		specialDice = new List<string> { "Fire Dice", "Water Dice" };

		// Conectar botones a métodos de compra
		Button purchaseCardButton = GetNode<Button>("MarginContainer/VBoxContainer/PurchaseCardButton");
		Button purchaseUpgradeButton = GetNode<Button>("MarginContainer/VBoxContainer/PurchaseUpgradeButton");
		Button purchaseDiceButton = GetNode<Button>("MarginContainer/VBoxContainer/PurchaseDiceButton");

		purchaseCardButton.Connect("pressed", new Callable(this, nameof(OnPurchaseCardButtonPressed)));
		purchaseUpgradeButton.Connect("pressed", new Callable(this, nameof(OnPurchaseUpgradeButtonPressed)));
		purchaseDiceButton.Connect("pressed", new Callable(this, nameof(OnPurchaseDiceButtonPressed)));

		GD.Print("Shop is ready with initial setup.");
	}

	private async void LoadAvailableCards()
	{
		Nakama.Client client = GlobalState.Instance.NakamaClient;
		Nakama.ISession session = GlobalState.Instance.Session;

		Nakama.IApiRpc response = await client.RpcAsync(session, "GetAvailableCards");
		availableCards = JsonSerializer.Deserialize<List<CardData>>(response.Payload);

		GridContainer cardContainer = GetNode<GridContainer>("Container");

		foreach (CardData card in availableCards)
		{
			Node cardNode = GD.Load<PackedScene>("res://scenes/Shop_card.tscn").Instantiate();
			((ShopCard)cardNode).SetCardData(card.ID, card.Name, card.Cost);

			cardContainer.AddChild(cardNode);
		}
	}

	private void OnPurchaseCardButtonPressed()
	{
		// Aquí selecciona la carta que deseas comprar
		// Ejemplo: Compra la primera carta en la lista de cartas disponibles
		if (availableCards.Count > 0)
		{
			PurchaseCard(availableCards[0]);
		}
	}

	private void OnPurchaseUpgradeButtonPressed()
	{
		// Aquí selecciona la mejora que deseas comprar
		// Ejemplo: Compra la primera mejora en la lista de mejoras
		if (upgrades.Count > 0)
		{
			PurchaseUpgrade(upgrades[0]);
		}
	}

	private void OnPurchaseDiceButtonPressed()
	{
		// Aquí selecciona el dado especial que deseas comprar
		// Ejemplo: Compra el primer dado especial en la lista de dados especiales
		if (specialDice.Count > 0)
		{
			PurchaseSpecialDice(specialDice[0]);
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
		int upgradeCost = 50; // Ejemplo de costo de mejora
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
		int diceCost = 30; // Ejemplo de costo de dado especial
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
