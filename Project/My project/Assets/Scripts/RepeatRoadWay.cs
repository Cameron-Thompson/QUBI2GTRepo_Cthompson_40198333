using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatRoadWay : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatLength;
    public GameObject Road;
    private GameObject playerObj;
    private float spawnPosZ;
    private float playerProgress;
    // Start is called before the first frame update
    void Start()
    {
        playerProgress = 300;
    }

    // Update is called once per frame
    void Update()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj.transform.position.z > playerProgress) 
        {
            playerProgress += 300;
            CreateRoad(playerObj.transform.position.z);
        }
        
    }

    void CreateRoad(float playerPositionZ)
    {
        float spawnPosZ = playerPositionZ + 344;
        Vector3 spawnPos = new Vector3(0.2f, 0, spawnPosZ);
        Instantiate(Road, spawnPos, Road.transform.rotation);
    }

}