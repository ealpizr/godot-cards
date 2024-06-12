
using Godot;

public interface IInteractable
{
    public void Init(HBoxContainer container, Hand hand);
    public void ReceiveInteraction(PlayerBase interaction);
    public void SendInteraction(GameField gameField, PlayerBase interaction);
}