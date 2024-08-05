using Godot;

public partial class CardIdleState : CardStateBase
{
	public override async void Enter()
	{
		// In Godot, children are added to the scene before the parent is added.
		// This is because the parent can't be added to the scene until all of its children already exist.
		// This means that we might get here before the Card node is ready, so we need to wait for it.
		if (!Card.IsNodeReady())
		{
			await ToSignal(Card, "ready");
		}

		Card.EmitSignal(Card.SignalName.ReparentRequested, Card);
		Card.ColorRect.Color = Colors.WebGreen;
		Card.Label.Text = Card.Name;

		// PivotOffset can be resseted to left top corner on idle state
		Card.PivotOffset = new Vector2(0, 0);
	}

	public override void OnGuiInput(InputEvent e)
	{
		if (e is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left)
		{
			// Set card pivot offset to the mouse position, so the card follows the mouse correctly
			Card.PivotOffset = Card.GetGlobalMousePosition() - Card.GlobalPosition;
			EmitSignal(SignalName.TransitionRequested, this, Variant.From(CardState.Clicked));
		}
	}
}
