
using Godot;

public partial class NormalCard : CardDecorator
{
	public NormalCard(aCard card) : base(card)
	{
		card.RarityValue = 0;
	}

    public override void Init(aCard c, godotcards.core.Api.Card card)
	{
		c.Name = card.Name + "NOONO";
		c.Description = card.Description;
		c.Icon = GD.Load<Texture2D>(card.Image);
		c.EnergyCost = card.ManaCost;
		c.AttackPoints = card.Attack;
		c.DefensePoints = 0;
		c.HealthPoints = card.Health;

        Label label = new Label
        {
            Text = card.Rarity,
            Position = new Vector2(10, 10), // Adjust the position as needed
			Size = new Vector2(20, 20) // Adjust the size as needed
        };

		label.AddThemeFontSizeOverride("font_size", 10);
		c.GetNode("CardShape").AddChild(label);
		c.Rarity = CardRarity.Normal;

		this.card.eliminationPoints += 1;
    }

    public override void OnCardEliminated(PlayerBase player)
	{
		this.card.eliminationPoints = 2;
		//this.card.OnCardEliminated((PlayerBase)player);
	}
}
