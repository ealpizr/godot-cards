using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Deck : Control
{
	public Stack<Card> Cards = new Stack<Card>();

	// Most likely don't need this
	[Signal]
	public delegate void LoadCardsEventHandler(Array<Card> cardsList);

	[Signal]
	public delegate void ClickEventHandler();

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
					EmitSignal(nameof(SignalName.Click));
				}
			}
		}
	}

	public void LoadCardsToDeck(Array<Card> cardsList)
	{
		Cards = new Stack<Card>(cardsList);
        RenderDeck();
	}

	public void RenderDeck()
    {
        Label label = GetChild<Label>(1);
        label.Text = Cards.Count.ToString();
    }

}
