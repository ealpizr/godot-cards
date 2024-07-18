using Godot;

public abstract partial class PlayerBase : Control, IInteractable
{
    public abstract int Id {get; }
    public abstract string UserName {get; }
    public abstract int Points {get; }

    public abstract Hand Hand {get; set;}

    public abstract Hand PlayHand {get; set; }

    public HBoxContainer PlayingFieldContainer {get; set; }

    public abstract void Init(HBoxContainer container, Hand hand);
    public abstract void ReceiveInteraction(PlayerBase interaction);
    public abstract void SendInteraction(GameField gameField, PlayerBase interaction);
}