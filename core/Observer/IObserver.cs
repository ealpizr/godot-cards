namespace GodotCards.DesignPatterns.Observer;

public interface IObserver<T>
{
    void Update(T state);
}
