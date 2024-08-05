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
	private string cardType;
	private string cardImage;

	private Shop shop;

	public override void _Ready()
	{
		base._Ready();

		Button buyButton = GetNode<Button>("BuyButton");
		buyButton.Connect("pressed", new Callable(this, nameof(OnBuyButtonPressed)));

		// Busca el nodo Shop en la jerarquía de padres
		Node parent = GetParent();
		while (parent != null && !(parent is Shop))
		{
			parent = parent.GetParent();
		}

		if (parent is Shop foundShop)
		{
			shop = foundShop;
		}
	}

	public void SetCardData(int id, string name, int cost, int attack, int defense, int health, string description, string rarity, int manacost, string type, string image)
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
		cardType = type;
		cardImage = image;

		Label idLabel = GetNode<Label>("Container/ID");
		Label nameLabel = GetNode<Label>("Container/Name");
		Label costLabel = GetNode<Label>("Container/Cost");
		Label attackLabel = GetNode<Label>("Container/Attack");
		Label defenseLabel = GetNode<Label>("Container/Defense");
		Label healthLabel = GetNode<Label>("Container/Health");
		Label descriptionLabel = GetNode<Label>("Container/Description");
		Label rarityLabel = GetNode<Label>("Container/Rarity");
		Label manacostLabel = GetNode<Label>("Container/Mana");
		Label typeLabel = GetNode<Label>("Container/Type");
		TextureRect imageRect = GetNode<TextureRect>("Container/ImageRect");

		idLabel.Text = id.ToString();
		nameLabel.Text = name;
		costLabel.Text = cost.ToString();
		attackLabel.Text = attack.ToString();
		defenseLabel.Text = defense.ToString();
		healthLabel.Text = health.ToString();
		descriptionLabel.Text = description;
		rarityLabel.Text = rarity;
		manacostLabel.Text = manacost.ToString();
		typeLabel.Text = type;
		Texture2D texture = (Texture2D)GD.Load(cardImage);
		imageRect.Texture = texture;
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
