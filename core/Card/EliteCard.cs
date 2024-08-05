public partial class EliteCard : CardDecorator
{
	public EliteCard(aCard card) : base(card)
	{
		card.RarityValue= 3;
	}

	public override void OnCardEliminated(PlayerBase player)
	{
		this.card.eliminationPoints = 3;
		this.card.OnCardEliminated(player);
	}
}
