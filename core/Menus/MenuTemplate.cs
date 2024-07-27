using Godot;

// Possible to use this class to replace the menu logic in the NavigationMenu class
public abstract class MenuTemplate
{
	public abstract void Init(BoxContainer container);

	public abstract void Open();

	public abstract void Close();
}
