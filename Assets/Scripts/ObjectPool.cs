using System.Collections;
using System.Collections.Generic;

public class ObjectPool <T>
{
    LinkedList<T> pool;

    public ObjectPool()
    {
        pool = new LinkedList<T>();
    }

    public void AddObjectToPool(T thisObject)
    {
        pool.AddLast(thisObject);
    }

    public void RemoveFromPool(T thisObject)
    {
        pool.Remove(thisObject);
    }

    public T GetObject()
    {
        return pool.First.Value;
    }

    public uint Size()
    {
        return (uint)pool.Count;
    }

}
