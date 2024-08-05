using Godot;
using Godot.Collections;

public partial class Hand : HBoxContainer
{
	public Array<Card> Cards = new Array<Card>();
	public bool HandStatus { get; set; }
	public override void _Ready()
	{
		//GetTree().Get
		foreach (Node child in GetChildren())
		{
			if (child is Card card)
			{
				Cards.Add(card);
				card.ReparentRequested += OnReparentRequested;
			}
		}
	}

	private void RemoveAllCards()
    {
        foreach (Node child in GetChildren())
        {
            if (child is Card card)
            {
                RemoveChild(card);
            }
        }
    }

	public void LoadInitialCards(Array<Card> cardsList)
    {
		RemoveAllCards();

        foreach (Card card in cardsList)
        {			
            AddChild(card);
        }
    }

	public void AddCard(Card card)
    {
		Cards.Add(card);
        AddChild(card);
    }

	public void RemoveCard(Card card)
    {
        card.Dispose();
    }
	
	private void OnReparentRequested(Card card)
	{
		card.Reparent(this);
	}
}
