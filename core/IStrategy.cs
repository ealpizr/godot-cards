using System;
using Godot.Collections;

public interface IStrategy
{
    public Array<Card> PlanAttack(Player player, PlayerBase currentPlayer);
}

public class PeaceFulStrategy : IStrategy
{
    public Array<Card> PlanAttack(Player player, PlayerBase currentPlayer)
    {
        if (currentPlayer.PlayHand.Cards.Count == 0)
        {
            Array<Card> cards = new Array<Card>
            {
                selectRandomCard(currentPlayer.Hand)
            };
            return cards;
        }
        else
        {
            if (currentPlayer.Hand.Cards.Count > 0) {
                Array<Card> cards = currentPlayer.PlayHand.Cards;
                cards.Add(selectRandomCard(currentPlayer.Hand));
                return cards;
            }

            else {
                return currentPlayer.PlayHand.Cards;
            }
        }
    }

    private Card selectRandomCard(Hand hand)
    {
        Random random = new Random();
        int index = random.Next(hand.Cards.Count);
        return hand.Cards[index];
    }
}