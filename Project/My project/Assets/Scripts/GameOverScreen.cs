using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI deadZombies;
    public TextMeshProUGUI points;

    public void Setup(int deadZombiesInt,int pointsInt)
    {
        string deadZombiesString = deadZombiesInt.ToString();
        string pointsString = pointsInt.ToString();

        gameObject.SetActive(true);
        deadZombies.text = deadZombiesString + " DEAD ZOMBIES";
        points.text = "TOTAL POINTS: " + pointsString.ToString();
    }
}
