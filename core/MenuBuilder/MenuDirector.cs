public class MenuDirector 
{
	private IMenuBuilder _menuBuilder;

	private INavigation _menuBase;

	public MenuDirector(IMenuBuilder menuBuilder)
	{
		_menuBuilder = menuBuilder;
	}

	public void AddNavigateToSceneButton(string name, string action, string scenePath,  int depthId = 0)
	{
		_menuBuilder.AddNavigateToSceneButton(name, action, scenePath, depthId);
	}

	public void AddNavigateToMenuButton(string name, string text, int fromDepthId,  int toDepthId = 0)
	{
		_menuBuilder.AddNavigateToMenuButton(name, text, fromDepthId, toDepthId);
	}

	public void AddExitButton(string name, string text, int depthId = 0)
	{
		_menuBuilder.AddExitButton(name, text, depthId);
	}
	public void Construct()
	{
		this._menuBase = _menuBuilder.BuildMenu();
	}

	public INavigation GetMenu()
	{
		return this._menuBase;
	}
}
