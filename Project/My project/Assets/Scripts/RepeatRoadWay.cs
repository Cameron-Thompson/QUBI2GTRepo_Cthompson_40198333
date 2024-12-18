using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatRoadWay : MonoBehaviour
{
    public GameObject Road;
    private GameObject playerObj;
    private float playerProgress;
    // Start is called before the first frame update
    void Start()
    {
        playerProgress = 300;
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {


        if (playerObj.transform.position.z > playerProgress) 
        {
            playerProgress += 300;
            CreateRoad(playerObj.transform.position.z);
        }
        
    }

    void CreateRoad(float playerPositionZ)
    {
        float spawnPosZ = playerPositionZ + 334f;
        Vector3 spawnPos = new Vector3(0, 0, spawnPosZ);
        Instantiate(Road, spawnPos, Road.transform.rotation);
    }

}
