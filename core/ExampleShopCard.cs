using Godot;
using System;

public partial class ExampleShopCard : Control
{
	public void SetCardData(int id, string name, int cost)
    {
        Label idLabel = GetNode<Label>("Container/ID");
        Label nameLabel = GetNode<Label>("Container/Name");
        Label costLabel = GetNode<Label>("Container/Cost");

        idLabel.Text = id.ToString();
        nameLabel.Text = name;
        costLabel.Text = cost.ToString();
    }
}
