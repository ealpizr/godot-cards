using Godot;

public partial class CardDraggingState : CardStateBase
{
	const float DRAG_MINIMUM_THRESHOLD = 0.05f;
	private bool minimumDragThresholdElapsed = false;

	public override void Enter()
	{
		Node uiLayer = GetTree().GetFirstNodeInGroup("ui_layer");
		if (uiLayer == null)
		{
			GD.PrintErr("UI Layer not found");
			return;
		}

		Card.Reparent(uiLayer);
		Card.ColorRect.Color = Colors.NavyBlue;

		minimumDragThresholdElapsed = false;
		SceneTreeTimer thresholdTimer = GetTree().CreateTimer(DRAG_MINIMUM_THRESHOLD, false);
		thresholdTimer.Timeout += () => minimumDragThresholdElapsed = true;

	}

	public override void OnInput(InputEvent e)
	{
		if (e is InputEventMouseMotion mouseMotion)
		{
			Card.GlobalPosition = Card.GetGlobalMousePosition() - Card.PivotOffset;
			return;
		}

		if (e is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Right)
		{
			EmitSignal(SignalName.TransitionRequested, this, Variant.From(CardState.Idle));
			return;
		}

		if (e is InputEventMouseButton mb && mb.ButtonIndex == MouseButton.Left && minimumDragThresholdElapsed)
		{
			GetViewport().SetInputAsHandled();
			EmitSignal(SignalName.TransitionRequested, this, Variant.From(CardState.Released));
		}
	}
}
