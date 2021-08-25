using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public enum State
    {
        Idle,
        Chase,
        Attack
    }

    public State currentState;

    Transform playerTransform;

    [SerializeField] float chaseToDistance = 5f;
    [SerializeField] float coolDownAttackTime = 1f;
    [SerializeField] float speed = 5f;
    [SerializeField] Rigidbody projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    Transform spawnPoint;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        spawnPoint = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Attack();
                break;
        }
    }

    void Idle()
    {
        // Debug.Log("Idle state");
        // Do nothing/Patrol
    }

    void Chase()
    {
        // Debug.Log("Chase state");
        // Chase player till some distance
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, Time.deltaTime * speed);
        transform.LookAt(playerTransform.position);
        if (Vector3.Distance(playerTransform.position, transform.position) <= chaseToDistance)
        {
            currentState = State.Attack;
        }
    }

    void Attack()
    {
        transform.LookAt(playerTransform.position);
        // Debug.Log("Attack state");
        if (timer > coolDownAttackTime)
        {
            Shoot();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
        
        if (Vector3.Distance(playerTransform.position, transform.position) > chaseToDistance)
        {
            currentState = State.Chase;
        }
    }

    void Shoot()
    {
        Rigidbody rb = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        rb.velocity = projectileSpeed * transform.forward;
    }
}
