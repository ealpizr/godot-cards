namespace GodotCards.DesignPatterns.Observer;

public interface ITurnAware
{
    void OnTurnStart();
    void OnTurnEnd();
}
