using Godot;
using System.Collections.Generic;

public partial class Shop : Node
{
	private int playerCoins;
	private List<Card> availableCards;
	private List<string> upgrades;
	private List<string> specialDice;

	public override void _Ready()
	{
		// Inicializa la tienda con algunos datos
		playerCoins = 100; // Ejemplo: El jugador comienza con 100 monedas
		availableCards = new List<Card>(); // Añade instancias de cartas disponibles
		upgrades = new List<string> { "Energy Bar Upgrade", "Shield Upgrade" };
		specialDice = new List<string> { "Fire Dice", "Water Dice" };

		GD.Print("Shop is ready with initial setup.");
	}

	public bool PurchaseCard(Card card)
	{
		if (playerCoins >= card.EnergyCost)
		{
			playerCoins -= card.EnergyCost;
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
