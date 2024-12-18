using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Vector3 offset = new Vector3(0, 30, -15);
    private Vector3 rotationOffset = new Vector3(41, 0, 0); // Camera's fixed rotation

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.SetPositionAndRotation(player.transform.position + offset, Quaternion.Euler(rotationOffset));
    }
}
