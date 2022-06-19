using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TotalManager : MonoBehaviour
{
    private float[,] SpawnPosition = new float[16, 2] { {-6, 6}, {-2.5f, 6}, {2.5f, 6}, {6, 6},
                                                        {-6, 1.5f}, {-2.5f, 1.5f}, {2.5f, 1.5f}, {6, 1.5f},
                                                        {-6, -1.5f}, {-2.5f, -1.5f}, {2.5f, -1.5f}, {6, -1.5f},
                                                        {-6, -6}, {-2.5f, -6}, {2.5f, -6}, {6, -6}};
    public GameObject enemyP;

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

        // end fever
        if(enemyBag.childCount == 0 && feverOn){
            SpawnEnemy();
            feverOn = false;
        }
        // end game - no more monster
        else if (enemyBag.childCount == 0)
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
                //feverOn = false;
                Debug.Log("fever OFF");
                feverManager.EndFever();
            }
        }
    }

    void SpawnEnemy(){
        for(int i = 0; i < SpawnPosition.GetLength(0); i+=3)
        {
            Vector2 pos = new Vector2(SpawnPosition[i,0], SpawnPosition[i,1]);
            GameObject temp = Instantiate(enemyP, pos, Quaternion.identity, enemyBag);
        }
    }

    public void GameOver(){
        PlayerPrefs.SetInt("score", totalScore);
        SceneManager.LoadScene("gameOver");
    }
}
