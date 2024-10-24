using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRangeX = 57;
    private float spawnPosZ;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    public GameObject player;
    public GameObject zombie;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnZombie", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnZombie()
    {
        spawnPosZ = player.transform.position.z + 400;

        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

        Instantiate(zombie, spawnPos, zombie.transform.rotation);
    }
}
