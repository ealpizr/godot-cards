using Godot;
using System;

public partial class ShopUpgrade : Control
{
	private string upgradeName;
	private int upgradeCost;

	public void SetUpgradeData(string name, int cost)
	{
		upgradeName = name;
		upgradeCost = cost;

		GetNode<Label>("UpgradeName").Text = upgradeName;
		GetNode<Label>("UpgradeCost").Text = upgradeCost.ToString();
	}

	private void OnPurchaseButtonPressed()
	{
		Shop shop = GetParent().GetParent().GetParent() as Shop;
		if (shop != null)
		{
			shop.PurchaseUpgrade(upgradeName);
		}
	}
}
