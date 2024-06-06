using System.IO;
using Godot;

public partial class CPUPlayer : PlayerBase, IInteractable
{
    public override int Id { get; }
    public override string UserName { get; }
    public override int Points { get; }

    public override Hand Hand { get; set; }

    public override Hand PlayHand {get; set; }

    public CPUPlayer() {
        this.PlayHand = new Hand();
    }

    public void ReceiveInteraction(PlayerBase interaction) {
        
    }
    public virtual void SendInteraction(PlayerBase interaction) {
        // default behaviour here, one base execution example
        // we could add things like: verification process for cards.
        if (PlayHand.Cards is not null)
        {
            foreach (Card item in PlayHand.Cards)
            {
                GD.Print(item.Name);
            }
        }

        else
        {
            GD.Print("No cards to play.");
        }
        ((Player)interaction).ReceiveInteraction(this);
    }
}

public partial class EasyCPUPlayer : CustomCPUPlayer
{
    public EasyCPUPlayer(IInteractable interactable) : base(interactable)
    {
        this.interactable = interactable;

        this.Strategy = new PeaceFulStrategy();
    }

    // don't forget to change to override.
    // https://learn.microsoft.com/es-es/dotnet/csharp/misc/cs0506
    public new void ReceiveInteraction(PlayerBase interaction)
    {
        throw new System.NotImplementedException();
    }

    public override void SendInteraction(PlayerBase interaction)
    {
        GD.Print(this.Hand is null);
        // foreach (Card item in this.Hand.Cards)
        // {
        //     GD.Print(item.Name);
        // }

        this.PlayHand.Cards = this.Strategy.PlanAttack((Player)interaction, this);

        base.SendInteraction(interaction);
        // behaviour such as: verify that the player is capable of winning.
        //this.interactable.SendInteraction(this);
    }
}