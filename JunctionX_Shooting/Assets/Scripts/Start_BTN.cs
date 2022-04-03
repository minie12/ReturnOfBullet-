using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Start_BTN : MonoBehaviour
{
    
    public GameObject start_mon, tutorial_mon;
    // Start is called before the first frame update
    void Start()
    {
        start_mon.gameObject.SetActive(false);
        tutorial_mon.gameObject.SetActive(false);
    }

    public void StartMouseOn()
    {
        //Debug.Log("들어옴");
        start_mon.SetActive(true);
    }
    public void TutorialMouseOn()
    {
        tutorial_mon.SetActive(true);
    }

    public void UIOff()
    {
        start_mon.gameObject.SetActive(false);
        tutorial_mon.gameObject.SetActive(false);
    }

    public void goPlay()
    {
        SceneManager.LoadScene("Play");
    }

    public void goTutorial()
    {
        SceneManager.LoadScene("Rank");
    }



}
