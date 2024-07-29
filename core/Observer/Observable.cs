using System.Collections.Generic;

namespace GodotCards.DesignPatterns.Observer;

public abstract class Observable<T> : IObservable<T>
{
    private readonly List<IObserver<T>> observers = new List<IObserver<T>>();
    protected T State { get; set; }

    public void Subscribe(IObserver<T> observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Unsubscribe(IObserver<T> observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (IObserver<T> observer in observers)
        {
            observer.Update(State);
        }
    }
}