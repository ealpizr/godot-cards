using Godot;

public interface IControllable
{
    public void OnInput(InputEvent e);

    public void OnGuiInput(InputEvent e);

    public void OnMouseEnter();

    public void OnMouseExit();
}