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

		this.campaign = new Campaign(Difficulty.Easy, player, otherPlayer.Hand, otherPlayer.PlayingFieldContainer, gameField);        
		this.campaign.CPUPlayerPlay();

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
		if (this.turnManager.IsPlayerTurn) {
			this.turnManager.EndPlayerTurn();

			this.otherPlayer.Hand.HandStatus = false;
			this.otherPlayer.PlayHand.HandStatus = false;

			GD.Print("Ahora es turno del oponente");
		}  else {
			this.turnManager.StartPlayerTurn();

			this.otherPlayer.Hand.HandStatus = true;
			this.otherPlayer.PlayHand.HandStatus = true;

			GD.Print("Ahora es tu turno");
		}
	}

	public void Update()
	{
		this.currentLevel = this.campaign.CurrentLevel;

		this.LevelLabel.Text = "Level: " + this.currentLevel;
	}
}
