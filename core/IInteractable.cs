
public interface IInteractable
{
    public void ReceiveInteraction(IInteractable interaction);
    public void SendInteraction(IInteractable interaction);
}