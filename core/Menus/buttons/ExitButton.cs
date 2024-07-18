using Godot;

public partial class ExitButton : Button, INavigation
{

    public ExitButton(string name, string text, Vector2 size)
    {
        this.Name = name;

        this.Text = text;

        this.CustomMinimumSize = size;

        this.Pressed += OnClick;

        this.Visible = false;
    }
    public void OnClick()
    {
        GetTree().Quit();
    }

    public void Open()
    {
        this.Visible = true;
    }

    public void Init(BoxContainer boxContainer)
    {
        boxContainer.AddChild(this);
        this.Visible = false;
    }

    public void Close()
    {
        this.Visible = false;
    }
}