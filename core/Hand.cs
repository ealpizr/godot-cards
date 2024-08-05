using Godot;
using Godot.Collections;

public partial class Hand : HBoxContainer
{
	public Array<aCard> Cards = new Array<aCard>();
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

	public void AddCard(aCard card)
	{
		Cards.Add(card);
		card.ReparentRequested += OnReparentRequested;
		AddChild(card);
	}
	private void RemoveAllCards()
    {
        foreach (Node child in GetChildren())
        {
            if (child is aCard card)
            {
                RemoveChild(card);
            }
        }
    }

	public void LoadInitialCards(Array<aCard> cardsList)
    {
		RemoveAllCards();

        foreach (aCard card in cardsList)
        {			
            AddChild(card);
        }
    }

	public void RemoveCard(aCard card)
    {
        card.Dispose();
    }
	
	private void OnReparentRequested(aCard card)
	{
		card.Reparent(this);
	}
}
