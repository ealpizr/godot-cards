using Godot;
using Godot.Collections;

// This is partial class but also abstract, the reason is C# technical rules.
// The idea is to have the DifficultyCPUPlayer
// override the CustomCPUPlayer but there are limitation to this.
// You can find more information here:
// https://learn.microsoft.com/es-es/dotnet/csharp/misc/cs0506
public abstract partial class CustomCPUPlayer : CPUPlayer
{
    public override int Id { get; }
    public override int Points { get; set; }

    public PlayContext Strategy { get; set; }

    public IInteractable interactable;

    public CustomCPUPlayer(IInteractable interactable) {
        this.interactable = interactable;
        this.Strategy = new PlayContext();
    }

    public abstract void ChangeLevel(Difficulty difficulty);
    // don't forget to change to override.
    // https://learn.microsoft.com/es-es/dotnet/csharp/misc/cs0506
    public override void ReceiveInteraction(PlayerBase interaction)
    {
        throw new System.NotImplementedException();
    }

    public override void SendInteraction(GameField gameField, PlayerBase interaction)
    {
        interactable.SendInteraction(gameField, interaction);
    }

    public override void Init(HBoxContainer container, Hand hand, Deck deck, Array<Card> cards)
    {
        throw new System.NotImplementedException();
    }
}