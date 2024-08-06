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
}
