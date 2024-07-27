using Godot;

public abstract partial class aCard : Control {
	// for these cards, I need to create th e
	public bool isAttackPosition = true;

	// New properties for the card
	public new string Name { get; set; }
	public string Description { get; set; }
	public Texture Icon { get; set; }
	public int EnergyCost { get; set; }
	public int AttackPoints { get; set; }
	public int DefensePoints { get; set; }
	public int HealthPoints { get; set; }
	public CardRarity Rarity { get; set; }

	public bool IsSelected { get; set; }

	public int eliminationPoints { get; set; }
	
	public int Puntos { get; set; }

	public abstract void OnCardEliminated(PlayerBase player);
} 
