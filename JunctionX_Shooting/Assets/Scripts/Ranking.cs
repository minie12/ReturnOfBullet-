using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    int length=10;

    int[] scores = new int[10];
    string[] names = new string[10];

    public Text[] ranks;

    // Start is called before the first frame update
    void Start()
    {
        dataLoad();
    }

    // Update is called once per frame
    void Update()
    {
        dataLoad();
        for (int i = 0; i < length; i++)
        {
            ranks[i].gameObject.SetActive(true);
            ranks[i].text = (i + 1) + ".        " + names[i] + "          " + scores[i];
        }
    }


    void dataLoad()
    {
        
        //length = PlayerPrefs.GetInt("length", 0);
        for (int i = 0; i < length; i++) {
            names[i]=PlayerPrefs.GetString(i+"name","unknown");
            scores[i]=PlayerPrefs.GetInt(i + "score", 0);
        }

    }
    void dataSave()
    {
        PlayerPrefs.SetInt("length", length);
        for(int i = 0; i < length; i++)
        {
            PlayerPrefs.SetString(i + "name", names[i]);
            PlayerPrefs.SetInt(i + "score", scores[i]);
        }
    }
}
