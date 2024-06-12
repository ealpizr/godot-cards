using Godot;
using System;

public partial class Menu : Control
{
	public override void _Ready()
	{
		Button playButton = GetNode<Button>("Container/PlayButton");
		Button shopButton = GetNode<Button>("Container/ShopButton");
		Button exitButton = GetNode<Button>("Container/ExitButton");
		Label usernameLabel = GetNode<Label>("InfoBar/Username");
		Label versionLabel = GetNode<Label>("InfoBar/Version");

		usernameLabel.Text = GlobalState.Instance.Session.Username;
		versionLabel.Text = GlobalState.Instance.Version;

		playButton.Pressed += OnPlayButtonPressed;
		shopButton.Pressed += OnShopButtonPressed;
		exitButton.Pressed += OnExitButtonPressed;
	}

	public void OnPlayButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}

	public void OnShopButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/Shop.tscn");
	}

	public void OnExitButtonPressed()
	{
		GetTree().Quit();
	}
}
