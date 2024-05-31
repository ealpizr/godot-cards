using Godot;

public partial class Hand : HBoxContainer
{
	public override void _Ready()
	{
		foreach (Node child in GetChildren())
		{
			if (child is Card card)
			{
				card.ReparentRequested += OnReparentRequested;
			}
		}
	}
	
	private void OnReparentRequested(Card card)
	{
		card.Reparent(this);
	}
}
