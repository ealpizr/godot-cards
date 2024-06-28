
using Godot;

public partial class MediumCPUPlayer : CustomCPUPlayer
{
    public override int Id { get; }
    public override string UserName { get; }
    public override int Points { get; }

    public override Hand Hand { get; set; }

    public override Hand PlayHand {get; set; }
    public MediumCPUPlayer(IInteractable interactable) : base(interactable)
    {
        this.interactable = interactable;

        this.Strategy.SetStrategy(new AggressiveStrategy());
    }

    // don't forget to change to override.
    // https://learn.microsoft.com/es-es/dotnet/csharp/misc/cs0506
    public new void ReceiveInteraction(PlayerBase interaction)
    {
        throw new System.NotImplementedException();
    }

    public override void SendInteraction(GameField gameField, PlayerBase interaction)
    {
        if (((CPUPlayer)interactable).Hand is null)
        {
            GD.Print("Hand is null. Please verify the hand is being initialized.");
        }

        // add more points to the strategy plan beforehand.

        ((CPUPlayer)interactable).PlayHand.Cards = this.Strategy.PlanAttack((Player)interaction, ((CPUPlayer)interactable));

        base.SendInteraction(gameField, interaction);
    }
}