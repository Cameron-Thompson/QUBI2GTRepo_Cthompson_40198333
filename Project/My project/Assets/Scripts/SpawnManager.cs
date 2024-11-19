using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRangeX = 40f;
    private float spawnRangeXBarricade = 30f;
    private float spawnPosZ;

    // Spawn intervals in seconds
    private float spawnInterval = 0.2f;            // Adjust this value as needed
    private float spawnIntervalBarricade = 10.0f;  // Adjust this value as needed

    private float spawnTimer = 0f;
    private float spawnTimerBarricade = 0f;

    public GameObject barricade;
    public GameObject zombie;
    private GameObject playerObj = null;
    private Animator playerAnim;
    private GameManager gameManager;

    //TODO implement object pooling to reduce CPU load

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();


        // Cache the player object and animator
        playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            playerAnim = playerObj.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        spawnTimerBarricade += Time.deltaTime;

        if (playerAnim.GetBool("DeadPlayer_b") == true || gameManager.isGameActive == false)
        {
            return;
        }

        if (spawnTimer >= spawnInterval/gameManager.difficultySelected)
        {
                SpawnZombie(playerObj.transform.position.z);
                spawnTimer = 0f;
        }

        // Spawn barricade
        if (spawnTimerBarricade >= spawnIntervalBarricade/gameManager.difficultySelected)
        {
            SpawnBarricade(playerObj.transform.position.z);
            spawnTimerBarricade = 0f;
        }
    }

    void SpawnZombie(float playerPositionZ)
    {
        spawnPosZ = playerPositionZ + 200f;
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0f, spawnPosZ);
        Instantiate(zombie, spawnPos, zombie.transform.rotation);
    }

    void SpawnBarricade(float playerPositionZ)
    {
        spawnPosZ = playerPositionZ + 300f;
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeXBarricade, spawnRangeXBarricade), 0f, spawnPosZ);
        Instantiate(barricade, spawnPos, barricade.transform.rotation);
    }
}
