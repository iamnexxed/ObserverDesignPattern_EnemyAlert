using UnityEngine;
public class Enemy : MonoBehaviour, IObserver
{
    EnemyStateMachine enemySM;
   
    public void OnHealthDamage()
    {
        enemySM = GetComponent<EnemyStateMachine>();
        // Debug.Log("Start Chasing Player");
        enemySM.currentState = EnemyStateMachine.State.Chase;
    }

    public void ObserverDestroyed(IObserver observer)
    {
        GetComponent<Health>().RemoveObserver(observer);
    }
}
