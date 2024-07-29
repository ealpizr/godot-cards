using System;

namespace GodotCards.DesignPatterns.Command;

public class PlayCardCommand : IUndoableCommand
{
    private PlayerBase player;
    private Card card;

    public PlayCardCommand(PlayerBase player, Card card) { }

    public void Execute()
    {
        if (!player.Hand.Cards.Contains(card))
        {
            throw new Exception("Illegal move, player does not have this card in their hand.");
        }

        player.Hand.Cards.Remove(card);
        player.PlayHand.Cards.Add(card);
        card.Reparent(player.PlayingFieldContainer);
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
