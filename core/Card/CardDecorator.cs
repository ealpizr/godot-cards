public partial class CardDecorator : aCard {
	public aCard card;
	public CardDecorator(aCard card) {
		this.card = card;
	}
	public override void OnCardEliminated(PlayerBase player) {
		card.OnCardEliminated(player);
	}

} 
