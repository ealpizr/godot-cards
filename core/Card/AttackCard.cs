using Godot;
using System;

public partial class AttackCard : Card
{
	public new int AttackPoints { get; set; }

	public AttackCard(string name, string description, Texture icon, int energyCost, int attackPoints, int healthPoints, CardRarity rarity)
	{
		Name = name;
		Description = description;
		Icon = icon;
		EnergyCost = energyCost;
		HealthPoints = healthPoints;
		Rarity = rarity;
		AttackPoints = attackPoints;
	}
}
