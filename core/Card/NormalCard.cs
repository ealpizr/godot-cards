public partial class NormalCard : CardDecorator
{
	public NormalCard(aCard card) : base(card)
	{
		card.RarityValue = 0;
	}

	public override void OnCardEliminated(PlayerBase player)
	{
		this.card.eliminationPoints = 2;
		this.card.OnCardEliminated(player);
	}
}
