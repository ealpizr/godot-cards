using Godot;
using Godot.Collections;

public partial class Hand : HBoxContainer
{
	public Array<Card> Cards = new Array<Card>();
	public bool HandStatus { get; set; }
	public override void _Ready()
	{
		//GetTree().Get
		foreach (Node child in GetChildren())
		{
			if (child is Card card)
			{
				Cards.Add(card);
				card.ReparentRequested += OnReparentRequested;
			}
		}
	}
	
	private void OnReparentRequested(Card card)
	{
		card.Reparent(this);
	}
}
