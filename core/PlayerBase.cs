using Godot;

public abstract partial class PlayerBase : Control
{
    public abstract int Id {get; }
    public abstract string UserName {get; }
    public abstract int Points {get; }

    public abstract Hand Hand {get; set;}

    public abstract Hand PlayHand {get; set; }

    public HBoxContainer PlayingFieldContainer {get; set; }
}