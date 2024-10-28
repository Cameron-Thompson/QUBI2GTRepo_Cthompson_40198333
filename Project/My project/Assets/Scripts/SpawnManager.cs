using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRangeX = 40;
    private float spawnPosZ;
    private float startDelay = 2;
    private int spawnInterval = 250;
    public float spawnIntervalCurrent = 0;

    public GameObject zombie;
    private GameObject playerObj = null;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        if (spawnIntervalCurrent == spawnInterval)
        {
            SpawnZombie(playerObj.transform.position.z);
            spawnIntervalCurrent = 0;
        }
        else 
        { 
            spawnIntervalCurrent += 1;
        }
    }

    void SpawnZombie(float playerPositionZ)
    {
        spawnPosZ = playerPositionZ + 500;
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX/2, spawnRangeX/2), 0, spawnPosZ);
        Instantiate(zombie, spawnPos, zombie.transform.rotation);
    }
}
