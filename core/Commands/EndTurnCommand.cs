using GodotCards.DesignPatterns.Observer;

namespace GodotCards.DesignPatterns.Command;

public class EndTurnCommand : ICommand
{
    private readonly TurnManager turnManager;

    public EndTurnCommand(TurnManager turnManager)
    {
        this.turnManager = turnManager;
    }

    public void Execute()
    {
        turnManager.EndTurn();
    }
}
