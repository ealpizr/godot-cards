using Godot;
using System;

public partial class ShopDice : Control
{
	private int diceID;
	private int diceMax;
	private int diceMin;
	private int diceCost;
	private string diceName;
	private string diceRarity;
	private string diceDescription;

	private Shop shop;
	private Label descriptionLabel;
	private ColorRect colorOverlay;

	public override void _Ready()
	{
		base._Ready();

		Button buyButton = GetNode<Button>("BuyButton");
		buyButton.Connect("pressed", new Callable(this, nameof(OnBuyButtonPressed)));
		
		Node parent = GetParent();
		while (parent != null && !(parent is Shop))
		{
			parent = parent.GetParent();
		}

		if (parent is Shop foundShop)
		{
			shop = foundShop;
		}

		descriptionLabel = GetNode<Label>("Background/Description");
		descriptionLabel.Visible = false;

		colorOverlay = GetNode<ColorRect>("Background/ColorOverlay");
		colorOverlay.Visible = false;
		
		Connect("mouse_entered", new Callable(this, nameof(_on_mouse_entered)));
		Connect("mouse_exited", new Callable(this, nameof(_on_mouse_exited)));
	}

	private void _on_mouse_entered()
	{
		descriptionLabel.Visible = true;
		colorOverlay.Visible = true;
	}

	private void _on_mouse_exited()
	{
		descriptionLabel.Visible = false;
		colorOverlay.Visible = false;
	}

	public void SetDiceData(int id, int max, int min, int cost, string name, string rarity, string description)
	{
		diceID = id;
		diceMax = max;
		diceMin = min;
		diceCost = cost;
		diceName = name;
		diceRarity = rarity;
		diceDescription = description;

		GetNode<Label>("Background/ID").Text = diceID.ToString();
		GetNode<Label>("Background/Max").Text = $"➕ {diceMax.ToString()}";
		GetNode<Label>("Background/Min").Text = $"➖ {diceMin.ToString()}";
		GetNode<Label>("Background/Cost").Text = $"🪙 {diceCost.ToString()}";
		GetNode<Label>("Background/Name").Text = diceName;
		GetNode<Label>("Background/Rarity").Text = diceRarity;
		GetNode<Label>("Background/Description").Text = diceDescription;	
	}
	
	public void SetShopReference(Shop shopRef)
	{
		shop = shopRef;
	}

	private void OnBuyButtonPressed()
	{
		if (shop != null)
		{
			if (shop.CanAfford(diceCost))
			{
				shop.BuyCard(diceID, diceCost);
				GD.Print("Dice purchased!");
			}
			else
			{
				GD.Print("Not enough C-Coins.");
			}
		}
		else
		{
			GD.Print("No se encontró el nodo Shop.");
		}
	}
}
