using Godot;
using GodotCards.DesignPatterns.Command;
using GodotCards.DesignPatterns.Observer;

public enum GameMode
{
    Campaign,
    PvP
}

public partial class Game : Node, IGame
{
    private GameMode gameMode { get; set; }
    private TurnManager turnManager = new TurnManager();
    private readonly ActionManager actionManager = new ActionManager();

    PlayerBase player;
    PlayerBase opponent;
    GameField gameField;

    Label LevelLabel;
    int currentLevel;

    private PuntuacionFactory _puntuacionFactory = new PuntuacionJugadorFactory();

    private void SetupPlayers()
    {
        Hand playerHand = GetNode<Hand>("GameUI/PlayerHand");
        Hand playerPlayHand = playerHand;
        Dice playerDice = GetNode<Dice>("GameUI/PlayerDice");
        Deck playerDeck = GetNode<Deck>("GameUI/PlayerDeck");
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
        if(!IsPlayerTurn())
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

        SetupPlayers();
        SetupCampaign(1);
        return;

        //this.Start();

        //Array<Card> cards = new Array<Card>();
        //cards.Add(new Card());
        //this.campaign = new Campaign(Difficulty.Easy, player, opponent.Hand, opponent.PlayingFieldContainer, gameField, player.Deck, cards);
        //this.campaign.CPUPlayerPlay();

        //this.Update();

        //if (this.turnManager.IsPlayerTurn)
        //{
        //    this.campaign = new Campaign(Difficulty.Easy, player, opponent.Hand, opponent.PlayingFieldContainer, gameField, opponent.Deck, cards);
        //    this.campaign.AdvanceLevel();
        //}
        //else
        //{
        //    this.campaign.CPUPlayerPlay();
        //}

       
    }

    // Are we using this?
    private void _on_end_turn_pressed()
    {
        this.turnManager.EndTurn();
    }

    public void Start()
    {
        //this.player = new Player();
        //this.opponent = new CPUPlayer();
        // Refactorable.
        // This can be generalized to a way to use N amount of player,
        // here the game has a predefined amount of players and it's easy as assigning.  

        player.PlayingFieldContainer = GetNode("GameUI/CardDropArea").GetNode<HBoxContainer>("HBoxContainer");
        opponent.PlayingFieldContainer = GetNode("GameUI/CardDropArea").GetNode<HBoxContainer>("HBoxContainer");

        player.Hand = GetNode<Hand>("GameUI/Hand");
        opponent.Hand = GetNode<Hand>("GameUI/HandOther");

        player.Deck = GetNode<Deck>("GameUI/DeckPlayer");
        opponent.Deck = GetNode<Deck>("GameUI/DeckOtherPlayer");

        gameField = GetNode<GameField>("GameUI");
        LevelLabel = GetNode<Label>("GameUI/Level/Label");
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
