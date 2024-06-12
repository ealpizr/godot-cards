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
    public override void _Ready()
    {
        this.Start();

        this.campaign = new Campaign(Difficulty.Easy, player, otherPlayer.Hand, otherPlayer.PlayingFieldContainer, gameField);        
        this.campaign.AdvanceLevel();
        this.campaign.CPUPlayerPlay();
    }
    public void Start()
    {
        player = new Player();
        this.otherPlayer = new CPUPlayer();
        // Refactorable.
        // This can be generalized to a way to use N amount of player,
        // here the game has a predifined amount of players and it's easy as assigning.  

        player.PlayingFieldContainer = GetNode("GameUI/CardDropArea").GetNode<HBoxContainer>("HBoxContainer");
        otherPlayer.PlayingFieldContainer = GetNode("GameUI/CardDropArea").GetNode<HBoxContainer>("HBoxContainer");
        
        player.Hand = GetNode<Hand>("GameUI/Hand");
        otherPlayer.Hand = GetNode<Hand>("GameUI/HandOther");

        gameField = GetNode<GameField>("GameUI");
        }
}