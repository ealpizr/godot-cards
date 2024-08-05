public partial class LegendariaCard : CardDecorator
{
	public LegendariaCard(aCard card) : base(card)
	{
		card.RarityValue = 2;
	}

	public override void OnCardEliminated(PlayerBase player)
	{
		this.card.eliminationPoints = 5;
		this.card.OnCardEliminated(player);
	}
}
