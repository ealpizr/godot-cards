using Godot;
using System;

public partial class InventoryCard : Control
{
	public void SetCard(godotcards.core.Api.Card card)
    {
        GetNode<TextureRect>("Background/Image").Texture = GD.Load<Texture2D>(card.Image);
        GetNode<Label>("Background/Name").Text = card.Name;
        GetNode<Label>("Background/Attack").Text = $"⚔ {card.Attack.ToString()}";
        GetNode<Label>("Background/Health").Text = $"❤ {card.Health.ToString()}";
        GetNode<Label>("Background/ManaCost").Text = card.ManaCost.ToString();
    }
}
