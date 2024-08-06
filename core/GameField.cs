using Godot;

// This is meant for the management of the GameField.
// This could be a Singleton and pass the corresponding card to the corresponding play field.
public partial class GameField : CanvasLayer, IGameField
{

	// create a signal to update the corresponding card to this hboxcontainer
	[Signal]
	public delegate void ReparentToHboxContainerEventHandler(Card card, HBoxContainer container);

	public override void _Ready()
	{
		ReparentToHboxContainer += PlaceCard;
	}

	public void PlaceCard(Card card, HBoxContainer container)
	{
		card.Reparent(container);
	}

	public void RemoveCard(Card card, HBoxContainer container)
	{
		container.RemoveChild(card);
	}
}
