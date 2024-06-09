using Godot;
using System;

public partial class Coins : Control
{
	public ColorRect ColorRect;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Label user = GetNode<Label>("VBoxContainer/ColorRect/Username");
		Label coins = GetNode<Label>("VBoxContainer/ColorRect/Coins");

		user.Text += GlobalState.Instance.Session.Username;
		coins.Text += GlobalState.Instance.Coins;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
