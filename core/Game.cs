using Godot;
// placeholder for the game logic that can needs
// to be implement later by the other side of the
// team related to game logic.
public partial class Game : Node, IGame
{
    Player Player;
    PlayerBase OtherPlayer;

    Campaign Campaign;

    GameField GameField;

    Label LevelLabel;

    // atributo de turno: booleano.

    int currentLevel;
    public override void _Ready()
    {
        this.Start();

        this.Campaign = new Campaign(Difficulty.Easy, Player, OtherPlayer.Hand, OtherPlayer.PlayingFieldContainer, GameField);        
        this.Campaign.CPUPlayerPlay();

        this.Update();
    }

    public void Update()
    {
        this.currentLevel = this.Campaign.CurrentLevel;

        this.LevelLabel.Text = "Level: " + this.currentLevel;
        
    }
    public void Start()
    {
        Player = new Player();
        this.OtherPlayer = new CPUPlayer();

        // Refactorable.
        // This can be generalized to a way to use N amount of player,
        // here the game has a predifined amount of players and it's as easy as assigning.  

        Player.PlayingFieldContainer = GetNode("GameUI/CardDropArea").GetNode<HBoxContainer>("HBoxContainer");
        OtherPlayer.PlayingFieldContainer = GetNode("GameUI/CardDropArea").GetNode<HBoxContainer>("HBoxContainer");
        
        Player.Hand = GetNode<Hand>("GameUI/Hand");
        OtherPlayer.Hand = GetNode<Hand>("GameUI/HandOther");

        GameField = GetNode<GameField>("GameUI");

        // UI
        LevelLabel = GetNode<Label>("GameUI/Level/Label");
    }
}