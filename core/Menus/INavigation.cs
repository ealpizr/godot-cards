using Godot;

public interface INavigation
{
    void Init(BoxContainer container);
    void Open();

    void Close();
}