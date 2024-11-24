using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;

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

            Destroy(gameObject);
            Destroy(other.gameObject);
            PlayerMovement.deadZombies += 1;

            

            if(gameObject.tag != "BigZombie")
            {
                Debug.Log("game object tag" + gameObject.tag);
                Debug.Log("Otherr game object tag" + other.gameObject.tag);
                Debug.Log("regular zombie dead");
               gameManager.UpdateZombiesKilled(1,1);
            }
            else
            {
                Debug.Log("bigs zombie dead");
                gameManager.UpdateZombiesKilled(1, 5);
            }
        }
        else if (other.gameObject.tag == "Barricade")
        {
            Destroy(gameObject);
        }
    }

}
