using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private ParticleSystem bloodEffect;
    void Start()
    {
       gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
       spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //TODO clean up this method so that there are less game tag check


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Barricade") && !other.gameObject.CompareTag("Bullet"))
        {
            if ((gameObject.CompareTag("BigZombie") && other.gameObject.CompareTag("Zombie")) || (gameObject.CompareTag("Zombie") && other.gameObject.CompareTag("BigZombie")))
            {
                return;
            }

            if (!gameObject.CompareTag("BigZombie"))
            {
                HandleZombieKill(other,true,gameObject);
                gameManager.UpdateZombiesKilled(1, 1);
            }
            else
            {
                HandleZombieKill(other,false,gameObject);
                gameManager.UpdateZombiesKilled(1, 5);
            }
        }
        else if (other.gameObject.CompareTag("Barricade"))
        {
            Destroy(gameObject);
        }
    }


    private void HandleZombieKill(Collider other, bool isBigZombie,GameObject bullet)
    {
        bloodEffect = other.gameObject.GetComponent<ParticleSystem>();
        bloodEffect?.Play();
        ReleaseZombieAfterDelay(other.gameObject, 0.05f, isBigZombie, bullet);
    }
    private void ReleaseZombieAfterDelay(GameObject zombie, float delay, bool isBigZombie,GameObject bullet)
    {
        StartCoroutine(ReleaseAfterDelayCoroutine(zombie, delay, isBigZombie,bullet));
    }

    private IEnumerator ReleaseAfterDelayCoroutine(GameObject zombie, float delay, bool isBigZombie, GameObject bullet)
    {
        yield return new WaitForSeconds(delay);

        if (isBigZombie)
        {
            spawnManager.bigZombiesPool.Release(zombie);
        }
        else
        {
            spawnManager.zombiesPool.Release(zombie);
        }
        Destroy(bullet);
    }

}
