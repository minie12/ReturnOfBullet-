using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TotalManager : MonoBehaviour
{
    enum GameState
    {
        READY,
        MAIN,
        FEVER
    };

    private float[,] spawnPosition = new float[16, 2] { {-6, 6}, {-2.5f, 6}, {2.5f, 6}, {6, 6},
                                                        {-6, 1.5f}, {-2.5f, 1.5f}, {2.5f, 1.5f}, {6, 1.5f},
                                                        {-6, -1.5f}, {-2.5f, -1.5f}, {2.5f, -1.5f}, {6, -1.5f},
                                                        {-6, -6}, {-2.5f, -6}, {2.5f, -6}, {6, -6}};
    private int monsterSpawnNumber = 1;

    public GameObject enemyP;
    private GameState gameState;

    public FeverManager feverManager;
    public Transform enemyBag;

    public PlayerMovement player;
    public int red, green, orange, purple;
    int totalScore;

    public Text scoreText;

    public SpriteRenderer backgroundBox;
    private float org_hue, hue;
    public float rainbowSpeed;

    private void Start()
    {
        Color.RGBToHSV(backgroundBox.color, out org_hue, out _, out _);
        gameState = GameState.MAIN;
    }

    // Update is called once per frame
    void Update()
    {
        // calc score
        totalScore = red * 5 + green * 3 + orange * 2 + purple;
        scoreText.text = totalScore.ToString();

        if(enemyBag.childCount == 0)
        {
            if (gameState == GameState.MAIN)
            {
                gameState = GameState.FEVER;
                feverManager.StartFever();
                hue = org_hue;
            }
            else if(gameState == GameState.FEVER)
            {
                player.MoveToMiddle();
                Invoke("SpawnEnemy", 0.3f);
                backgroundBox.color = Color.HSVToRGB(org_hue, 1, 1);
                gameState = GameState.READY;
            }
        }

        /*
        //fever time
        if (Input.GetMouseButtonDown(1))
        {
            if (!feverOn)
            {
                feverOn = true;
                feverManager.StartFever();
                Color.RGBToHSV(backgroundBox.color, out hue, out sat, out bri);
                Debug.Log("fever On" + hue + sat + bri);
            }
            else
            {
                //feverOn = false;
                Debug.Log("fever OFF");
                feverManager.EndFever();
            }
        }*/

        if (gameState == GameState.FEVER)
        {
            hue += rainbowSpeed / 1000;
            if (hue >= 1) hue = 0;
            backgroundBox.color = Color.HSVToRGB(hue, 1, 1);
        }
    }

    void SpawnEnemy(){
        monsterSpawnNumber += 2;

        List<int> randomNumber = new List<int>();
        GetRandomNumbers(monsterSpawnNumber, ref randomNumber);

        for(int i = 0; i < monsterSpawnNumber; i++)
        {
            int num = randomNumber[i];
            Vector2 pos = new Vector2(spawnPosition[num, 0], spawnPosition[num, 1]);
            Instantiate(enemyP, pos, Quaternion.identity, enemyBag);
        }

        gameState = GameState.MAIN;
    }

    void GetRandomNumbers(int count, ref List<int> numbList)
    {
        while (numbList.Count < count)
        {
            int n = Random.Range(0, spawnPosition.GetLength(0));

            bool repeat = false;
            foreach(int num in numbList)
            {
                if(num == n)
                {
                    repeat = true;
                    break;
                }
            }

            if (!repeat) numbList.Add(n);
        }
    }

    public void GameOver(){
        PlayerPrefs.SetInt("score", totalScore);
        SceneManager.LoadScene("gameOver");
    }

    public bool IsFever()
    {
        return gameState == GameState.FEVER;
    }
}
