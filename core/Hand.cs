using Godot;

public partial class Hand : HBoxContainer
{
	public override void _Ready()
	{
		//GetTree().Get
		foreach (Node child in GetChildren())
		{
			Card card = (Card)child;
			card.ReparentRequested += OnReparentRequested;
		}
	}

	private void OnReparentRequested(Card card)
	{
		card.Reparent(this);
	}
}
