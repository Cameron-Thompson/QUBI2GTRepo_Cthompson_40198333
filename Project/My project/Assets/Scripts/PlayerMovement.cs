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
    public ParticleSystem muzzleFlash;
    private ParticleSystem bloodEffect;
    private Animator playerAnim;
    public AudioClip gameOver;
    public AudioClip gunShot;
    private AudioSource playerAudio;
    private float Xboundary = 40;
    public static int deadZombies = 0;
    public GameOverScreen gameOverScreen;
    private GameManager gameManager;
    private BoxCollider playerDetectCollisions;
    private float shotCooldownModifier;
    public bool hasShield = false;
    public GameObject hasShieldIndicator;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        bloodEffect = GetComponent<ParticleSystem>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        Debug.Log(playerAnim != null ? "Animator assigned" : "Animator is null");
        Debug.Log(playerAudio != null ? "AudioSource assigned" : "AudioSource is null");
        Debug.Log(gameManager != null ? "GameManager assigned" : "GameManager is null");
        Debug.Log(playerDetectCollisions != null ? "BoxCollider assigned" : "BoxCollider is null");
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

        if (gameManager.difficultySelected == 1)
        {
            shotCooldownModifier = 1.5f;
        }
        else
        {
            shotCooldownModifier = gameManager.difficultySelected;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastShotTime >= shotCooldown/shotCooldownModifier && playerAnim.GetBool("DeadPlayer_b") == false)
        {
            Vector3 bulletVector = new Vector3(transform.position.x+1.2f, transform.position.y + 7, transform.position.z + 5);
            playerAudio.PlayOneShot(gunShot, 0.5f);
            Instantiate(projectilePrefab, bulletVector, projectilePrefab.transform.rotation);
            muzzleFlash.Play();


            lastShotTime = Time.time;

        }

        if (playerAnim.GetBool("DeadPlayer_b") == true)
        {
            forwardMovementSpeed = 0;
            gameOverScreen.Setup(gameManager.zombiesKilledCount,gameManager.points);
        }
        else if (gameManager.isGameActive == false)
        {
            forwardMovementSpeed = 0;
        }
        else if (gameManager.isGameActive == true)
        {
            forwardMovementSpeed = 30.0f * gameManager.difficultySelected;
        }

        hasShieldIndicator.transform.position = transform.position + new Vector3(0,0,0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie" || other.gameObject.tag == "BigZombie" && gameManager.isGameActive == true)
        {
            if (hasShield == true)
            {
                hasShield = false;
                hasShieldIndicator.gameObject.SetActive(false);
                return;
            }
            killPlayer();
            playerAnim.SetBool("DeadPlayerZombie_b", true);
        }
        else if (other.gameObject.tag == "Barricade")
        {
            killPlayer();
            playerAnim.SetBool("DeadPlayerBarricade_b", true);
        }
        
        if (other.gameObject.CompareTag("ShieldPowerup"))
        {
            hasShield = true;
            hasShieldIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
        }
    }

    private void killPlayer()
    {
        horizontalSpeed = 0;
        gameManager.isGameActive = false;
        gameObject.GetComponent<Collider>().enabled = false;
        bloodEffect.Play();
        playerAudio.Stop();
        playerAudio.PlayOneShot(gameOver, 1.0f);
        playerAnim.SetBool("DeadPlayer_b", true);
    }
}
