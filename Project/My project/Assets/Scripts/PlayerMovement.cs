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
    public AudioClip pickupShield;
    public AudioClip pickupAmmo;

    private AudioSource playerAudio;
    private float Xboundary = 40;
    public static int deadZombies = 0;
    public GameOverScreen gameOverScreen;
    private GameManager gameManager;
    private BoxCollider playerDetectCollisions;
    private float shotCooldownModifier;
    public bool hasShield = false;
    public bool hasAmmoUp = false;
    public GameObject hasShieldIndicator;
    public GameObject hasAmmoUpIndicator;


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
        transform.Translate(forwardMovementSpeed * Time.deltaTime * Vector3.forward);

        // Get horizontal input and move the player horizontally
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(horizontalInput * horizontalSpeed * Time.deltaTime * Vector3.right);

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
            if (hasAmmoUp)
            {
                Vector3 bulletVector2 = new Vector3(transform.position.x + 3.2f, transform.position.y + 7, transform.position.z + 5);
                Vector3 bulletVector3 = new Vector3(transform.position.x - 1.2f, transform.position.y + 7, transform.position.z + 5);
                Vector3 bulletVector4 = new Vector3(transform.position.x + 5.2f, transform.position.y + 7, transform.position.z + 5);
                Vector3 bulletVector5 = new Vector3(transform.position.x - 3.2f, transform.position.y + 7, transform.position.z + 5);

                Instantiate(projectilePrefab, bulletVector, projectilePrefab.transform.rotation);
                Instantiate(projectilePrefab, bulletVector2, projectilePrefab.transform.rotation);
                Instantiate(projectilePrefab, bulletVector3, projectilePrefab.transform.rotation);
                Instantiate(projectilePrefab, bulletVector4, projectilePrefab.transform.rotation);
                Instantiate(projectilePrefab, bulletVector5, projectilePrefab.transform.rotation);
            }
            else
            {
               Instantiate(projectilePrefab, bulletVector, projectilePrefab.transform.rotation);
            }
            muzzleFlash.Play();


            lastShotTime = Time.time;

        }

        if (playerAnim.GetBool("DeadPlayer_b") == true)
        {
            forwardMovementSpeed = 0;
            gameOverScreen.Setup(gameManager.zombiesKilledCount, gameManager.points);
        }
        else if (gameManager.isGameActive == false)
        {
            forwardMovementSpeed = 0;
        }
        else if (gameManager.isGameActive == true && (gameManager.difficultySelected == 2 || gameManager.difficultySelected == 3))
        {
            forwardMovementSpeed = 30.0f * gameManager.difficultySelected;
        }
        else
        {
            forwardMovementSpeed = 45.0f;
        }

        hasShieldIndicator.transform.position = transform.position + new Vector3(0,1,0);
        hasAmmoUpIndicator.transform.position = transform.position + new Vector3(0,0,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie") || other.gameObject.CompareTag("BigZombie") && gameManager.isGameActive == true)
        {
            if (hasShield == true)
            {
                hasShield = false;
                hasShieldIndicator.SetActive(false);
                return;
            }
            killPlayer();
            playerAnim.SetBool("DeadPlayerZombie_b", true);
        }
        else if (other.gameObject.CompareTag("Barricade"))
        {
            killPlayer();
            playerAnim.SetBool("DeadPlayerBarricade_b", true);
        }
        
        if (other.gameObject.CompareTag("ShieldPowerup"))
        {
            hasShield = true;
            hasShieldIndicator.SetActive(true);
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(pickupShield,2.5f);
            StartCoroutine(DisableAmmoUpAfterDelay(5f));
        }

        if (other.gameObject.CompareTag("BulletPowerUp"))
        {
            hasAmmoUp = true;
            hasAmmoUpIndicator.SetActive(true);
            playerAudio.PlayOneShot(pickupAmmo, 2.5f);
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

    private IEnumerator DisableAmmoUpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasAmmoUp = false;
        hasAmmoUpIndicator.SetActive(false);
    }
}
