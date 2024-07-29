namespace GodotCards.DesignPatterns.Observer;


public class TurnObserver : IObserver<Turn>
{
    private readonly ITurnAware owner;
    public  TurnDelegate TurnDelegate { get; private set; }

    public TurnObserver(ITurnAware owner, TurnDelegate isMyTurn)
    {
        this.owner = owner;
        this.TurnDelegate = isMyTurn;
    }

    public void Update(Turn state)
    {
        if (TurnDelegate(state))
        {
            owner.OnTurnStart();
        }
        else
        {
            owner.OnTurnEnd();
        }
    }
}
