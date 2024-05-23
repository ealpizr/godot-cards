using System;
using Godot;

public enum CardState
{
    Idle,
    Clicked,
    Dragging,
    Released
}

public partial class CardStateBase : Node, IStateBase<CardState>
{
    [Signal]
    public delegate void TransitionRequestedEventHandler(CardStateBase from, CardState to);

    // We can use the Export attribute to expose a property to the Godot editor
    [Export]
    public CardState State;

    // Reference to that Card node
    public Card Card;

    // Called when entering a new state
    public Enum GetState()
    {
        return State;
    }


    public void SetTransitionRequest(Action<IStateBase<CardState>, CardState> action)
    {
        TransitionRequested += (from, to) => action(from, to);
    }

    public virtual void Enter()
    {

    }


    // Called when exiting a state
    public virtual void Exit()
    {

    }

    // Callbacks
    public virtual void OnInput(InputEvent e)
    {

    }

    public virtual void OnGuiInput(InputEvent e)
    {

    }

    public virtual void OnMouseEnter()
    {

    }

    public virtual void OnMouseExit()
    {

    }
}