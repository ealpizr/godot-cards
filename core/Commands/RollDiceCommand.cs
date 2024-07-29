namespace GodotCards.DesignPatterns.Command;

public class RollDiceCommand : ICommand
{
    private PlayerBase player;

    public RollDiceCommand(PlayerBase player)
    {
        this.player = player;
    }

    public async void Execute()
    {
        await this.player.Dice.Roll();
        int energy = this.player.Dice.GetSum();
        this.player.EnergyBar.AddEnergy(energy);
    }
}
