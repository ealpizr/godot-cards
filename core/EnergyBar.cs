using Godot;

public partial class EnergyBar : Control
{
    private int energy;

    private void UpdateEnergyBar()
    {
        GetNode<ProgressBar>("Container/Bar").Value = energy;
        GetNode<Label>("Container/Label").Text = $"{energy}/26";
    }

	public override void _Ready()
	{
        energy = 0;
        UpdateEnergyBar();
    }

    public bool CanConsumeEnergy(int amount)
    {
        return energy >= amount;
    }

    public void AddEnergy(int amount)
    {
        energy += amount;
        if (energy > 26)
        {
            energy = 26;
        }
        UpdateEnergyBar();
    }

    public void ConsumeEnergy(int amount)
    {
        energy -= amount;
        if (energy < 0)
        {
            energy = 0;
        }
        UpdateEnergyBar();
    }
}
