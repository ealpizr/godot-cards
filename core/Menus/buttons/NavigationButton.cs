using System;
using Godot;

public partial class NavigationButton : Button, INavigation
{
    private INavigation NextNavigation;
    private NavigationMenu parent;

    public NavigationButton(string name, string text, Vector2 size, INavigation nextNavigation, NavigationMenu parent)
    {
        this.Name = name;

        this.parent = parent;

        this.Text = text;

        this.CustomMinimumSize = size;

        this.NextNavigation = nextNavigation;

        this.Visible = false;
    }

    public NavigationButton(string text, INavigation nextNavigation, Button buttonClone)
    {
        this.Clone(buttonClone);

        this.Text = text;

        this.NextNavigation = nextNavigation;

        this.Visible = false;
    }

    public void OnClick()
    {
        GD.Print("NavigationButton Clicked");
        GD.Print(this.Size);
        this.parent.Close();
        this.NextNavigation.Open();
    }

    public void Open()
    {
        this.Visible = true;
    }

    public void Close()
    {
        this.Visible = false;
    }

    public void Init(BoxContainer boxContainer)
    {
        boxContainer.AddChild(this);
        this.Pressed += OnClick;
        this.Visible = false;
    }

    private void Clone(Button button)
    {
        this.Text = button.Text;
        this.Visible = button.Visible;
        this.Size = button.Size;
    }
}