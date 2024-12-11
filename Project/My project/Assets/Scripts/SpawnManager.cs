using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    private float spawnRangeX = 40f;
    private float spawnRangeXBarricade = 30f;
    private float spawnPosZ;

    // Spawn intervals in seconds
    private float spawnInterval = 0.2f;            // Adjust this value as needed
    private float spawnIntervalBarricade = 10.0f;  // Adjust this value as needed
    private float spawnIntervalShieldPowerup = 10.0f;

    private float spawnTimer = 0f;
    private float spawnTimerBarricade = 0f;
    private float spawnTimerShieldPowerup = 0f;

    public GameObject barricade;
    public GameObject zombie;
    public GameObject BigZombie;
    public GameObject powerUpShield;

    private GameObject playerObj = null;
    private Animator playerAnim;
    private GameManager gameManager;
    public ObjectPool<GameObject> zombiesPool;
    public ObjectPool<GameObject> bigZombiesPool;
    public ObjectPool<GameObject> barricadesPool;

    //TODO implement object pooling to reduce CPU load

    void Start()
    {
        zombiesPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(zombie);
        }, z => {
            z.SetActive(true);
        }, z => {
            z.SetActive(false);
        }, z => {
            Destroy(z);
        }, true, 40, 50);


        bigZombiesPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(BigZombie);
        }, bz => {
            bz.SetActive(true);
        }, bz => {
            bz.SetActive(false);
        }, bz => {
            Destroy(bz);
        }, true, 10, 20);


        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();


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
        spawnTimerShieldPowerup += Time.deltaTime;

        if (playerAnim.GetBool("DeadPlayer_b") == true || gameManager.isGameActive == false)
        {
            return;
        }

        if (spawnTimer >= spawnInterval/gameManager.difficultySelected)
        {
                SpawnZombie(playerObj.transform.position.z);
                spawnTimer = 0f;
        }

        if (spawnTimerBarricade >= spawnIntervalBarricade/gameManager.difficultySelected)
        {
            SpawnBarricade(playerObj.transform.position.z);
            spawnTimerBarricade = 0f;
        }

        if (spawnTimerShieldPowerup >= spawnIntervalShieldPowerup * gameManager.difficultySelected/2)
        {
            SpawnShield(playerObj.transform.position.z);
            spawnTimerShieldPowerup = 0f;
        }

    }

    void SpawnZombie(float playerPositionZ)
    {
        spawnPosZ = playerPositionZ + 200f;
        Vector3 spawnPos = new(Random.Range(-spawnRangeX, spawnRangeX), 0f, spawnPosZ);
        int randomNumber = Random.Range(0, 30);

        if (gameManager.difficultySelected == 1 && randomNumber == 1)
        {
            SpawnBigZombie(spawnPos);
        }
        else if (gameManager.difficultySelected == 2 && (randomNumber == 1 || randomNumber == 2))
        {
            SpawnBigZombie(spawnPos);
        }
        else if (gameManager.difficultySelected == 3 && (randomNumber == 1 || randomNumber == 2 || randomNumber == 3))
        {
            SpawnBigZombie(spawnPos);
        }
        else
        {
            SpawnZombie(spawnPos);
        }
    }

    private void SpawnZombie(Vector3 spawnPos)
    {
        GameObject pooledZombie = zombiesPool.Get();
        pooledZombie.transform.position = spawnPos;
        pooledZombie.transform.rotation = zombie.transform.rotation;
    }

    private void SpawnBigZombie(Vector3 spawnPos)
    {
        GameObject pooledBigZombie = bigZombiesPool.Get();
        pooledBigZombie.transform.position = spawnPos;
        pooledBigZombie.transform.rotation = BigZombie.transform.rotation;
    }

    void SpawnBarricade(float playerPositionZ)
    {
        spawnPosZ = playerPositionZ + 300f;
        Vector3 spawnPos = new(Random.Range(-spawnRangeXBarricade, spawnRangeXBarricade), 0f, spawnPosZ);
        Instantiate(barricade, spawnPos, barricade.transform.rotation);
    }

    void SpawnShield(float playerPositionZ)
    {
        spawnPosZ = playerPositionZ + 300f;
        Vector3 spawnPos = new(Random.Range(-spawnRangeXBarricade+10, spawnRangeXBarricade-10), 8f, spawnPosZ);
        Instantiate(powerUpShield, spawnPos, powerUpShield.transform.rotation);
    }
}
