
using Godot;

public abstract partial class CardDecorator : aCard {
	public aCard card;
	public CardDecorator(aCard card) {
		this.card = card;
	}

    public abstract override void Init(aCard c, godotcards.core.Api.Card card);

    public override void OnCardEliminated(PlayerBase player) {
		card.OnCardEliminated(player);
	}
} 
