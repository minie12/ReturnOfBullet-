using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TotalManager : MonoBehaviour
{
    public int monNumb;

    public int red, green, orange, purple;
    int totalScore;

    public Text scoreText;
    public bool feverOn = false;

    // Update is called once per frame
    void Update()
    {
        // calc score
        totalScore = red * 5 + green * 3 + orange * 2 + purple;
        scoreText.text = totalScore.ToString();

        // end game - no more monster
        if (monNumb < 1)
        {
            PlayerPrefs.SetInt("score", totalScore);
            SceneManager.LoadScene("Clear");
        }

        //fever time
        if (Input.GetMouseButtonDown(1))
        {
            if (!feverOn)
            {
                feverOn = true;
                Debug.Log("fever On");
            }
            else
            {
                feverOn = false;
                Debug.Log("fever OFF");
            }
        }
    }

    public void EnemyIncrease(){ monNumb++; }
    public void EnemyDecrease(){ monNumb--; }
    public int GetMonNumb(){return monNumb;}

    public void GameOver(){
        PlayerPrefs.SetInt("score", totalScore);
        SceneManager.LoadScene("gameOver");
    }
}
