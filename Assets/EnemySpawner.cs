using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemySpawnPositions;
    public GameObject enemyPrefab;
    public float timeBetweenEnemySpawn, startTimeBetweenEnemySpawn, minSpawnTime;
    bool oneShot = true;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenEnemySpawn = startTimeBetweenEnemySpawn;
    }

    void spawnEnemy()
    {
        int randPosition = Random.Range(0, enemySpawnPositions.Length);
        Instantiate(enemyPrefab, enemySpawnPositions[randPosition].transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.S))
        {
            spawnEnemy();
        }*/

        if (timeBetweenEnemySpawn <= 0)
        {
            if (oneShot)
            {
                oneShot = false;
                spawnEnemy();
                if (startTimeBetweenEnemySpawn > minSpawnTime)
                {
                    startTimeBetweenEnemySpawn -= 0.25f;

                }
                timeBetweenEnemySpawn = startTimeBetweenEnemySpawn;
                oneShot = true;

            }
        } else if (timeBetweenEnemySpawn > 0)
        {
            timeBetweenEnemySpawn -= Time.deltaTime;
        }
    }
}
