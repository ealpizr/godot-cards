using Godot;

public partial class Player: PlayerBase, IInteractable, IControllable
{
    public override int Id { get; }
    public override string UserName { get; }
    public override int Points { get; }

    public override Hand Hand { get; set; }

    public void OnGuiInput(InputEvent e)
    {
        GD.Print("Player being clicked.");
    }

    public void OnInput(InputEvent e)
    {
        throw new System.NotImplementedException();
    }

    public void OnMouseEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnMouseExit()
    {
        throw new System.NotImplementedException();
    }

    public void ReceiveInteraction(IInteractable interaction)
    {
        throw new System.NotImplementedException();
    }

    public void SendInteraction(IInteractable interaction)
    {
        throw new System.NotImplementedException();
    }
} 