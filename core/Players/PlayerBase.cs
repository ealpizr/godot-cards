using System;
using Godot;
using Godot.Collections;
using GodotCards.DesignPatterns.Observer;

public abstract partial class PlayerBase :  IInteractable, ITurnAware
{
    public TurnObserver TurnObserver { get; set; }

	public PlayerBase(Hand hand, Hand playHand, Deck deck, Dice dice, EnergyBar energyBar, TurnDelegate turnDelegate)
	{
		this.Hand = hand;
		this.PlayHand = playHand;
		this.Deck = deck;
		this.Deck.Player = this;
		this.Dice = dice;
		this.EnergyBar = energyBar;
		TurnObserver = new TurnObserver(this, turnDelegate);
		GlobalState globalState = GlobalState.Instance;
	}

	public int Points { get; set; }

	public Hand Hand {get; set;}

	public Hand PlayHand {get; set; }

	public Deck Deck {get; set;}

	public Dice Dice { get; set; }

	public EnergyBar EnergyBar { get; set; }

	public HBoxContainer PlayingFieldContainer {get; set; }

	public abstract void Init(HBoxContainer container, Hand hand, Deck deck, Array<Card> cards);
	public abstract void ReceiveInteraction(PlayerBase interaction);
	public abstract void SendInteraction(GameField gameField, PlayerBase interaction);

	public abstract void OnTurnStart();
	public abstract void OnTurnEnd();
}
