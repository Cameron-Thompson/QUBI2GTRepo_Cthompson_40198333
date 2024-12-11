using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float offsetUpperBound = 150;
    private float offsetLowerBoundZombie = 70;
    private float offsetLowerBoundRoad = 500;
    private GameObject player;
    private SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (transform.position.z > player.transform.position.z + offsetUpperBound && gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        else if (outOfBoundsZombieBarricades() && gameObject.tag == "Zombie")
        {
            spawnManager.zombiesPool.Release(gameObject);
        }
        else if (outOfBoundsZombieBarricades() && gameObject.tag == "BigZombie") {
            spawnManager.bigZombiesPool.Release(gameObject);
        }
        else if (outOfBoundsZombieBarricades() && gameObject.tag == "Barricade")
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < player.transform.position.z - offsetLowerBoundRoad && gameObject.tag == "Road")
        {
            Destroy(gameObject);
        }
    }

    private bool outOfBoundsZombieBarricades()
    {
        return transform.position.z < player.transform.position.z - offsetLowerBoundZombie;
    }
}
