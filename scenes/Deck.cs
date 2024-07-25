using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Deck : Control
{
	public Array<Card> cards;

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
			if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.IsPressed() && e.IsPressed())
			{
				if (GetRect().HasPoint(((InputEventMouseButton)e).Position)) {
					GD.Print("Deck clicked" + this.Name);
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
