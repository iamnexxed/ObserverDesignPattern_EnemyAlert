using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float destroyTime = 2f;
    public int damage = 1;
    public enum ProjectileType
    {
        Player,
        Enemy
    }

    public ProjectileType currentType;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnCollisionEnter(Collision other)
    {
        switch (currentType)
        {
            case ProjectileType.Player:
                if (other.collider.CompareTag("Enemy"))
                {
                    other.collider.GetComponent<Health>().DamageHealth(damage);
                    Destroy(gameObject);
                }
                break;

            case ProjectileType.Enemy:
                if (other.collider.CompareTag("Player"))
                {
                    
                    // Debug.Log("Collided with Player");
                    
                    Destroy(gameObject);
                }
                break;

            default:
                // Destroy(gameObject, destroyTime);
                break;
        }

    }

}
