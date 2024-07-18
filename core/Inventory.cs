using Godot;
using System;

public partial class Inventory : Control
{
	public override void _Ready()
	{
        RenderSidebarMenu();
    }

	private void RenderSidebarMenu()
	{
        // We should probably just use a single button and register the click event to change the scene.
        // Lets use the menu builder for now.

        NavigationTypeMenuBuilder navigationTypeMenuBuilder = new NavigationTypeMenuBuilder();
        MenuDirector menuDirector = new MenuDirector(navigationTypeMenuBuilder);

        menuDirector.AddNavigateToSceneButton("Home", "Regresar", "res://scenes/menu.tscn", 0);

        menuDirector.Construct();
        INavigation navigation = menuDirector.GetMenu();
        navigation.Init(GetNode<VBoxContainer>("Sidebar/Content/StatsContainer/VBoxContainer/ButtonsContainer"));
        navigation.Open();
    }
}
