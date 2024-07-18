using Godot;
// placeholder for the game logic that can needs
// to be implement later by the other side of the
// team related to game logic.
public partial class Game : Node, IGame
{
	PlayerBase player;
	PlayerBase otherPlayer;

	Campaign campaign;

	GameField gameField;
	TurnManager turnManager;
	Timer timer = new Timer();

    Label LevelLabel;
	int currentLevel;

	public override void _Ready()
	{
		this.Start();

		this.Campaign = new Campaign(Difficulty.Easy, Player, OtherPlayer.Hand, OtherPlayer.PlayingFieldContainer, GameField);        
        this.Campaign.CPUPlayerPlay();

        this.Update();

		this.turnManager = new TurnManager();
		this.timer.WaitTime = 10;

		if (this.turnManager.IsPlayerTurn) {
			timer.Autostart = true;
			this.campaign = new Campaign(Difficulty.Easy, player, otherPlayer.Hand, otherPlayer.PlayingFieldContainer, gameField);        
			this.campaign.AdvanceLevel();
			
			if (timer.IsStopped()) {
				ChangeTurn();
			}

		} else {
			timer.Autostart = true;
			this.campaign.CPUPlayerPlay();
			if (timer.IsStopped()) {
				ChangeTurn();
			}
		}
	}

	public void Start()
	{
		this.player = new Player();
		this.otherPlayer = new CPUPlayer();
		// Refactorable.
		// This can be generalized to a way to use N amount of player,
		// here the game has a predifined amount of players and it's easy as assigning.  

		player.PlayingFieldContainer = GetNode("GameUI/CardDropArea").GetNode<HBoxContainer>("HBoxContainer");
		otherPlayer.PlayingFieldContainer = GetNode("GameUI/CardDropArea").GetNode<HBoxContainer>("HBoxContainer");
		
		player.Hand = GetNode<Hand>("GameUI/Hand");
		otherPlayer.Hand = GetNode<Hand>("GameUI/HandOther");

		gameField = GetNode<GameField>("GameUI");
		LevelLabel = GetNode<Label>("GameUI/Level/Label");
	}

	private void _on_end_turn_pressed()
	{
		timer.Stop();
		ChangeTurn();
	}

	public void ChangeTurn() {
		Button endTurn = GetNode<Button>("EndTurn");

		if (this.turnManager.IsPlayerTurn) {
			this.turnManager.EndTurn();
			GD.Print("Ahora es turno del oponente");

			endTurn.Disabled = true;
		}  else {
			this.turnManager.IsPlayerTurn = true;
			endTurn.Disabled = false;
			GD.Print("Ahora es tu turno");
		}
	}

	public void Update()
    {
        this.currentLevel = this.Campaign.CurrentLevel;

        this.LevelLabel.Text = "Level: " + this.currentLevel;
    }
}