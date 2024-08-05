using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Deck : Control
{
	public Array<Card> cards;

	public PlayerBase Player { get; set; }

	[Signal]
	public delegate void LoadCardsEventHandler(Array<Card> cardsList);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.LoadCards += LoadCardsToDeck;
	}

	public override void _Input(InputEvent e)
	{
		if (e is InputEventMouseButton mouseButton)
		{
			if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.IsPressed() && e.IsPressed() && this.Name == "PlayerDeck")
			{
				if (GetRect().HasPoint(((InputEventMouseButton)e).Position)) {
					GD.Print("Card selected:", this.cards.Count);
					cards[0].CustomMinimumSize = new Vector2(100, 150);

					// Load a new card scene and populate it with the card data
					Card card = Player.Hand.Cards[0].Duplicate() as Card;

					// population
					aCard reference = cards[0];
					card.Name = reference.Name;
					card.Description = reference.Description;
					card.AttackPoints = reference.AttackPoints;
					card.DefensePoints= reference.DefensePoints;
					card.HealthPoints = reference.HealthPoints;
					card.EnergyCost = reference.EnergyCost;
					GD.Print(reference.Rarity.Text);
					card.RarityValue = reference.RarityValue;
					card.Icon = reference.Icon;
					card.eliminationPoints = reference.eliminationPoints;
					card.Puntos = reference.Puntos;
					card.IsSelected = reference.IsSelected;

					Player.Hand.AddCard((Card)card);
					GD.Print(Player.Hand.GetChildCount());
				}
			}
		}
	}

	public void LoadCardsToDeck(Array<Card> cardsList)
	{
		cards = cardsList;
		Label label = GetChild<Label>(1);
		label.Text = cards.Count.ToString();
	}

}
