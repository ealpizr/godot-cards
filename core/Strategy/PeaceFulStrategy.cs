
using System;
using Godot.Collections;

public class PeaceFulStrategy : IStrategy
{
    public Array<aCard> PlanAttack(Player oponent, PlayerBase currentPlayer)
    {
        Array<aCard> cards = new Array<aCard>
        {
            selectRandomCard(currentPlayer.Hand)
        };

        return cards;
    }

    private aCard selectRandomCard(Hand hand)
    {
        Random random = new Random();
        int index = random.Next(hand.Cards.Count);
        return hand.Cards[index];
    }
}