public class NormalLevel
{
    public Card BetCard(Card card)
    {
        throw new System.NotImplementedException();
    }

    public PlayerBase GetPlayer()
    {
        return new EasyCPUPlayer(new Player());
    }
}