using Godot;

public enum Turn
{
	Player,
	Opponent
}

public partial class TurnManager
{
    private Timer timer;
	private Turn currentTurn;
    public bool IsPlayerTurn => currentTurn == Turn.Player;

    private PlayerBase player;
    private PlayerBase opponent;

    public TurnManager(PlayerBase player, PlayerBase opponent, Turn turn = Turn.Player)
    {
        this.timer = new Timer();
        this.timer.WaitTime = 10;
        this.timer.Timeout += EndTurn;
        this.player = player;
        this.opponent = opponent;
        this.currentTurn = turn;
    }

    public void Start()
    {
        this.timer.Start();
    }

    private void SetOpponentHandStatus(bool value)
    {
        // Why not use signals, events or observers here?
        this.opponent.Hand.HandStatus = value;
        this.opponent.PlayHand.HandStatus = value;
    }

	public void EndTurn() {
		if (currentTurn == Turn.Player) {
            currentTurn = Turn.Opponent;

            SetOpponentHandStatus(false);
        } else {
            currentTurn = Turn.Player;

            SetOpponentHandStatus(true);
        }
	}
}
