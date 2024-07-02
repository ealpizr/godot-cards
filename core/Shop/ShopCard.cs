using Godot;
using System;

public partial class ShopCard : Control
{
	private int cardID;
	private string cardName;
	private int cardCost;
	private int cardAttack;
	private int cardHealth;
	private string cardDescription;
	private string cardRarity;
	private int cardManaCost;
	private string cardType;

	public override void _Ready()
	{
		base._Ready();

		Button buyButton = GetNode<Button>("BuyButton");
		buyButton.Connect("pressed", new Callable(this, nameof(OnBuyButtonPressed)));
	}

	public void SetCardData(int id, string name, int cost, int attack, int health, string description, string rarity, int manacost, string type)
	{
		cardID = id;
		cardName = name;
		cardCost = cost;
		cardAttack = attack;
		cardHealth = health;
		cardDescription = description;
		cardRarity = rarity;
		cardManaCost = manacost;
		cardType = type;

		Label idLabel = GetNode<Label>("Container/ID");
		Label nameLabel = GetNode<Label>("Container/Name");
		Label costLabel = GetNode<Label>("Container/Cost");
		Label attackLabel = GetNode<Label>("Container/Attack");
		Label healthLabel = GetNode<Label>("Container/Health");
		Label descriptionLabel = GetNode<Label>("Container/Description");
		Label rarityLabel = GetNode<Label>("Container/Rarity");
		Label manacostLabel = GetNode<Label>("Container/Mana");
		Label typeLabel = GetNode<Label>("Container/Type");

		idLabel.Text = id.ToString();
		nameLabel.Text = name;
		costLabel.Text = cost.ToString();
		attackLabel.Text = attack.ToString();
		healthLabel.Text = health.ToString();
		descriptionLabel.Text = description;
		rarityLabel.Text = rarity;
		manacostLabel.Text = manacost.ToString();
		typeLabel.Text = type;
		
	}

	private void OnBuyButtonPressed()
	{
		Shop shop = GetParent<Shop>();
		if (shop.PurchaseCard(new CardData { ID = cardID, Name = cardName, Cost = cardCost }))
		{
			GD.Print("Card purchased successfully.");
		}
		else
		{
			GD.Print("Card purchase failed.");
		}
	}
}
