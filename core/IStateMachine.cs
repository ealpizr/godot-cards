using Godot;

public interface IStateMachine {

    public void OnInput(InputEvent e);

    public void OnGuiInput(InputEvent e);

    public void OnMouseEnter();

    public void OnMouseExit();

    public void Init(Control control);

}