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
    public virtual void SendInteraction(GameField gameField, PlayerBase interaction) {
        // default behaviour here, one base execution example
        // we could add things like: verification process for cards.
        if (PlayHand.Cards is not null)
        {
            GD.Print("Playing cards...");
            GD.Print(PlayHand.Cards.Count);
            foreach (Card item in PlayHand.Cards)
            {  
                GD.Print("Playing card: " + item.Name);
                GD.Print(item.Name);
                gameField.EmitSignal(GameField.SignalName.ReparentToHboxContainer, item, this.PlayingFieldContainer);

            }

        }

        else
        {
            GD.Print("No cards to play.");
        }
        ((Player)interaction).ReceiveInteraction(this);
    }

    public void Init(HBoxContainer container, Hand hand)
    {
        this.Hand = hand;
        this.PlayingFieldContainer = container;
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

public partial class MediumCPUPlayer : CustomCPUPlayer
{
    public MediumCPUPlayer(IInteractable interactable) : base(interactable)
    {
        this.interactable = interactable;

        this.Strategy = new AggressiveStrategy();
    }

    // don't forget to change to override.
    // https://learn.microsoft.com/es-es/dotnet/csharp/misc/cs0506
    public new void ReceiveInteraction(PlayerBase interaction)
    {
        throw new System.NotImplementedException();
    }

    public override void SendInteraction(GameField gameField, PlayerBase interaction)
    {
        if (((PlayerBase)interactable).Hand is null)
        {
            GD.Print("Hand is null. Please verify the hand is being initialized.");
        }

        // add more points to the strategy plan beforehand.

        ((PlayerBase)this.interactable).PlayHand.Cards = this.Strategy.PlanAttack((Player)interaction, (PlayerBase)this.interactable);

        base.SendInteraction(gameField, interaction);
        //this.interactable.SendInteraction(this);
    }
}