using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    ObjectPool<GameObject> enemyPool;

    [SerializeField] float spawnTime = 5f;
    [SerializeField] Transform[] spawnLocations;
    [SerializeField] int enemiesToSpawn = 5;
    [SerializeField] GameObject enemyPrefab;
    int locationIndex = 0;

    float timer = 0;

    GameObject[] enemies;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        enemyPool = new ObjectPool<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // GameObject[] enemies;
        // Instantiate Enemies
        enemies = new GameObject[enemiesToSpawn];
        for(int i = 0; i < enemiesToSpawn; ++i)
        {
            enemies[i] = Instantiate(enemyPrefab, spawnLocations[locationIndex].position, spawnLocations[locationIndex].rotation);
            enemies[i].transform.SetParent(transform);
            locationIndex++;
            locationIndex %= spawnLocations.Length;
        }

        // Observe the enemies
        for (int i = 0; i < enemies.Length; ++i)
        {
            for (int j = 0; j < enemies.Length; ++j)
                enemies[i].GetComponent<Health>().AddObserver(enemies[j].GetComponent<Enemy>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyPool.Size() > 0)
        {
            if(timer >= spawnTime)
            {
                timer = 0f;
                
                SpawnEnemy(enemyPool.GetObject());
                
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        // Remove the observer class

        // Remove the observer from other enemies subject
        for(int i = 0; i < enemiesToSpawn; ++i)
        {
            if (transform.GetChild(i).gameObject.activeInHierarchy)
            {
                // Debug.Log("Removing observer from : " + transform.GetChild(i).name);
                transform.GetChild(i).GetComponent<Health>().RemoveObserver(enemy.GetComponent<Enemy>());
            }
                
        }


        // Remove the subject class
        // enemy.GetComponent<Health>().ClearObservers();

        // Add enemy into the list
        enemyPool.AddObjectToPool(enemy);

        // Deactivate enemy
        enemy.SetActive(false);
    }

    public void SpawnEnemy(GameObject enemy)
    {
        // Debug.Log("Should Spawn Enemy");
        // Add the observer class
        
        // Add observer to other enemies subject
        // Add the subject class
        
        // Add other enemies to this subject
        // Spawn enemy at the location

        // Remove the enemy from the list
        enemyPool.RemoveFromPool(enemy);

        // Summon Enemy
        Summon(enemy);
    }

    void Summon(GameObject enemy)
    {
        enemy.SetActive(true);
        enemy.transform.position = spawnLocations[locationIndex].position;
        locationIndex++;
        locationIndex %= spawnLocations.Length;
        Health subject = enemy.GetComponent<Health>();
        for(int i = 0; i < enemiesToSpawn; ++i)
        {
            if(transform.GetChild(i).gameObject.activeInHierarchy)
            {
                // Debug.Log("Found " + i + " for : " + enemy.name);
                subject.AddObserver(transform.GetChild(i).GetComponent<Enemy>());
                transform.GetChild(i).GetComponent<Health>().AddObserver(enemy.GetComponent<Enemy>());
            }
        }
    }
}
