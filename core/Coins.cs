using Godot;
using System;

public partial class Coins : Control
{
	private Label userLabel;
	private Label coinsLabel;

	public override void _Ready()
	{
		userLabel = GetNode<Label>("VBoxContainer/ColorRect/Username");
		coinsLabel = GetNode<Label>("VBoxContainer/ColorRect/Coins");

		UpdateUI();
	}

	public void UpdateUI(int newCoins)
	{
		coinsLabel.Text = $"C-Coins: {newCoins}";
	}

	private void UpdateUI()
	{
		userLabel.Text = $"User: {GlobalState.Instance.Session.Username}";
		coinsLabel.Text = $"C-Coins: {GlobalState.Instance.Coins}";
	}
}
