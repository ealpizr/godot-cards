using Godot;

namespace GodotCards.DesignPatterns.Command;

public class DrawCardFromDeckCommand : IUndoableCommand
{
    private const int MAX_CARDS_IN_HAND = 5;
    private readonly PlayerBase _player;
    private aCard _card;

    public DrawCardFromDeckCommand(PlayerBase player)
    {
        _player = player;
    }

    public void Execute()
    {
        if(_player.Deck.Cards.Count == 0)
        {
            GD.Print("No cards in deck");
            return;
        }

        if(_player.Hand.Cards.Count >= MAX_CARDS_IN_HAND)
        {
            GD.Print("Hand is full");
            return;
        }

        _card = _player.Deck.Cards.Pop();
        _player.Deck.RenderDeck();
        _player.Hand.AddCard(_card);
    }

    public void Undo()
    {
        if(_card == null)
        {
            return;
        }

        _player.Hand.RemoveCard(_card);
        _player.Deck.Cards.Push(_card);
        _player.Deck.RenderDeck();
    }
}
