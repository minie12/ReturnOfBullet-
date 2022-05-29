using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_red : MonoBehaviour
{
    public GameObject[] greens;
    public GameObject red;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("touch");
            turn_to_red();
            Destroy(gameObject);
        }
    }

    void turn_to_red()
    {
        greens = GameObject.FindGameObjectsWithTag("enemyGreen");
        for (int i = 0; i < greens.Length; i++)
        {
            Destroy(greens[i]);
            Instantiate(red, greens[i].transform.position, Quaternion.identity);
        }
        //Destroy(greens);
    }
}
