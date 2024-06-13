using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CardData
{
    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("cost")]
    public int Cost { get; set; }
}

public partial class ExampleShop : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        LoadAvailableCards();
    }

    private async void LoadAvailableCards()
    {
        Nakama.Client client = GlobalState.Instance.NakamaClient;
        Nakama.ISession session = GlobalState.Instance.Session;

        Nakama.IApiRpc response = await client.RpcAsync(session, "GetAvailableCards");
        List<CardData> cards = JsonSerializer.Deserialize<List<CardData>>(response.Payload);

        GridContainer cardContainer = GetNode<GridContainer>("Container");

        foreach (CardData card in cards)
        {
            Node cardNode = GD.Load<PackedScene>("res://scenes/Shop_card.tscn").Instantiate();
            ((ShopCard)cardNode).SetCardData(card.ID, card.Name, card.Cost);

            cardContainer.AddChild(cardNode);
        }
    }
}