using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySpawner : MonoBehaviour
{

    public GameObject[] batterySpawnPositions;
    public GameObject batteryPrefab;
    public float timeBetweenBatterySpawn, startTimeBetweenBatterySpawn;
    bool oneShot = true;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenBatterySpawn = startTimeBetweenBatterySpawn;
    }

    void spawnBattery()
    {
        int randPosition = Random.Range(0, batterySpawnPositions.Length);
        if (batterySpawnPositions[randPosition].GetComponent<BatterySpawnPosition>().empty == true)
        {
            Debug.Log("Battery Spawned");
            Instantiate(batteryPrefab, batterySpawnPositions[randPosition].transform.position, Quaternion.identity);
            batterySpawnPositions[randPosition].GetComponent<BatterySpawnPosition>().empty = false;

        } 
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.B))
        {
            spawnBattery();
        }*/

        if (timeBetweenBatterySpawn <= 0)
        {
            if (oneShot)
            {
                oneShot = false;
                spawnBattery();
                timeBetweenBatterySpawn = startTimeBetweenBatterySpawn;
                oneShot = true;

            }
        }
        else if (timeBetweenBatterySpawn > 0)
        {
            timeBetweenBatterySpawn -= Time.deltaTime;
        }
    }
}
