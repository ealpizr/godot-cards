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
        if (inDropPoint)
        {
            //Card.SetGlobalPosition(new Vector2(0, 0));
            return;
        }

        EmitSignal(SignalName.TransitionRequested, this, Variant.From(CardState.Idle));
    }
}
