using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Start_BTN : MonoBehaviour
{
    public GameObject start_mon, tutorial_mon;

    public AudioSource audioSource;
    public Slider soundVolume;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("volume", 0.2f);
        soundVolume.value = PlayerPrefs.GetFloat("volume", 0.2f);
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
        try
        {
            start_mon.gameObject.SetActive(false);
            tutorial_mon.gameObject.SetActive(false);
        }
        catch
        {

        }
        
    }

    public void goPlay()
    {
        PlayerPrefs.SetFloat("volume", soundVolume.value);
        SceneManager.LoadScene("Play");
    }

    public void goTutorial()
    {
        SceneManager.LoadScene("Rank");
    }



}
