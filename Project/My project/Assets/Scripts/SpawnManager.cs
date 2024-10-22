using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRangeX = 120;
    private float spawnPosZ;
    private float startDelay = 2;
    private float spawnInterval = 2.5f;
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
        spawnPosZ = player.transform.position.z + 500;

        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX/2, spawnRangeX/2), 0, spawnPosZ);

        Instantiate(zombie, spawnPos, zombie.transform.rotation);
    }
}
