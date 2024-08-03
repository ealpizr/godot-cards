using Godot;
using GodotCards.DesignPatterns.Observer;



public class TurnManager : Observable<Turn>
{
	private Timer timer;
	public bool IsPlayerTurn => State == Turn.Player;

	public TurnManager()
	{
		this.timer = new Timer();
		this.timer.WaitTime = 10;
		this.timer.OneShot = false;
		this.timer.Timeout += EndTurn;
	}

	public void Start(Turn turn = Turn.Player)
	{
		this.State = turn;
		this.timer.Start();
		NotifyObservers();
	}

	public void EndTurn()
	{
		if (State == Turn.Player)
		{
			State = Turn.Opponent;
		}
		else
		{
			State = Turn.Player;
		}

		NotifyObservers();
	}
}
