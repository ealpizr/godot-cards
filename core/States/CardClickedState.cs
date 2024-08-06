using Godot;

public partial class CardClickedState : CardStateBase
{
	public override void Enter()
	{
		Card.ColorRect.Color = Colors.Orange;
		GD.Print("CardClickedState");
		// When we click a card, we start interacting with it, so we need to monitor the drop point detector
		// to check for collisions with the drop area.
		Card.DropPointDetector.Monitoring = true;
	}

	public override void OnGuiInput(InputEvent e)
	{
		if (e is InputEventMouseMotion mouseMotion)
		{
			EmitSignal(SignalName.TransitionRequested, this, Variant.From(CardState.Dragging));
		}
	}
}
