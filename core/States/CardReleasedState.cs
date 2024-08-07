using System;
using Godot;

public partial class CardReleasedState : CardStateBase
{
	private bool inDropPoint = false;

	public override void Enter()
	{
		Card.ColorRect.Color = Colors.DarkViolet;

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

					Card.CardShape.PivotOffset = Card.Size / 2;

					Card.CardShape.RotationDegrees = 90;
				}
				else
				{
					Card.isAttackPosition = true;

					Card.CardShape.PivotOffset = Card.Size / 2;

					Card.CardShape.RotationDegrees = 0;
				}
			}
			else if (mouseButton.ButtonIndex == MouseButton.Right && mouseButton.IsPressed() && Card.IsSelected)  
			{
				if (Card.isAttackPosition) 
				{
					if (!Card.IsAttacking) 
					{
						Card.ColorRect.Color = Colors.PaleVioletRed;
						Card.IsAttacking = true;
					} 
					else 
					{
						Card.ColorRect.Color = Colors.DarkViolet;
						Card.IsAttacking = false;
					}

					GD.Print("Attack position: " + Card.IsAttacking);
				} 
			}
		}
		// leave this after the mouse button event, so we can check if the card is in the drop point. Bad idea to handle this since the code shouldnt be an ordered behavior in this way but it works for now.
		if (Card.DropPointDetector.Monitoring == false) return;
		if (inDropPoint)
		{
			// verification process to see if the targets are in the game field.
			if (Card.Targets[0].GetParent().Name == "GameUI")
			{
				HBoxContainer playerContainer = Card.Targets[0].GetNode<HBoxContainer>("PlayerPlayHand");

				((GameField) Card.Targets[0].GetParent()).EmitSignal(GameField.SignalName.ReparentToHboxContainer, Card, playerContainer);
			}
			Card.DropPointDetector.Monitoring = false;
			
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
