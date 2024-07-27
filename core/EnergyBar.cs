using Godot;

public partial class EnergyBar : Control
{
	public int Energy { get; set; }

    private void UpdateEnergyBar()
    {
        GetNode<ProgressBar>("Container/Bar").Value = Energy;
        GetNode<Label>("Container/Label").Text = $"{Energy}/26";
    }

	public override void _Ready()
	{
		Energy = 0;
        UpdateEnergyBar();
    }
}
