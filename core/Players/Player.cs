using Godot;
using Godot.Collections;
using GodotCards.DesignPatterns.Observer;
using System.Collections.Generic;

public partial class Player : PlayerBase, IInteractable, IControllable
{
	public Player(Hand hand, Hand playHand, Deck deck, Dice dice, EnergyBar energyBar, TurnDelegate turnDelegate) : base(hand, playHand, deck, dice, energyBar, turnDelegate)
    {

    }

	public int Id { get; }
	public string UserName { get; }

	public List<Card> CartasEliminadas { get; set; } = new List<Card>();
	public int ObjetivosEspecialesCompletados { get; set; }
	public int PuntosExtras { get; set; }

    public override void OnTurnStart()
    {
        GD.Print("OnTurnStart Player");
    }

    public override void OnTurnEnd()
    {
        GD.Print("OnTurnEnd Player");
    }

    public void OnGuiInput(InputEvent e)
	{
		GD.Print("Player being clicked.");
	}

	public override void Init(HBoxContainer container, Hand hand, Deck deck, Array<Card> cards)
	{
		return;
		// this.Hand = hand;
		// this.PlayingFieldContainer = container;
		// this.Deck = deck;
		// this.Deck.EmitSignal(Deck.SignalName.LoadCards, cards);
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
