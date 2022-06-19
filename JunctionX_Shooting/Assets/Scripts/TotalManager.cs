using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TotalManager : MonoBehaviour
{
    public FeverManager feverManager;
    public Transform enemyBag;

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
        if (enemyBag.childCount == 0)
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
                feverManager.StartFever();
                Debug.Log("fever On");
            }
            else
            {
                feverOn = false;
                Debug.Log("fever OFF");
            }
        }
    }

    public void GameOver(){
        PlayerPrefs.SetInt("score", totalScore);
        SceneManager.LoadScene("gameOver");
    }
}
