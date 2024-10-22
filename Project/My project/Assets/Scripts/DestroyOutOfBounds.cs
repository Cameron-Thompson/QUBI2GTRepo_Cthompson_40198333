using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float offsetUpperBound = 800;
    private float offsetLowerBound = 30;
    public GameObject player; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If object goes past player view in game, remove object 
        if (transform.position.z > player.transform.position.z + offsetUpperBound && gameObject.tag=="Bullet")
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < player.transform.position.z - offsetLowerBound && gameObject.tag=="Zombie")
        {
            Destroy(gameObject);
        }
    }
}
