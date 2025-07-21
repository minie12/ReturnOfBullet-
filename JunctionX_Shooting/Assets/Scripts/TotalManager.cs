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

    public AudioSource bgAudio;
    public AudioClip[] bgClips;

    private float[,] spawnPosition = new float[16, 2] { {-6, 6}, {-2.5f, 6}, {2.5f, 6}, {6, 6},
                                                        {-6, 1.5f}, {-2.4f, 1.5f}, {2.6f, 1.5f}, {6, 1.5f},
                                                        {-6, -1.5f}, {-2.4f, -1.5f}, {2.6f, -1.5f}, {6, -1.5f},
                                                        {-6, -6}, {-2.5f, -6}, {2.5f, -6}, {6, -6}};
    private int monsterSpawnNumber = 1;

    public GameObject enemyP;
    private GameState gameState;

    public FeverManager feverManager;
    public Transform enemyBag;
    public Transform bulletBag;

    public PlayerMovement player;
    public Text[] enemyScoreTexts;
    int[] enemyScores = new int[4] { 1, 2, 3, 5 };
    public int red, green, orange, purple;
    int totalScore;
    int highScore;

    public Text scoreText, highScoreText;

    public Animator animBG;
    public SpriteRenderer backgroundBox;
    public SpriteRenderer feverBackSprite;
    public GameObject feverBackground;
    private float org_hue, hue;
    public float rainbowSpeed;

    private void Start()
    {
        bgAudio.volume = PlayerPrefs.GetFloat("volume", 0.2f);
        highScore = PlayerPrefs.GetInt("highscore", 0);
        highScoreText.text = highScore.ToString();
        Color.RGBToHSV(feverBackSprite.color, out org_hue, out _, out _);
        Color.RGBToHSV(backgroundBox.color, out org_hue, out _, out _);
        gameState = GameState.MAIN;

        // set UI Score text
        enemyScoreTexts[0].text = "+" + enemyScores[0].ToString();
        enemyScoreTexts[1].text = "+" + enemyScores[1].ToString();
        enemyScoreTexts[2].text = "+" + enemyScores[2].ToString();
        enemyScoreTexts[3].text = "+" + enemyScores[3].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // calc score
        totalScore = red * enemyScores[3] + green * enemyScores[2] + orange * enemyScores[1] + purple * enemyScores[0];
        scoreText.text = totalScore.ToString();

        if(enemyBag.childCount == 0)
        {
            if (gameState == GameState.MAIN)
            {
                bgAudio.clip = bgClips[1];
                bgAudio.Play();

                gameState = GameState.FEVER;
                feverBackground.gameObject.SetActive(true);
                feverManager.StartFever();
                animBG.SetBool("isFever", true);
                hue = org_hue;
            }
            else if(gameState == GameState.FEVER)
            {
                bgAudio.clip = bgClips[0];
                bgAudio.Play();
                // end fever
                feverBackground.gameObject.SetActive(false);
                removeBullets();
                UpdateEnemyScores();
                player.SetEnableMovement(false);
                player.SetEnableShooting(false);
                player.MoveToMiddle();
                Invoke("SpawnEnemy", 0.5f);
                feverBackSprite.color = Color.HSVToRGB(org_hue, 1, 1);
                backgroundBox.color = Color.HSVToRGB(org_hue, 1, 1);
                gameState = GameState.READY;
                animBG.SetBool("isFever", false);
            }
        }
        
        if (gameState == GameState.FEVER)
        {
            feverBackground.gameObject.SetActive(true);
            feverManager.SetAndCheckFever(Time.deltaTime);
            hue += rainbowSpeed / 1000;
            if (hue >= 1) hue = 0;
            backgroundBox.color = Color.HSVToRGB(hue, 1, 1);
            feverBackSprite.color = Color.HSVToRGB(hue, 1, 1);
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

        player.SetEnableMovement(true);
        player.SetEnableShooting(true);
        gameState = GameState.MAIN;
    }

    void UpdateEnemyScores()
    {
        for(int i = 0; i < 4; i++)
        {
            enemyScores[i]++;
        }

        // set UI Score text
        enemyScoreTexts[0].text = "+" + enemyScores[0].ToString();
        enemyScoreTexts[1].text = "+" + enemyScores[1].ToString();
        enemyScoreTexts[2].text = "+" + enemyScores[2].ToString();
        enemyScoreTexts[3].text = "+" + enemyScores[3].ToString();
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

    void removeBullets() 
    {
        foreach (Transform child in bulletBag)
        {
            Destroy(child.gameObject);
        }
    }

    public void GameOver(){
        PlayerPrefs.SetInt("score", totalScore);
        if (totalScore > highScore)
        {
            highScore = totalScore;
        }
        PlayerPrefs.SetInt("highscore", highScore);

        // stop player from moving
        player.enabled = false;

        for(int i = 0; i < enemyBag.childCount; i++)
        {
            GameObject enemy = enemyBag.GetChild(i).gameObject;
            // stop all monsters movement
            if (enemy.tag.Equals("enemyRed"))
            {
                enemy.GetComponent<EnemyRed>().StopMovement();
            }
            else
            {
                enemy.GetComponent<EnemyNormal>().StopMovement();
            }
        }

        for (int i = 0; i < bulletBag.childCount; i++)
        {
            // stop all bullets movment
            bulletBag.GetChild(i).gameObject.GetComponent<Bullet>().enabled = false;
        }

        Invoke("GameOver_", 1.5f);
    }

    void GameOver_()
    {
        SceneManager.LoadScene("gameOver norank");
    }

    public bool IsFever()
    {
        return gameState == GameState.FEVER;
    }
}
