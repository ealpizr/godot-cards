using Godot;

public partial class GameField : CanvasLayer, IGameField
{
	[Export]
	private HBoxContainer player1Hand;
	[Export]
	private HBoxContainer player2Hand;

	// create a signal to update the corresponding card to this hboxcontainer
	[Signal]
	public delegate void ReparentToHboxContainerEventHandler(Card card, HBoxContainer container);

	public override void _Ready()
	{
		ReparentToHboxContainer += PlaceCard;
	}

    // TODO: Remove this, this is for testing purpose.
	public void PlaceCardtest(Card card)
	{
		card.Reparent(player1Hand);
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