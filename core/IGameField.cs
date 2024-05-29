using Godot;

public interface IPlayField {
    public void PlaceCard(Card card, HBoxContainer container);
    public void RemoveCard(Card card, HBoxContainer container);
}