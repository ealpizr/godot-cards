using Godot;
using System;

public partial class ShopDice : Control
{
	private string diceName;
	private int diceCost;

	public void SetDiceData(string name, int cost)
	{
		diceName = name;
		diceCost = cost;

		GetNode<Label>("DiceName").Text = diceName;
		GetNode<Label>("DiceCost").Text = diceCost.ToString();
	}

	private void OnPurchaseButtonPressed()
	{
		Shop shop = GetParent().GetParent().GetParent() as Shop;
		if (shop != null)
		{
			shop.PurchaseSpecialDice(diceName);
		}
	}
}
