using System.Collections;
using Godot.Collections;

public class PlayContext
{
    private IStrategy _strategy;

    public PlayContext(IStrategy strategy)
    {
        this._strategy = strategy;
    }

    public PlayContext()
    {

    }

    public void SetStrategy(IStrategy strategy)
    {
        this._strategy = strategy;
    }

    public Array<Card> PlanAttack(Player player, PlayerBase currentPlayer)
    {
        return this._strategy.PlanAttack(player, currentPlayer);
    }
}