using System.IO;
using Godot;

public partial class CPUPlayer : PlayerBase, IInteractable
{
    public override int Id { get; }
    public override string UserName { get; }
    public override int Points { get; }

    public IStrategy Strategy { get; set; }

    public override Hand Hand { get; set; }

    public override void _Ready()
    {
       
    }

    public override void _PhysicsProcess(double delta)
    {
      
    }

    public void ReceiveInteraction(IInteractable interaction)
    {
        throw new System.NotImplementedException();
    }

    public void SendInteraction(IInteractable interaction)
    {
        throw new System.NotImplementedException();
    }

    public void Think(PlayerBase playerBase)
    {
        throw new System.NotImplementedException();
    }
}