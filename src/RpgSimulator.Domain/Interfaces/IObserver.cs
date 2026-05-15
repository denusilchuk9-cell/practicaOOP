namespace RpgSimulator.Domain.Interfaces
{
    public interface IObserver
    {
        void Update(string message);
    }

    public interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string message);
    }
}