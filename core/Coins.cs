using Godot;
using System;

public partial class Coins : Control
{
	public ColorRect ColorRect;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Label user = GetNode<Label>("VBoxContainer/ColorRect/Username");
		user.Text = GlobalState.Instance.Username;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
