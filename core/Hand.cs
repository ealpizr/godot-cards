using Godot;
using Godot.Collections;

public partial class Hand : HBoxContainer
{
	public Array<Card> Cards = new Array<Card>();
	public override void _Ready()
	{
		//GetTree().Get
		foreach (Node child in GetChildren())
		{
			Card card = (Card)child;
			Cards.Add(card);
			card.ReparentRequested += OnReparentRequested;
		}
	}

	private void OnReparentRequested(Card card)
	{
		card.Reparent(this);
	}
}
