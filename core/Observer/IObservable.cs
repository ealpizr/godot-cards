using System.Collections.Generic;

namespace GodotCards.DesignPatterns.Observer;

public interface IObservable<T>
{
    void Subscribe(IObserver<T> observer);
    void Unsubscribe(IObserver<T> observer);
    void NotifyObservers();
}
