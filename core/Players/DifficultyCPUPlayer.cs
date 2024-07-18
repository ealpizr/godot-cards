using Godot;

public partial class DifficultyCPUPlayer : CustomCPUPlayer
{

    public DifficultyCPUPlayer(IInteractable interactable) : base(interactable)
    {
        this.interactable = interactable;

        this.Strategy.SetStrategy(new PeaceFulStrategy());
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
