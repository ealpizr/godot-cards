using System;
using Godot;

public class State {

}

// T: Enum type, this is the state enum that we can use to generate different kinds of states based on the requirement.
public interface IStateBase<T> where T : Enum {

    public Enum GetState();

    public void SetTransitionRequest(Action<IStateBase<T>, T> action);

    // Called when entering a new state
    public void Enter();

    // Called when exiting a state
    public void Exit();
    // Callbacks
    public void OnInput(InputEvent e);

    public void OnGuiInput(InputEvent e);

    public void OnMouseEnter();

    public void OnMouseExit();
    
 }