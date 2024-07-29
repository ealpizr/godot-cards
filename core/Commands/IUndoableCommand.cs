namespace GodotCards.DesignPatterns.Command;

public interface IUndoableCommand : ICommand
{
    void Undo();
}
