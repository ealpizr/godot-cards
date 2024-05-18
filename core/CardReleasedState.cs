using Godot;

public partial class CardReleasedState : CardStateBase
{
    public override void Enter()
    {
        Card.ColorRect.Color = Colors.DarkViolet;
        Card.Label.Text = "Released";
    }
}
