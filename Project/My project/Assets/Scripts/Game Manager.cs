using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pointsText;

    public int zombiesKilledCount;
    public bool isGameActive;
    public GameObject titleScreen;
    public int difficultySelected;
    public int points;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateZombiesKilled(int kill)
    {
        zombiesKilledCount += kill;
        scoreText.text = "Zombies Killed: " + zombiesKilledCount;
        UpdatePoints(kill);
    }

    public void UpdatePoints(int kill)
    {
        points += difficultySelected * 5 * kill;
        pointsText.text = "Points: " + points;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startGame(int difficulty)
    {
        difficultySelected = difficulty;
        isGameActive = true;
        pointsText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        zombiesKilledCount = 0;
        UpdateZombiesKilled(0);
        titleScreen.SetActive(false);
    }

}
