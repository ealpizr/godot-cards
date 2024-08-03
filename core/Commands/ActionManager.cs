using System;
using System.Collections.Generic;

namespace GodotCards.DesignPatterns.Command;

public class ActionManager
{
    private readonly Stack<ICommand> commandStack = new Stack<ICommand>();
    private readonly Stack<ICommand> undoStack = new Stack<ICommand>();

    public void ExecuteAction(ICommand action)
    {
        action.Execute();
        commandStack.Push(action);
    }

    public void UndoLastAction()
    {
        if (commandStack.Count == 0)
        {
            throw new Exception("No actions executed on this turn.");
        }

        if(!CanUndo())
        {
            throw new Exception("Last action is not undoable.");
        }

        ICommand action = commandStack.Pop();
        IUndoableCommand undoableAction = (IUndoableCommand)action;
        undoableAction.Undo();
        undoStack.Push(action);
    }

    public void RedoLastAction()
    {
        if (undoStack.Count == 0)
        {
            throw new Exception("No actions to redo.");
        }

        ICommand action = undoStack.Pop();
        action.Execute();
        commandStack.Push(action);
    }

    public void Clear()
    {
        commandStack.Clear();
        undoStack.Clear();
    }

    public bool CanUndo()
    {
        return commandStack.Count > 0 && commandStack.Peek().GetType().GetInterface(nameof(IUndoableCommand)) != null;
    }

    public bool CanRedo()
    {
        return undoStack.Count > 0;
    }
}
