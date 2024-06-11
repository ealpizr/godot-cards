using Godot;
using System;

public partial class ShopCard : Control
{
	private int cardID;
	private string cardName;
	private int cardCost;

	public override void _Ready()
	{
		// Conectar el botón de compra al método de compra
		Button buyButton = GetNode<Button>("Container/BuyButton");
		buyButton.Connect("pressed", new Callable(this, nameof(OnBuyButtonPressed)));
	}

	public void SetCardData(int id, string name, int cost)
	{
		cardID = id;
		cardName = name;
		cardCost = cost;

		Label idLabel = GetNode<Label>("Container/ID");
		Label nameLabel = GetNode<Label>("Container/Name");
		Label costLabel = GetNode<Label>("Container/Cost");

		idLabel.Text = id.ToString();
		nameLabel.Text = name;
		costLabel.Text = cost.ToString();
	}

	private void OnBuyButtonPressed()
	{
		Shop shop = (Shop)GetNode("/root/Shop");
		CardData cardData = new CardData { ID = cardID, Name = cardName, Cost = cardCost };
		shop.PurchaseCard(cardData);
	}
}
