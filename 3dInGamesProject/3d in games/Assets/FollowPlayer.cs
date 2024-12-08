using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float distance = 15.0f;
    public float height = 10.0f;

    void LateUpdate()
    {
        Vector3 offset = -player.transform.forward * distance + Vector3.up * height;
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position + Vector3.up * 3); // Adjust the height of the look-at target if needed
    }
}
