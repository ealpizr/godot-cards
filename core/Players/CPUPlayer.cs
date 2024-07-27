using System.IO;
using Godot;
using Godot.Collections;

public partial class CPUPlayer : PlayerBase
{
	public override int Id { get; }
	public override string UserName { get; }
	public override int Points { get; set; }

	public override Hand Hand { get; set; }

	public override Hand PlayHand {get; set; }
  public override Deck deck { get; set; }

	public CPUPlayer() {
		this.PlayHand = new Hand();
	}

	public override void ReceiveInteraction(PlayerBase interaction) {
		
	}
  
	public override void SendInteraction(GameField gameField, PlayerBase interaction) {
		if (!this.PlayHand.HandStatus || !this.Hand.HandStatus) {
			GD.Print("Player out of turn...");
			return;
		}
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

	  public override void Init(HBoxContainer container, Hand hand, Deck deck, Array<Card> cards)
    {
        this.Hand = hand;
        this.PlayingFieldContainer = container;
        this.deck = deck;
        this.deck.EmitSignal(Deck.SignalName.LoadCards, cards);
    }
}
