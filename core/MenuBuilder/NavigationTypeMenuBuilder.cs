using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using Godot.Collections;
using GodotPlugins.Game;

public class NavigationTypeMenuBuilder : IMenuBuilder
{
    NavigationMenu MainMenu;

    Vector2 Size;

    public NavigationTypeMenuBuilder()
    {
        this.Size = new Vector2(300, 50);
        this.MainMenu = new NavigationMenu();
    }
    public void AddNavigateToSceneButton(string name, string text, string ScenePath, int depthId = 0)
    {
        NavigationMenu currentNavigationMenu = GetCurrentNavigationMenu(depthId);

        SceneButton sceneButton = new SceneButton(name, text, Size, ScenePath);

        currentNavigationMenu.AddNavigation(sceneButton);
    }

    public void AddNavigateToMenuButton(string name, string text, int fromDepthId,  int toDepthId)
    {
        NavigationMenu currentNavigationMenu = GetCurrentNavigationMenu(fromDepthId);

        NavigationMenu toNavigationMenu = GetCurrentNavigationMenu(toDepthId);

        if (toNavigationMenu.DepthId != toDepthId)
        {
            toNavigationMenu = new NavigationMenu
            {
                DepthId = toDepthId
            };
        }

        NavigationButton navigationButton = new NavigationButton(name, text, Size, toNavigationMenu, currentNavigationMenu)
        {
            Name = name
        };

        // I hope this is not too confusing, but this is how I control the order the navigations are displayed. 
        currentNavigationMenu.AddNavigation(navigationButton);

        currentNavigationMenu.AddNavigation(toNavigationMenu);
    }

    public void AddExitButton(string name, string text, int depthId = 0)
    {
        NavigationMenu currentNavigationMenu = GetCurrentNavigationMenu(depthId);

        ExitButton exitButton = new ExitButton(name, text, Size);

        currentNavigationMenu.AddNavigation(exitButton);
    }

    public INavigation BuildMenu()
    {
        return MainMenu;
    }

    private NavigationMenu GetCurrentNavigationMenu(int depthId)
    {
        NavigationMenu currentNavigationMenu = MainMenu;
        
        while (depthId > 0)
        {
            List<INavigation> childrens = currentNavigationMenu.GetChildren();
            
            foreach (INavigation child in childrens)
            {
                if (child is NavigationMenu)
                {
                    currentNavigationMenu = (NavigationMenu)child;
                    break;
                }
            }

            depthId--;
        }

        return currentNavigationMenu;
    }
}