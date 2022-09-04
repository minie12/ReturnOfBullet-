using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class changeScene : MonoBehaviour
{
    public InputField username;
    int length, totalScore;
    int[] scores = new int[6];
    string[] names = new string[6];

    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("score", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    

    public void dataSort()
    {
        Debug.Log("Sort");
        dataLoad();
        if (length == 0)
        {
            scores[0] = totalScore;
            names[0] = username.text;
        }
        else
        {
            int index = length+1;
            for (int i = 0; i < length; i++)
            {
                if (scores[i] <= totalScore)
                {
                    index = i; 

                    Debug.Log("After Finding index: " + index);

                    //for (int k = 0; k < (length + 1 - index); k++)
                    //{
                    //    scores[length - k + 1] = scores[length - k];
                    //    names[length - k + 1] = names[length - k];
                    //}

                    for (int k = 4; k > index; k--)
                    {
                        scores[k] = scores[k - 1];
                        names[k] = names[k - 1];
                    }

                    scores[index] = totalScore;
                    names[index] = username.text;

                    break;
                }
            }

           
        }
        dataSave();


    }

    void dataLoad()
    {
        totalScore = PlayerPrefs.GetInt("score", 0);
        length = PlayerPrefs.GetInt("length", 0);

        for (int i = 0; i < length; i++)
        {
            names[i] = PlayerPrefs.GetString(i + "name", "unknown");
            scores[i] = PlayerPrefs.GetInt(i + "score", 0);
        }
    }

    void dataSave()
    {
        if (length > 5)
        {
            length = 5;
        }
        else
        {
            length++;
        }
        Debug.Log(length);
            PlayerPrefs.SetInt("length", length);
            for (int i = 0; i < length; i++)
            {
                PlayerPrefs.SetString(i + "name", names[i]);
            Debug.Log(names[i]);
                PlayerPrefs.SetInt(i + "score", scores[i]);
            }
        // SceneManager.LoadScene("Rank");
    }

    public void delete()
    {
        PlayerPrefs.DeleteAll();
    }
}
