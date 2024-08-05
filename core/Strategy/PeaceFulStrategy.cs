
using System;
using Godot.Collections;

public class PeaceFulStrategy : IStrategy
{
    public Array<aCard> PlanAttack(Player oponent, PlayerBase currentPlayer)
    {
        if (currentPlayer.PlayHand.Cards.Count == 0)
        {
            Array<aCard> cards = new Array<aCard>
            {
                selectRandomCard(currentPlayer.Hand)
            };
            return cards;
        }
        else
        {
            if (currentPlayer.Hand.Cards.Count > 0) {
                Array<aCard> cards = currentPlayer.PlayHand.Cards;
                cards.Add(selectRandomCard(currentPlayer.Hand));
                return cards;
            }

            else {
                return currentPlayer.PlayHand.Cards;
            }
        }
    }

    private aCard selectRandomCard(Hand hand)
    {
        Random random = new Random();
        int index = random.Next(hand.Cards.Count);
        return hand.Cards[index];
    }
}