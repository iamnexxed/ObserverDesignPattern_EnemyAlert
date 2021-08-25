
public interface ISubject 
{
    void AddObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyDamage();
    void NotifyDeath();
}
