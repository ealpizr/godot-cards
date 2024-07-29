using System.IO;
using Godot;
using Godot.Collections;
using GodotCards.DesignPatterns.Observer;

// Why so many classes for CPU?
// Feels a little bit overkill.
public abstract class CPUPlayer : PlayerBase
{
	protected CPUPlayer(Hand hand, Hand playHand, Deck deck, Dice dice, EnergyBar energyBar, TurnDelegate turnDelegate) : base(hand, playHand, deck, dice, energyBar, turnDelegate)
	{
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
        this.Deck = deck;
        this.Deck.EmitSignal(Deck.SignalName.LoadCards, cards);
    }

    public override void OnTurnStart()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTurnEnd()
    {
        throw new System.NotImplementedException();
    }
}
