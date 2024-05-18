using Godot;

public partial class CardDraggingState : CardStateBase
{
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
        Card.Label.Text = "Dragging";
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

        if (e is InputEventMouseButton mb && mb.ButtonIndex == MouseButton.Left)
        {
            GetViewport().SetInputAsHandled();
            EmitSignal(SignalName.TransitionRequested, this, Variant.From(CardState.Released));
        }
    }
}
