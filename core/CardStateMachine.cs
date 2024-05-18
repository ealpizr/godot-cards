using Godot;
using Godot.Collections;

public partial class CardStateMachine : Node
{
    [Export]
    private CardStateBase initialState;

    private CardStateBase currentState;
    private Dictionary<CardState, CardStateBase> states = new Dictionary<CardState, CardStateBase>();

    public void Init(Card card)
    {
        foreach (Node child in GetChildren())
        {
            if (child is CardStateBase)
            {
                CardStateBase s = (CardStateBase)child;
                states[s.State] = s;
                s.TransitionRequested += OnTransitionRequested;
                s.Card = card;
            }
        }

        if (initialState != null)
        {
            initialState.Enter();
            currentState = initialState;
        }
    }

    private void OnTransitionRequested(CardStateBase from, CardState to)
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
