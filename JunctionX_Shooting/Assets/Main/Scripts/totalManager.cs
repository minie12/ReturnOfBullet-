using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class totalManager : MonoBehaviour
{
    public GameObject player;
    public Image gameOverUI;
    bool monCollide, bulletCollide;

    public int monNumb, length;

    public int red, green, orange, purple;
    int totalScore;

    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        // calc score
        totalScore = red * 5 + green * 3 + orange * 2 + purple;
        scoreText.text = totalScore.ToString();

        // End Game
        monCollide = player.GetComponent<PlayerMovement>().monCollide;
        bulletCollide = player.GetComponent<PlayerMovement>().bulletCollide;
        

        if (monCollide || bulletCollide)
        {
            PlayerPrefs.SetInt("score", totalScore);
            SceneManager.LoadScene("gameOver");
        }

        if (monNumb < 1)
        {
            PlayerPrefs.SetInt("score", totalScore);
            SceneManager.LoadScene("Clear");
        }
    }

}
