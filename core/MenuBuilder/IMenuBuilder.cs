using System;
using Godot;
public interface IMenuBuilder
{
	void AddNavigateToSceneButton(string name, string text, string ScenePath,  int depthId = 0);

	void AddNavigateToMenuButton(string name, string text, int fromDepthId,  int toDepthId = 0);

	void AddExitButton(string name, string text, int depthId = 0);

	INavigation BuildMenu();
}
