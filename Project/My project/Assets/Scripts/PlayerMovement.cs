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
    private Animator playerAnim;
    public AudioClip gameOver;
    private AudioSource playerAudio;


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

        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastShotTime >= shotCooldown)
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            lastShotTime = Time.time;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            Debug.Log("Game Over");
            playerAnim.SetBool("DeadPlayer_b", true);
            playerAudio.PlayOneShot(gameOver, 1.0f);
            forwardMovementSpeed = 0;
        }
    }
}
