using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRangeX = 120;
    private float spawnPosZ;
    private float startDelay = 2;
    private int spawnInterval = 500;
    public float spawnIntervalCurrent = 0;

    public GameObject player;
    public GameObject zombie;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(spawnIntervalCurrent);
        if (spawnIntervalCurrent == spawnInterval)
        {
            SpawnZombie();
            spawnIntervalCurrent = 0;
        }
        else 
        { 
            spawnIntervalCurrent += 5;
        }
    }

    void SpawnZombie()
    {
        spawnPosZ = player.transform.position.z + 500;
        Debug.Log(spawnPosZ);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX/2, spawnRangeX/2), 0, spawnPosZ);
        Instantiate(zombie, spawnPos, zombie.transform.rotation);
    }
}
