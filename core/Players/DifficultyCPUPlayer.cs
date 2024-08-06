using System;
using Godot;
using Godot.Collections;
using GodotCards.DesignPatterns.Command;
using GodotCards.DesignPatterns.Observer;
using Microsoft.VisualBasic;

// Why so many classes for CPU?
// Feels a little bit overkill.
public partial class DifficultyCPUPlayer : CustomCPUPlayer
{
    private readonly TurnManager turnManager;
    private readonly ActionManager actionManager;

    public DifficultyCPUPlayer(PlayerBase player, TurnManager turnManager, ActionManager actionManager) : base(player, player.Hand, player.PlayHand, player.Deck, player.Dice, player.EnergyBar, player.TurnObserver.TurnDelegate)
    {
        this.interactable = player;
        this.turnManager = turnManager;
        this.actionManager = actionManager;

        this.Strategy.SetStrategy(new PeaceFulStrategy());
    }

    public override void OnTurnStart()
    {
        ICommand rollDiceCommand = new RollDiceCommand(this);
        actionManager.ExecuteAction(rollDiceCommand);

        ICommand drawCardFromDeckCommand = new DrawCardFromDeckCommand(this);
        actionManager.ExecuteAction(drawCardFromDeckCommand);

        // Executing strategy based on the current state of the game.
        Array<aCard> cardsToPlay = this.Strategy.PlanAttack((Player)this.interactable, (PlayerBase)this.interactable);

        GD.Print("Playing cards from strategy.", cardsToPlay.Count);
        // Place cards on the field.
        foreach (Card card in cardsToPlay)
        {
            ICommand placeCardOnFieldCommand = new PlayCardCommand(this, card);
            actionManager.ExecuteAction(placeCardOnFieldCommand);
        }
        
        ICommand endTurnCommand = new EndTurnCommand(this.turnManager);
        actionManager.ExecuteAction(endTurnCommand);
    }

    public override void OnTurnEnd()
    {
        // We can do something here. Maybe show an alert or message.
    }

    // don't forget to change to override.
    // https://learn.microsoft.com/es-es/dotnet/csharp/misc/cs0506
    public new void ReceiveInteraction(PlayerBase interaction)
    {
        throw new System.NotImplementedException();
    }

    public override void ChangeLevel(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                this.Strategy.SetStrategy(new PeaceFulStrategy());
                break;
            case Difficulty.Medium:
                this.Strategy.SetStrategy(new AggressiveStrategy());
                break;
            default:
                this.Strategy.SetStrategy(new PeaceFulStrategy());
                break;
        }
    }

    public override void SendInteraction(GameField gameField, PlayerBase interaction)
    {
        if (((PlayerBase)interactable).Hand is null)
        {
            GD.Print("Hand is null. Please verify the hand is being initialized.");
        }

        ((PlayerBase)this.interactable).PlayHand.Cards = Strategy.PlanAttack((Player)interaction, (PlayerBase)this.interactable);

        base.SendInteraction(gameField, interaction);
        // behaviour such as: verify that the player is capable of winning.
        //this.interactable.SendInteraction(this);
    }
}