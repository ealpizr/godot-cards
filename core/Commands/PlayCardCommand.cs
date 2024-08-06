using System;
using Godot;

namespace GodotCards.DesignPatterns.Command;

public class PlayCardCommand : IUndoableCommand
{
    private PlayerBase player;
    private Card card;

    public PlayCardCommand(PlayerBase player, Card card) {
        this.player = player;
        this.card = card;
     }

    public void Execute()
    {
        GD.Print("players hand count: " + player.Hand.Cards.Count);
        GD.Print("players play hand count: " + player.PlayHand.Cards.Count);
        if (!player.Hand.Cards.Contains(card))
        {
            GD.Print(card.Name);
            GD.Print("card description: " + card.Description);
            throw new Exception("Illegal move, player does not have this card in their hand.");
        }

        player.Hand.Cards.Remove(card);
        player.PlayHand.Cards.Add(card);
        card.Reparent(player.PlayingFieldContainer);
        // quick fix for the card release bug the monitoring is being set up to true somewhere, I don't know where (only found in clickstate)
        card.DropPointDetector.Monitoring = false;
        card.cardStateMachine.ChangeState(CardState.Released);
    }

    public void Undo()
    {
        if (!player.PlayHand.Cards.Contains(card))
        {
            throw new Exception("Illegal move, player does not have this card in their play hand.");
        }

        player.PlayHand.Cards.Remove(card);
        player.Hand.Cards.Add(card);
        card.Reparent(player.Hand);
    }
}
