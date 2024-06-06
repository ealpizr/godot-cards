using Godot;

public interface IGameField {
    public void PlaceCard(Card card, HBoxContainer container);
    public void RemoveCard(Card card, HBoxContainer container);
}