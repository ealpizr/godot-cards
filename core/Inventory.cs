using Godot;
using godotcards.core.Api;
using Nakama;
using System;
using System.Text.Json;

public partial class Inventory : Control
{
	private readonly Nakama.Client _client = GlobalState.Instance.NakamaClient;
	private readonly Nakama.ISession _session = GlobalState.Instance.Session;

	public override void _Ready()
	{
		RenderSidebarMenu();
		LoadPlayerCoins();
		LoadPlayerInventory();
		LoadPlayerDice();
	}

	private async void LoadPlayerDice()
	{
		IApiRpc rpcReponse = await _client.RpcAsync(_session, "GetUserDice");
		Dice dice = JsonSerializer.Deserialize<Dice>(rpcReponse.Payload);

		GetNode<Label>("VBoxContainer/HBoxContainer/ColorRect/HBoxContainer/VBoxContainer/Label2").Text = dice.Name;
		GetNode<Label>("VBoxContainer/HBoxContainer/ColorRect/HBoxContainer/VBoxContainer/Label").Text = dice.Description;
	}

	private async void LoadPlayerInventory()
	{
		IApiRpc rpcReponse = await _client.RpcAsync(_session, "GetUserInventory");
		UserInventory inventory = JsonSerializer.Deserialize<UserInventory>(rpcReponse.Payload);

		GetNode<Label>("Sidebar/Content/StatsContainer/VBoxContainer/CardsContainer/Cards").Text = inventory.Cards.Count.ToString();
		GetNode<Label>("Sidebar/Content/StatsContainer/VBoxContainer/DiceContainer/Dice").Text = inventory.Dice.Count.ToString();
	
		GridContainer cardContainer = GetNode<GridContainer>("VBoxContainer/HBoxContainer2/ColorRect/ScrollContainer/GridContainer");
		foreach (godotcards.core.Api.Card card in inventory.Cards)
		{
			PackedScene cardScene = GD.Load<PackedScene>("res://scenes/inventory_card.tscn");
			InventoryCard inventoryCard = (InventoryCard)cardScene.Instantiate();
			inventoryCard.SetCard(card);
			cardContainer.AddChild(inventoryCard);
		}
	}

	private async void LoadPlayerCoins()
	{
		Nakama.IApiAccount account = await _client.GetAccountAsync(_session);
		Wallet wallet = JsonSerializer.Deserialize<Wallet>(account.Wallet);

		GetNode<Label>("Sidebar/Content/StatsContainer/VBoxContainer/CoinsContainer/Coins").Text = wallet.Coins.ToString();
	}

	private void RenderSidebarMenu()
	{
		// We should probably just use a single button and register the click event to change the scene.
		// Lets use the menu builder for now.

		NavigationTypeMenuBuilder navigationTypeMenuBuilder = new NavigationTypeMenuBuilder();
		MenuDirector menuDirector = new MenuDirector(navigationTypeMenuBuilder);

		menuDirector.AddNavigateToSceneButton("Home", "Regresar", "res://scenes/menu.tscn", 0);

		menuDirector.Construct();
		INavigation navigation = menuDirector.GetMenu();
		navigation.Init(GetNode<VBoxContainer>("Sidebar/Content/StatsContainer/VBoxContainer/ButtonsContainer"));
		navigation.Open();
	}
}
