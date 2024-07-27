using Godot;
using System.Collections.Generic;


public partial class ShopCardPack : Control
{
	[Signal]
	public delegate void BuyCardPackEventHandler(int id);

	private Label nameLabel;
	private Label costLabel;
	private VBoxContainer cardsContainer;
	private Button buyButton;

	private int id;
	private string name;
	private int cost;
	private List<CardData> cards;

	public override void _Ready()
	{
		nameLabel = GetNode<Label>("NameLabel");
		costLabel = GetNode<Label>("CostLabel");
		cardsContainer = GetNode<VBoxContainer>("CardsContainer");
		buyButton = GetNode<Button>("BuyButton");

		if (nameLabel == null) GD.PrintErr("nameLabel is null");
		if (costLabel == null) GD.PrintErr("costLabel is null");
		if (cardsContainer == null) GD.PrintErr("cardsContainer is null");
		if (buyButton == null) GD.PrintErr("buyButton is null");

		
	}

	public void SetCardPackData(int id, string name, int cost, List<CardData> cards)
	{
		if (nameLabel == null)
		{
			GD.PrintErr("Cannot set name; nameLabel is null.");
			return;
		}

		if (costLabel == null)
		{
			GD.PrintErr("Cannot set cost; costLabel is null.");
			return;
		}

		if (cardsContainer == null)
		{
			GD.PrintErr("Cannot add cards; cardsContainer is null.");
			return;
		}

		this.id = id;
		this.name = name;
		this.cost = cost;
		this.cards = cards;

		nameLabel.Text = name;
		costLabel.Text = "Cost: " + cost.ToString();

		cardsContainer.ClearChildren(); // Limpia los hijos anteriores antes de agregar nuevos

		foreach (CardData card in cards)
		{
			Label cardLabel = new Label();
			cardLabel.Text = card.Name;
			cardsContainer.AddChild(cardLabel);
		}
	}

	private void OnBuyButtonPressed()
	{
		EmitSignal(nameof(BuyCardPackEventHandler), id);
	}
}

// Método de extensión para limpiar hijos de un contenedor
public static class NodeExtensions
{
	public static void ClearChildren(this Node node)
	{
		foreach (Node child in node.GetChildren())
		{
			child.QueueFree();
		}
	}
}
