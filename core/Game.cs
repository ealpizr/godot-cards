using Godot;
using Godot.Collections;

public partial class Game : Node, IGame
{
    private TurnManager turnManager;

	PlayerBase player;
	PlayerBase otherPlayer;

	Campaign campaign;

	GameField gameField;

	Label LevelLabel;
	int currentLevel;

	private PuntuacionFactory _puntuacionFactory = new PuntuacionJugadorFactory();

	public override void _Ready()
	{
		this.Start();

		Array<Card> cards = new Array<Card>();
		cards.Add(new Card());
		this.campaign = new Campaign(Difficulty.Easy, player, otherPlayer.Hand, otherPlayer.PlayingFieldContainer, gameField, player.deck, cards);        
		this.campaign.CPUPlayerPlay();

		this.Update();

		if (this.turnManager.IsPlayerTurn) {
			this.campaign = new Campaign(Difficulty.Easy, player, otherPlayer.Hand, otherPlayer.PlayingFieldContainer, gameField, otherPlayer.deck, cards);        
			this.campaign.AdvanceLevel();
		} else {
			this.campaign.CPUPlayerPlay();
		}

		turnManager = new TurnManager(player, otherPlayer);
		turnManager.Start();
	}

	// Are we using this?
    private void _on_end_turn_pressed()
    {
        this.turnManager.EndTurn();
    }

    public void Start()
	{
		this.player = new Player();
		this.otherPlayer = new CPUPlayer();
		// Refactorable.
		// This can be generalized to a way to use N amount of player,
		// here the game has a predefined amount of players and it's easy as assigning.  

		player.PlayingFieldContainer = GetNode("GameUI/CardDropArea").GetNode<HBoxContainer>("HBoxContainer");
		otherPlayer.PlayingFieldContainer = GetNode("GameUI/CardDropArea").GetNode<HBoxContainer>("HBoxContainer");
		
		player.Hand = GetNode<Hand>("GameUI/Hand");
		otherPlayer.Hand = GetNode<Hand>("GameUI/HandOther");

		player.deck = GetNode<Deck>("GameUI/DeckPlayer");
		otherPlayer.deck = GetNode<Deck>("GameUI/DeckOtherPlayer");

		gameField = GetNode<GameField>("GameUI");
		LevelLabel = GetNode<Label>("GameUI/Level/Label");
	}

	public void Update()
	{
		this.currentLevel = this.campaign.CurrentLevel;

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
