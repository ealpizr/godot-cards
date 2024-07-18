using Godot;
using System;

public partial class Menu : Control
{
	private MenuDirector menuDirector;
	public override void _Ready()
	{
		// Gets all buttons in the container so that they cannot interfere with the menu builder.
		// The reason there is one button is for reference for the client.
		for (int i = 0; i < GetNode<BoxContainer>("Container").GetChildren().Count; i++)
		{
			if (GetNode<BoxContainer>("Container").GetChild(i) is Button)
			{
				((Button)GetNode<BoxContainer>("Container").GetChild(i)).Visible = false;
			}
		}

		NavigationTypeMenuBuilder navigationTypeMenuBuilder = new NavigationTypeMenuBuilder();
		menuDirector = new MenuDirector(navigationTypeMenuBuilder);

		// Create the menu
		menuDirector.AddNavigateToMenuButton("Play", "Jugar", 0, 1);
		menuDirector.AddNavigateToSceneButton("Store", "Tienda", "res://scenes/example_shop.tscn", 0);
		menuDirector.AddExitButton("Exit", "Salir", 0);

		menuDirector.AddNavigateToSceneButton("PVP", "PVP", "res://scenes/game.tscn", 1);
		menuDirector.AddNavigateToSceneButton("Campaign", "Campaña", "res://scenes/game.tscn", 1);

		menuDirector.AddNavigateToMenuButton("BacktoMainMenu", "Atras", 1, 0);

		// builds the menu with the assembled parts.
		menuDirector.Construct();

		// gets the menu to initialize the final product through the gestor (Menu.cs)
		NavigationMenu menu = menuDirector.GetMenu() as NavigationMenu;
		menu.Init(GetNode<BoxContainer>("Container"));
		menu.Open();

		Label usernameLabel = GetNode<Label>("InfoBar/Username");
		Label versionLabel = GetNode<Label>("InfoBar/Version");

		usernameLabel.Text = GlobalState.Instance.Session.Username;
		versionLabel.Text = GlobalState.Instance.Version;
	}
}
