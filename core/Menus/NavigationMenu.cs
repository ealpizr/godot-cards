using System.Collections.Generic;
using Godot;

public class NavigationMenu : INavigation
{
	public int DepthId { get; set; }
	private List<INavigation> navigations = new List<INavigation>();
	public void AddNavigation(INavigation navigation)
	{
		this.navigations.Add(navigation);
	}

	public void Remove(int index)
	{
		this.navigations.RemoveAt(index);
	}

	public INavigation Get(int index)
	{
		return this.navigations[index];
	}

	public List<INavigation> GetChildren()
	{
		return this.navigations;
	}

	public void Update(INavigation navigation, int index)
	{
		this.navigations[index] = navigation;
	}

	public void Open()
	{
		for (int i = 0; i < navigations.Count; i++)
		{
			if (navigations[i] is Button) navigations[i].Open();
		}
	}

	public void Close()
	{
		for (int i = 0; i < navigations.Count; i++)
		{
			if (navigations[i] is Button) navigations[i].Close();
		}
	}

	public void Init(BoxContainer boxContainer)
	{
		for (int i = 0; i < navigations.Count; i++)
		{
			if (navigations[i] is NavigationMenu)
			{
				if (((NavigationMenu)navigations[i]).DepthId < this.DepthId)
				{
					return;
				}
			}
			navigations[i].Init(boxContainer);
		}
	}
}
