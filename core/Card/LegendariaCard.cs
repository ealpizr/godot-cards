public partial class LegendariaCard : CardDecorator
{
	public LegendariaCard(aCard card) : base(card)
	{
	}

	public override void OnCardEliminated(PlayerBase player)
	{
		this.card.eliminationPoints = 5;
		this.card.OnCardEliminated(player);
	}
}
