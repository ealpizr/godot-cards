using System;
using Godot;
using Godot.Collections;

public interface IStrategy
{
    public Array<Card> PlanAttack(Player oponent, PlayerBase currentPlayer);
}