using Godot;
using GodotCards.DesignPatterns.Command;
using GodotCards.DesignPatterns.Observer;

// Why so many classes for CPU?
// Feels a little bit overkill.
public partial class DifficultyCPUPlayer : CustomCPUPlayer
{
    private readonly TurnManager turnManager;

    public DifficultyCPUPlayer(PlayerBase player, TurnManager turnManager) : base(player, player.Hand, player.PlayHand, player.Deck, player.Dice, player.EnergyBar, player.TurnObserver.TurnDelegate)
    {
        this.interactable = player;
        this.turnManager = turnManager;

        this.Strategy.SetStrategy(new PeaceFulStrategy());
    }

    public override void OnTurnStart()
    {
        // This should be executed by an invoker.
        ICommand rollDiceCommand = new RollDiceCommand(this);
        rollDiceCommand.Execute();

        ICommand endTurnCommand = new EndTurnCommand(this.turnManager);
        endTurnCommand.Execute();
    }

    public override void OnTurnEnd()
    {
        // We can do something here. Maybe show an alert or message.
    }

    // don't forget to change to override.
    // https://learn.microsoft.com/es-es/dotnet/csharp/misc/cs0506
    public new void ReceiveInteraction(PlayerBase interaction)
    {
        throw new System.NotImplementedException();
    }

    public override void ChangeLevel(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                this.Strategy.SetStrategy(new PeaceFulStrategy());
                break;
            case Difficulty.Medium:
                this.Strategy.SetStrategy(new AggressiveStrategy());
                break;
            default:
                this.Strategy.SetStrategy(new PeaceFulStrategy());
                break;
        }
    }

    public override void SendInteraction(GameField gameField, PlayerBase interaction)
    {
        if (((PlayerBase)interactable).Hand is null)
        {
            GD.Print("Hand is null. Please verify the hand is being initialized.");
        }

        ((PlayerBase)this.interactable).PlayHand.Cards = this.Strategy.PlanAttack((Player)interaction, (PlayerBase)this.interactable);

        base.SendInteraction(gameField, interaction);
        // behaviour such as: verify that the player is capable of winning.
        //this.interactable.SendInteraction(this);
    }
}
