using Godot;
using System;

public partial class ShopCard : Control
{
	private int cardID;
	private string cardName;
	private int cardCost;
	private int cardAttack;
	private int cardDefense;
	private int cardHealth;
	private string cardDescription;
	private string cardRarity;
	private int cardManaCost;
	private string cardImage;

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

	public void SetCardData(int id, string name, int cost, int attack, int defense, int health, string description, string rarity, int manacost, string image)
	{
		cardID = id;
		cardName = name;
		cardCost = cost;
		cardAttack = attack;
		cardDefense = defense;
		cardHealth = health;
		cardDescription = description;
		cardRarity = rarity;
		cardManaCost = manacost;
		cardImage = image;

		GetNode<TextureRect>("Background/Image").Texture = GD.Load<Texture2D>(cardImage);
		GetNode<Label>("Background/Name").Text = cardName;
		GetNode<Label>("Background/Cost").Text = $"🪙 {cardCost.ToString()}";
		GetNode<Label>("Background/Attack").Text = $"⚔ {cardAttack.ToString()}";
		GetNode<Label>("Background/Defense").Text = $"🛡️ {cardDefense.ToString()}";
		GetNode<Label>("Background/Health").Text = $"❤ {cardHealth.ToString()}";
		GetNode<Label>("Background/Description").Text = cardDescription;
		GetNode<Label>("Background/Rarity").Text = cardRarity;
		GetNode<Label>("Background/ManaCost").Text = cardManaCost.ToString();
		GetNode<Label>("Background/ID").Text = cardID.ToString();
	}
	
	public void SetShopReference(Shop shopRef)
	{
		shop = shopRef;
	}

	private void OnBuyButtonPressed()
	{
		if (shop != null)
		{
			if (shop.CanAfford(cardCost))
			{
				shop.BuyCard(cardID, cardCost);
				GD.Print("Card purchased!");
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
