using Godot;
using Nakama;
using System;
using System.Text.Json;
using System.Threading.Tasks;

public partial class Dice : Control
{
    [Signal]
    public delegate void ClickedEventHandler();

    private int _dice1;
    private int _dice2;
    private int _min = 1;
    private int _max = 6;

    public override void _Ready()
    {
        LoadPlayerDice();
        GetNode<Button>("Button").Pressed += OnClicked;
    }

    private void OnClicked()
    {
        EmitSignal(SignalName.Clicked);
    }

    private async void LoadPlayerDice()
    {
        Nakama.Client _client = GlobalState.Instance.NakamaClient;
        Nakama.ISession _session = GlobalState.Instance.Session;

        IApiRpc rpcReponse = await _client.RpcAsync(_session, "GetUserDice");
        godotcards.core.Api.Dice dice = JsonSerializer.Deserialize<godotcards.core.Api.Dice>(rpcReponse.Payload);

        _min = dice.Min;
        _max = dice.Max;
    }

    private void UpdateDiceValues()
    {
        GetNode<Label>("Container/FirstDice/Value").Text = _dice1.ToString();
        GetNode<Label>("Container/SecondDice/Value").Text = _dice2.ToString();
    }

    public async Task Roll()
    {
        Random random = new Random();

        for (int i = 0; i < 10; i++)
        {
            _dice1 = random.Next(1, 7);
            _dice2 = random.Next(1, 7);
            UpdateDiceValues();
            await ToSignal(GetTree().CreateTimer(0.3f), "timeout");
        }
    }

    public int GetSum()
    {
        return _dice1 + _dice2;
    }
}
