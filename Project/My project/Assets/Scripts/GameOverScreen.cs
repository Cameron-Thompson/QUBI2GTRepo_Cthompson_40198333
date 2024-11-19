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
    public TextMeshProUGUI highScore;
    private int highScoreVal = 0;


    public void Setup(int deadZombiesInt,int pointsInt)
    {
        string deadZombiesString = deadZombiesInt.ToString();
        string pointsString = pointsInt.ToString();

        gameObject.SetActive(true);
        deadZombies.text = deadZombiesString + " DEAD ZOMBIES";
        points.text = "TOTAL POINTS: " + pointsString.ToString();
        if (pointsInt > highScoreVal)
        {
            setHighScore(pointsInt);
        }
    }

    public void setHighScore(int pointsInt)
    {
        highScoreVal = pointsInt;
        highScore.text = "HIGH SCORE: " + highScoreVal;
    }
    
}
