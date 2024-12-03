using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;
    private ParticleSystem bloodEffect;
    void Start()
    {
       gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //TODO clean up this method so that there are less game tag check


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Barricade" && other.gameObject.tag != "Bullet")
        {
            if ((gameObject.CompareTag("BigZombie") && other.gameObject.tag == "Zombie") || (gameObject.CompareTag("Zombie") && other.gameObject.tag == "BigZombie"))
            {
                return;
            }

         
            if(gameObject.tag != "BigZombie")
            {
                //call blood effect 
                bloodEffect = other.gameObject.GetComponent<ParticleSystem>();
                bloodEffect.Play();
                Destroy(gameObject,0.05f);
                Destroy(other.gameObject,0.05f);
                PlayerMovement.deadZombies += 1;
                gameManager.UpdateZombiesKilled(1,1);
            }
            else
            {
                //call blood script?
                bloodEffect = other.gameObject.GetComponent<ParticleSystem>();
                bloodEffect.Play();
                Destroy(gameObject, 0.05f);
                Destroy(other.gameObject, 0.05f);
                PlayerMovement.deadZombies += 1;
                gameManager.UpdateZombiesKilled(1, 5);
            }
        }
        else if (other.gameObject.tag == "Barricade")
        {
            Destroy(gameObject);
        }
    }

}
