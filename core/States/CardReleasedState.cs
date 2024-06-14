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
            GD.Print(Card.Targets[0].Name);
            inDropPoint = true;
        }
    }

    public override void OnInput(InputEvent e)
    {
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
}
