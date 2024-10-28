using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Barricade" && other.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            PlayerMovement.deadZombies += 1;
        }
        else if (other.gameObject.tag == "Barricade")
        {
            Destroy(gameObject);
        }
    }

}
