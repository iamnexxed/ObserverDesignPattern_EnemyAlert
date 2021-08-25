using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ISubject
{
    // public Transform enemiesToObserve;
    [SerializeField] int startingLives = 10;
    [SerializeField] int life = 10;
 
    LinkedList<IObserver> observers = new LinkedList<IObserver>();
    LinkedListNode<IObserver> iterator;

    [SerializeField] int observerCount = 0;
    public void AddObserver(IObserver observer)
    {
        if(observers.Find(observer) == null)
            observers.AddLast(observer);
        observerCount = observers.Count;
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
        observerCount = observers.Count;
    }

    public void ClearObservers()
    {
        observers.Clear();
        observerCount = observers.Count;
    }

    public void NotifyDamage()
    {
        // Debug.Log("Linked list has nodes : " + observers.Count);
        iterator = observers.First;
        while(iterator != null)
        {
            iterator.Value.OnHealthDamage();
            iterator = iterator.Next;
        }
       
    }

    public void NotifyDeath()
    {
        iterator = observers.First;
        while (iterator != null)
        {
            
            iterator.Value.ObserverDestroyed(GetComponent<Enemy>());
            iterator = iterator.Next;
        }
        
    }

    private void OnEnable()
    {
        life = startingLives;
        
    }

    public void DamageHealth(int value = 1)
    {
        life -= value;
        NotifyDamage();
        if (life <= 0)
        {
            ClearObservers();
            EnemyManager.instance.RemoveEnemy(gameObject);
            NotifyDeath();
            gameObject.SetActive(false);
        }
    }

  
}
