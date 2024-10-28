using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRangeX = 40;
    private float spawnRangeXBarricade = 30;
    private float spawnPosZ;
    private float startDelay = 2;
    private int spawnInterval = 10;
    private int spawnIntervalBarricade = 250;

    public int spawnIntervalCurrent = 0;
    private int spawnIntervalCurrentBar = 0;
    public GameObject barricade;
    public GameObject zombie;
    private GameObject playerObj = null;
    private Animator playerAnim;


    // Start is called before the first frame update
    void Start()
    {
        // Limit framerate to cinematic 24fps.
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void Update()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerAnim = playerObj.GetComponent<Animator>();

        if (spawnIntervalCurrent == spawnInterval && playerAnim.GetBool("DeadPlayer_b") == false)
        {
            SpawnZombie(playerObj.transform.position.z);
            spawnIntervalCurrent = 0;
        }
        else if(spawnIntervalBarricade == spawnIntervalCurrentBar && playerAnim.GetBool("DeadPlayer_b") == false)
        {
            Debug.Log("Spawn barricade");
            SpawnBarricade(playerObj.transform.position.z);
            spawnIntervalCurrentBar = 0;
        }
        else 
        { 
            spawnIntervalCurrent += 1;
            spawnIntervalCurrentBar += 1;
        }
    }

    void SpawnZombie(float playerPositionZ)
    {
        spawnPosZ = playerPositionZ + 300;
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        Instantiate(zombie, spawnPos, zombie.transform.rotation);
    }

    void SpawnBarricade(float playerPositionZ)
    {
        spawnPosZ = playerPositionZ + 200;
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeXBarricade, spawnRangeXBarricade), 0, spawnPosZ);
        Instantiate(barricade, spawnPos, barricade.transform.rotation);
    }
}
