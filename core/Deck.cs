using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Deck : Control
{
	public Stack<aCard> Cards = new Stack<aCard>();

	public PlayerBase Player { get; set; }

	[Signal]
	public delegate void ClickEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//this.LoadCards += LoadCardsToDeck;
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

	public void LoadCardsToDeck(Array<aCard> cardsList)
	{
		Cards = new Stack<aCard>(cardsList);
        RenderDeck();
	}

	public void RenderDeck()
    {
        Label label = GetChild<Label>(1);
        label.Text = Cards.Count.ToString();
    }

}
