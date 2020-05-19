using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class background : MonoBehaviour
{
    Vector3 MousePos, EnterPos, ExitPos, NextPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerExit2D(Collider2D bullet1)
    {
        if (bullet1.gameObject.CompareTag("bullet"))
        {
                Destroy(bullet1.gameObject);
                bullet1.gameObject.SetActive(false);

        }
    }
}
