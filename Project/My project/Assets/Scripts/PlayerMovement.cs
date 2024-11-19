using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    public float horizontalSpeed = 20.0f;
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
    public GameOverScreen gameOverScreen;
    private GameManager gameManager;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    //TODO Move some of this into a game manager type object

    // Update is called once per frame
    void Update()
    {
        // Move the player forward
        transform.Translate(Vector3.forward * Time.deltaTime * forwardMovementSpeed);

        // Get horizontal input and move the player horizontally
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * horizontalSpeed);

        if (transform.position.x < -Xboundary)
        {
            transform.position = new Vector3(-Xboundary, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > Xboundary)
        {
            transform.position = new Vector3(Xboundary, transform.position.y, transform.position.z);
        }


        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastShotTime >= shotCooldown/gameManager.difficultySelected && playerAnim.GetBool("DeadPlayer_b") == false)
        {
            Vector3 bulletVector = new Vector3(transform.position.x+1.2f, transform.position.y + 7, transform.position.z + 5);

            Instantiate(projectilePrefab, bulletVector, projectilePrefab.transform.rotation);

            lastShotTime = Time.time;

        }

        if (playerAnim.GetBool("DeadPlayer_b") == true)
        {
            forwardMovementSpeed = 0;
            Debug.Log(deadZombies);
            gameOverScreen.Setup(deadZombies,gameManager.points);
        }
        else if (gameManager.isGameActive == false)
        {
            forwardMovementSpeed = 0;
        }
        else if (gameManager.isGameActive == true)
        {
            forwardMovementSpeed = 40.0f * gameManager.difficultySelected;
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
