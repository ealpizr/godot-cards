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
		Button CampaignButton = GetNode<Button>("Container/CampaignButton");
		Button PVPButton = GetNode<Button>("Container/PVPButton");
		Button BackButton = GetNode<Button>("Container/BackButton");

		usernameLabel.Text = GlobalState.Instance.Session.Username;
		versionLabel.Text = GlobalState.Instance.Version;

		playButton.Pressed += OnPlayButtonPressed;
		shopButton.Pressed += OnShopButtonPressed;
		exitButton.Pressed += OnExitButtonPressed;
		CampaignButton.Pressed += OnCampaignButtonPressed;
		PVPButton.Pressed += OnPVPButtonPressed;
		BackButton.Pressed += OnBackButtonPressed;
	}

    private void OnBackButtonPressed()
    {
		Button playButton = GetNode<Button>("Container/PlayButton");
		Button shopButton = GetNode<Button>("Container/ShopButton");
        Button CampaignButton = GetNode<Button>("Container/CampaignButton");
		Button PVPButton = GetNode<Button>("Container/PVPButton");
		Button BackButton = GetNode<Button>("Container/BackButton");
		CampaignButton.Visible = false;
		PVPButton.Visible = false;
		BackButton.Visible = false;

		playButton.Visible = true;
		shopButton.Visible = true;
    }


    private void OnPVPButtonPressed()
    {
        //GetTree().ChangeSceneToFile("res://scenes/PVP.tscn");
    }

    public void OnPlayButtonPressed()
	{
		Button playButton = GetNode<Button>("Container/PlayButton");
		Button shopButton = GetNode<Button>("Container/ShopButton");
        Button CampaignButton = GetNode<Button>("Container/CampaignButton");
		Button PVPButton = GetNode<Button>("Container/PVPButton");
		Button BackButton = GetNode<Button>("Container/BackButton");
		CampaignButton.Visible = true;
		PVPButton.Visible = true;
		BackButton.Visible = true;

		playButton.Visible = false;
		shopButton.Visible = false;
	}

	public void OnShopButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/Shop.tscn");
	}

	public void OnExitButtonPressed()
	{
		GetTree().Quit();
	}

	public void OnCampaignButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}
}
