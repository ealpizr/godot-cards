
using Godot;
using Godot.Collections;

public interface IInteractable
{
    public void Init(HBoxContainer container, Hand hand, Deck deck, Array<Card> cards);
    public void ReceiveInteraction(PlayerBase interaction);
    public void SendInteraction(GameField gameField, PlayerBase interaction);
}