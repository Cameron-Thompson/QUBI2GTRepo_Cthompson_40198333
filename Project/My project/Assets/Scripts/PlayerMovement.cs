using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20.0f;
    public float horizontalInput;
    public GameObject projectilePrefab;
    public float shotCooldown = 0.02f; // 0.02 seconds between shots
    private float lastShotTime = 0f;  // Tracks the time of the last shot
    public float forwardMovementSpeed = 20.0f;
    private Animator playerAnim;
    public AudioClip gameOver;
    private AudioSource playerAudio;
    private float Xboundary = 40;
    public static int deadZombies = 0;


    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player forward
        transform.Translate(Vector3.forward * Time.deltaTime * forwardMovementSpeed);

        // Get horizontal input and move the player horizontally
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        if (transform.position.x < -Xboundary)
        {
            transform.position = new Vector3(-Xboundary, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > Xboundary)
        {
            transform.position = new Vector3(Xboundary, transform.position.y, transform.position.z);
        }


        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastShotTime >= shotCooldown)
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);

            lastShotTime = Time.time;

        }

        if (playerAnim.GetBool("DeadPlayer_b") == true)
        {
            forwardMovementSpeed = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            Debug.Log("Game Over: Dead Zombies "+deadZombies);
            playerAnim.SetBool("DeadPlayer_b", true);
        }
        else if (other.gameObject.tag == "Barricade")
        {
            Debug.Log("Game Over: Dead Zombies " + deadZombies);
            playerAnim.SetBool("DeadPlayer_b", true);
        }
    }
}
