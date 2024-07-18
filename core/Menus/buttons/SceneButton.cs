using Godot;

public partial class SceneButton : Button, INavigation
{
    private string ScenePath;

    public SceneButton(string name, string text, Vector2 size, string scenePath)
    {
        this.Name = name;

        this.ScenePath = scenePath;

        this.Text = text;

        this.CustomMinimumSize = size;

        this.Pressed += OnClick;

        this.Visible = false;
    }
    public void OnClick()
    {
        GetTree().ChangeSceneToFile(this.ScenePath);
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