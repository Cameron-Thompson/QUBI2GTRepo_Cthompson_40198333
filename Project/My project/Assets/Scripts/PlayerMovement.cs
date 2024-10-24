using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20.0f;
    public float horizontalInput;
    public GameObject projectilePrefab;
    public float shotCooldown = 0.2f; // 2 seconds between shots
    private float lastShotTime = 0f;  // Tracks the time of the last shot
    public float forwardMovementSpeed = 20.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move the player forward
        transform.Translate(Vector3.forward * Time.deltaTime * forwardMovementSpeed);

        // Get horizontal input and move the player horizontally
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastShotTime >= shotCooldown)
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            lastShotTime = Time.time;
        }
    }
}
