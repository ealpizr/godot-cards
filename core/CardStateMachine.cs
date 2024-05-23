using System;
using Godot;
using Godot.Collections;

public partial class CardStateMachine : Node, IStateMachine
{
    [Export]
    private CardStateBase initialState;

    private IStateBase<CardState> currentState;
    private Dictionary<CardState, CardStateBase> states = new Dictionary<CardState, CardStateBase>();

    public void Init(Control card)
    {
        foreach (Node child in GetChildren())
        {
            if (child is CardStateBase)
            {
                CardStateBase s = (CardStateBase)child;
                states[(CardState)s.GetState()] = s;
                s.SetTransitionRequest(OnTransitionRequested);
                s.Card = (Card)card;
            }
        }

        if (initialState != null)
        {
            initialState.Enter();
            currentState = initialState;
        }
    }

    private void OnTransitionRequested(IStateBase<CardState> from, CardState to)
    {
        if (from != currentState)
        {
            GD.PrintErr("Card state mismatch. Something really bad happened.");
            return;
        }

        CardStateBase newState = states[to];
        if (newState == null)
        {
            GD.PrintErr($"Invalid card state requested. Got '{to}'.");
            return;
        }

        if (currentState != null)
        {
            currentState.Exit();
        }

        // after the state is transitioned, trigger the Enter method from that given state.
        newState.Enter();
        currentState = newState;
    }

    public void OnInput(InputEvent e)
    {
        if (currentState != null)
        {
            currentState.OnInput(e);
        }
    }

    public void OnGuiInput(InputEvent e)
    {
        if (currentState != null)
        {
            currentState.OnGuiInput(e);
        }
    }

    public void OnMouseEnter()
    {
        if (currentState != null)
        {
            currentState.OnMouseEnter();
        }
    }

    public void OnMouseExit()
    {
        if (currentState != null)
        {
            currentState.OnMouseExit();
        }
    }


}
