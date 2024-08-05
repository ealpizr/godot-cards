using System.Collections.Generic;
using Godot;
using Godot.Collections;
using GodotCards.DesignPatterns.Command;
using GodotCards.DesignPatterns.Observer;
using Nakama;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

public enum GameMode
{
    Campaign,
    PvP
}

public partial class Game : Node
{
    private const int INITIAL_HAND_CARDS = 3;
    private GameMode gameMode { get; set; }
    private TurnManager turnManager = new TurnManager();
    private readonly ActionManager actionManager = new ActionManager();

    PlayerBase player;
    PlayerBase opponent;
    GameField gameField;

    Label LevelLabel;
    int currentLevel;

    private PuntuacionFactory _puntuacionFactory = new PuntuacionJugadorFactory();
    public GodotCards.DesignPatterns.Command.ICommand _attackCommand;

    private aCard BuildGameCard(godotcards.core.Api.Card card)
    {
        // This is probably very bad. We creating a bunch of scenes here.
        // Does this allocate a bunch of memory we're not using? No time to check.
        aCard c = (aCard)GD.Load<PackedScene>("res://scenes/card.tscn").Instantiate();

        aCard cardInitalizer = new Card();
        if (card.Rarity != "Común")
        {
            switch (card.Rarity)
            {
                case "Normal":
                    cardInitalizer = new NormalCard(c);
                    break;
                case "Élite":
                    cardInitalizer = new EliteCard(c);
                    break;
                case "Legendaria":
                    cardInitalizer = new LegendariaCard(c);
                    break;
            }
        }


        cardInitalizer.Init(c, card);

        return c;
    }

    private async Task<(Array<aCard>, Array<aCard>)> GetGameOpponentCards()
    {
        IApiRpc rpcReponse = await GlobalState.Instance.NakamaClient.RpcAsync(GlobalState.Instance.Session, "GetAllCards");
        godotcards.core.Api.Card[] userDeck = JsonSerializer.Deserialize<godotcards.core.Api.Card[]>(rpcReponse.Payload);
        List<aCard> deck = new List<aCard>();
        List<aCard> hand = new List<aCard>();

        while (deck.Count < 60)
        {
            int randomIndex = new Random().Next(0, userDeck.Length);
            if (deck.Contains(BuildGameCard(userDeck[randomIndex])))
            {
                continue;
            }
            deck.Add(BuildGameCard(userDeck[randomIndex]));
        }

        for (int i = 0; i < INITIAL_HAND_CARDS; i++)
        {
            int randomIndex = new Random().Next(0, deck.Count);
            hand.Add(deck[randomIndex]);
            deck.RemoveAt(randomIndex);
        }

        return (new Array<aCard>(deck), new Array<aCard>(hand));
    }

    private async Task<(Array<aCard>, Array<aCard>)> GetGamePlayerCards()
    {
        IApiRpc rpcReponse = await GlobalState.Instance.NakamaClient.RpcAsync(GlobalState.Instance.Session, "GetUserDeck");
        godotcards.core.Api.Card[] userDeck = JsonSerializer.Deserialize<godotcards.core.Api.Card[]>(rpcReponse.Payload);
        List<aCard> deck = new List<aCard>();
        List<aCard> hand = new List<aCard>();

        // I don't like this mapping but it's the fastest way to do it.
        foreach (godotcards.core.Api.Card card in userDeck)
        {
            deck.Add(BuildGameCard(card));
        }

        for (int i = 0; i < INITIAL_HAND_CARDS; i++)
        {
            int randomIndex = new Random().Next(0, deck.Count);
            hand.Add(deck[randomIndex]);
            deck.RemoveAt(randomIndex);
        }

        return (new Array<aCard>(deck), new Array<aCard>(hand));
    }

    private async void SetupPlayers()
    {
        Hand playerHand = GetNode<Hand>("GameUI/PlayerHand");
        Hand playerPlayHand = playerHand;
        Dice playerDice = GetNode<Dice>("GameUI/PlayerDice");
        Deck playerDeck = GetNode<Deck>("GameUI/PlayerDeck");
        playerDeck.Click += PlayerDeckClicked;
        EnergyBar playerEnergyBar = GetNode<EnergyBar>("GameUI/PlayerEnergyBar");
        TurnDelegate playerTurnDelegate = (currentTurn) => currentTurn == Turn.Player;
        player = new Player(playerHand, playerPlayHand, playerDeck, playerDice, playerEnergyBar, playerTurnDelegate);

        Hand opponentHand = GetNode<Hand>("GameUI/OpponentHand");
        Hand opponentPlayHand = opponentHand;
        Dice opponentDice = GetNode<Dice>("GameUI/OpponentDice");
        Deck opponentDeck = GetNode<Deck>("GameUI/OpponentDeck");
        EnergyBar opponentEnergyBar = GetNode<EnergyBar>("GameUI/OpponentEnergyBar");
        TurnDelegate opponentTurnDelegate = (currentTurn) => currentTurn == Turn.Opponent;
        opponent = new Player(opponentHand, opponentPlayHand, opponentDeck, opponentDice, opponentEnergyBar, opponentTurnDelegate);

        (Array<aCard> deck, Array<aCard> hand) = await GetGamePlayerCards();
        playerHand.LoadInitialCards(hand);
        playerDeck.LoadCardsToDeck(deck);

        (Array<aCard> opponentDeckCards, Array<aCard> opponentHandCards) = await GetGameOpponentCards();
        opponentHand.LoadInitialCards(opponentHandCards);
        opponentDeck.LoadCardsToDeck(opponentDeckCards);
	}

    private void SetupAndStartTurnManager(Turn firstTurn)
    {
        turnManager.Subscribe(this.player.TurnObserver);
        turnManager.Subscribe(this.opponent.TurnObserver);
        turnManager.Start(firstTurn);
    }

    public void SetupCampaign(int level)
    {
        this.gameMode = GameMode.Campaign;
        this.SetupPlayers();
        Campaign campaign = new Campaign(level, this.opponent, this.turnManager, this.actionManager);
        this.opponent = campaign.GetCPUPlayer();

        SetupAndStartTurnManager(Turn.Opponent);
    }

    private bool IsPlayerTurn()
    {
        return this.turnManager.IsPlayerTurn;
    }

    private void PlayerDeckClicked()
    {
        if (!IsPlayerTurn())
        {
            return;
        }

        ICommand drawCardFromDeckCommand = new DrawCardFromDeckCommand(this.player);
        actionManager.ExecuteAction(drawCardFromDeckCommand);
    }

    private void PlayerDiceClicked()
    {
        if (!IsPlayerTurn())
        {
            return;
        }

        ICommand rollDiceCommand = new RollDiceCommand(this.player);
        actionManager.ExecuteAction(rollDiceCommand);
    }

    private void EndTurnPressed()
    {
        if (!IsPlayerTurn())
        {
            return;
        }

        ICommand endTurnCommand = new EndTurnCommand(this.turnManager);
        actionManager.ExecuteAction(endTurnCommand);
    }



    public override void _Ready()
    {
        GetNode<Dice>("GameUI/PlayerDice").Clicked += PlayerDiceClicked;
        GetNode<Button>("MarginContainer/VBoxContainer/EndTurn").Pressed += EndTurnPressed;

        SetupCampaign(1);
        return;
    }

    // Are we using this?
    private void _on_end_turn_pressed()
    {
        this.turnManager.EndTurn();
    }

    private void _on_attack_button_pressed()
    {
        _attackCommand = new AttackCommand(player, opponent);
        _attackCommand.Execute();
    }
    public void Update()
    {
        this.LevelLabel.Text = "Level: " + this.currentLevel;
    }

    public void CalcularPuntuacionFinal(Player player)
    {
        IPuntuacion puntuacion = _puntuacionFactory.CrearPuntuacion(player);
        GD.Print($"Puntuación Final: {puntuacion.TotalPuntos}");

        int winnerCCoins = puntuacion.GetCCoinsForWinner();
        int loserCCoins = puntuacion.GetCCoinsForLoser();
        int objectivesCCoins = puntuacion.GetCCoinsForObjectives();

        GD.Print($"C-Coins para el ganador: {winnerCCoins}");
        GD.Print($"C-Coins para el perdedor: {loserCCoins}");
        GD.Print($"C-Coins adicionales por objetivos: {objectivesCCoins}");
    }
}
