using Godot;

public partial class CardReleasedState : CardStateBase
{
	private bool inDropPoint = false;

	public override void Enter()
	{
		Card.ColorRect.Color = Colors.DarkViolet;
		Card.Label.Text = "Released";

		if (Card.Targets.Count > 0)
		{
			inDropPoint = true;
		}
	}

	public override void OnInput(InputEvent e)
	{
		if (e is InputEventMouseButton mouseButton)
		{
			if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.IsPressed() && Card.IsSelected)
			{
				if (Card.isAttackPosition)
				{
					Card.isAttackPosition = false;
					Card.Label.Text = "Defense";

					Card.CardShape.PivotOffset = Card.Size / 2;
					Card.Label.PivotOffset = Card.Size / 2;

					Card.CardShape.RotationDegrees = 90;
					Card.Label.RotationDegrees = -90;
				}
				else
				{
					Card.isAttackPosition = true;

					Card.CardShape.PivotOffset = Card.Size / 2;
					Card.Label.PivotOffset = Card.Size / 2;

					Card.Label.Text = "Attack";
					Card.CardShape.RotationDegrees = 0;
					Card.Label.RotationDegrees = -0;
				}
			}
			else if (mouseButton.ButtonIndex == MouseButton.Right && mouseButton.IsPressed() && Card.IsSelected)  
			{
				if (Card.isAttackPosition) 
				{
					if (Card.ColorRect.Color == Colors.DarkViolet) {
						Card.ColorRect.Color = Colors.Violet;
					} else {
						Card.ColorRect.Color = Colors.DarkViolet;
					}
				} 
				else 
				{

				}
			}
		}

		if (Card.DropPointDetector.Monitoring == false) return;
		if (inDropPoint)
		{
			// verification process to see if the targets are in the game field.
			if (Card.Targets[0].GetParent().Name == "GameUI")
			{
				// moves the card to the the selected play zone.
				HBoxContainer hBoxContainer = Card.Targets[0].GetNode<HBoxContainer>("HBoxContainer");
				((GameField) Card.Targets[0].GetParent()).EmitSignal(GameField.SignalName.ReparentToHboxContainer, Card, hBoxContainer);
			}
			Card.DropPointDetector.Monitoring = false;
			//EmitSignal(SignalName.TransitionRequested, this, Variant.From(CardState.Idle));
			
			return;
		}

		EmitSignal(SignalName.TransitionRequested, this, Variant.From(CardState.Idle));
	}

	public override void OnMouseEnter() {
		Card.IsSelected = true;
	}

	public override void OnMouseExit()
	{
		Card.IsSelected = false;
	}
}
