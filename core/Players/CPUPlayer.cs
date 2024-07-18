using System.IO;
using Godot;

public partial class CPUPlayer : PlayerBase
{
    public override int Id { get; }
    public override string UserName { get; }
    public override int Points { get; }

    public override Hand Hand { get; set; }

    public override Hand PlayHand {get; set; }

    public CPUPlayer() {
        this.PlayHand = new Hand();
    }

    public override void ReceiveInteraction(PlayerBase interaction) {
        
    }
    public override void SendInteraction(GameField gameField, PlayerBase interaction) {
        // default behaviour here, one base execution example
        // we could add things like: verification process for cards.
        if (PlayHand.Cards is not null)
        {
            GD.Print("Playing cards...");
            GD.Print(PlayHand.Cards.Count);
            foreach (Card item in PlayHand.Cards)
            {  
                GD.Print("Playing card: " + item.Name);
                GD.Print(item.Name);
                gameField.EmitSignal(GameField.SignalName.ReparentToHboxContainer, item, this.PlayingFieldContainer);

            }
        }

        else
        {
            GD.Print("No cards to play.");
        }
        ((Player)interaction).ReceiveInteraction(this);
    }

    public override void Init(HBoxContainer container, Hand hand)
    {
        this.Hand = hand;
        this.PlayingFieldContainer = container;
    }
}