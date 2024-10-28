using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float offsetUpperBound = 200;
    private float offsetLowerBoundZombie = 70;
    private float offsetLowerBoundRoad = 500;
    private GameObject player; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (transform.position.z > player.transform.position.z + offsetUpperBound && gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < player.transform.position.z - offsetLowerBoundZombie && gameObject.tag == "Zombie")
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < player.transform.position.z - offsetLowerBoundZombie && gameObject.tag == "Barricade")
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < player.transform.position.z - offsetLowerBoundRoad && gameObject.tag == "Road")
        {
            Destroy(gameObject);
        }
    }
}
