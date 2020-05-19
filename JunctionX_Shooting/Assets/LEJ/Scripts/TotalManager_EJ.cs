using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalManager_EJ : MonoBehaviour
{
    public GameObject player;
    bool monCollide;
    public int monNumb;

    // Start is called before the first frame update
    void Start()
    {
        monNumb = 6;
    }

    // Update is called once per frame
    void Update()
    {
        monCollide = player.GetComponent<PlayerControl_EJ>().monCollide;
        if (monCollide)
            Debug.Log("END: COLLIDED WITH MONSTER");
        if (monNumb < 1)
            Debug.Log("END: MONSTER NOT FOUND");
    }
}
