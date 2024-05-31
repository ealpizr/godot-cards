using Godot;
using System;

public partial class UtilityCard : Card
{
	public new int DefensePoints { get; set; }

	public UtilityCard(string name, string description, Texture icon, int energyCost, int defensePoints, int healthPoints, CardRarity rarity)
	{
		Name = name;
		Description = description;
		Icon = icon;
		EnergyCost = energyCost;
		HealthPoints = healthPoints;
		Rarity = rarity;
		DefensePoints = defensePoints;
	}

}
