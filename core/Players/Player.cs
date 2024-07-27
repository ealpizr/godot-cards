using Godot;
using Godot.Collections;

public partial class Player: PlayerBase, IInteractable, IControllable
{
    public override int Id { get; }
    public override string UserName { get; }
    public override int Points { get; set; }

    public override Hand Hand { get; set; }

    public override Hand PlayHand {get; set; }
    public override Deck deck { get; set; }


    public void OnGuiInput(InputEvent e)
    {
        GD.Print("Player being clicked.");
    }

    public override void Init(HBoxContainer container, Hand hand, Deck deck, Array<Card> cards)
    {
        this.Hand = hand;
        this.PlayingFieldContainer = container;
        this.deck = deck;
        this.deck.EmitSignal(Deck.SignalName.LoadCards, cards);
    }

    public void OnInput(InputEvent e)
    {
        throw new System.NotImplementedException();
    }

    public void OnMouseEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnMouseExit()
    {
        throw new System.NotImplementedException();
    }

    public override void ReceiveInteraction(PlayerBase interaction)
    {
        GD.Print("Player received interaction.");
    }

    public override void SendInteraction(GameField gameField, PlayerBase interaction)
    {
        throw new System.NotImplementedException();
    }
} 