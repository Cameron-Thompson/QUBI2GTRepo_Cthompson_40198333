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

    private float spawnInterval = 2f;            // Adjust this value as needed
    private float spawnIntervalBarricade = 5.0f;  // Adjust this value as needed
    private float spawnIntervalShieldPowerup = 6.5f;
    private float spawnIntervalAmmoPowerup = 8.0f;


    private float spawnTimer = 0f;
    private float spawnTimerBarricade = 0f;
    private float spawnTimerShieldPowerup = 0f;
    private float spawnTimerAmmoPowerup = 0f;


    public GameObject barricade;
    public GameObject zombie;
    public GameObject BigZombie;
    public GameObject powerUpShield;
    public GameObject powerUpAmmo;

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
            if (z.activeSelf == false)
            {
                return;
            }
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
            if (bz.activeSelf == false)
            {
                return;
            }
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
        spawnTimerAmmoPowerup += Time.deltaTime;

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

        if (spawnTimerShieldPowerup >= spawnIntervalShieldPowerup)
        {
            SpawnShield(playerObj.transform.position.z);
            spawnTimerShieldPowerup = 0f;
        }

        if (spawnTimerAmmoPowerup >= spawnIntervalAmmoPowerup)
        {
            SpawnAmmoUp(playerObj.transform.position.z);
            spawnTimerAmmoPowerup = 0f;
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
        pooledZombie.transform.SetPositionAndRotation(spawnPos, zombie.transform.rotation);
    }

    private void SpawnBigZombie(Vector3 spawnPos)
    {
        GameObject pooledBigZombie = bigZombiesPool.Get();
        pooledBigZombie.transform.SetPositionAndRotation(spawnPos, BigZombie.transform.rotation);
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
        Vector3 spawnPos = new(Random.Range(-spawnRangeXBarricade, spawnRangeXBarricade), 8f, spawnPosZ);
        Instantiate(powerUpShield, spawnPos, powerUpShield.transform.rotation);
    }

    void SpawnAmmoUp(float playerPositionZ)
    {
        spawnPosZ = playerPositionZ + 300f;
        Vector3 spawnPos = new(Random.Range(-spawnRangeXBarricade, spawnRangeXBarricade), 8f, spawnPosZ);
        Instantiate(powerUpAmmo, spawnPos, powerUpAmmo.transform.rotation);
    }
}
