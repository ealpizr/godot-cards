using System;
using Godot;

public abstract partial class aCard : Control {
		[Signal]
	public delegate void ReparentRequestedEventHandler(Card card);
	public bool isAttackPosition = true;

	// New properties for the card
	public new string Name { get; set; }
	public string Description { get; set; }
	public Texture2D Icon { get; set; }
	public int EnergyCost { get; set; }
	public int AttackPoints { get; set; }
	public int DefensePoints { get; set; }
	public int HealthPoints { get; set; }
	
	public Label RarityLabel { get; set; }

	
	public CardRarity Rarity { get; set; }

	public bool IsSelected { get; set; }

	public bool IsAttacking { get; set; }

	public int eliminationPoints { get; set; }
	
	public int Puntos { get; set; }

	public int RarityValue { get; set; }

	public abstract void Init(aCard c, godotcards.core.Api.Card card);

	public abstract void OnCardEliminated(PlayerBase player);
} 

// Enum to define the rarity of the card
public enum CardRarity
{
	Common,
	Normal,
	Elite,
	Legendary
}