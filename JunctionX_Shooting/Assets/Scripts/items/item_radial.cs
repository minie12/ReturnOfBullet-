using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class item_radial : MonoBehaviour
{

    public GameObject bullet;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void bullet_shoot()
    {
        Vector3 v0 = new Vector3(0, 1, -10);
        Vector3 v1 = new Vector3(1, 1, -10);
        Vector3 v2 = new Vector3(1, 0, -10);
        Vector3 v3 = new Vector3(1, -1, -10);
        Vector3 v4 = new Vector3(0, -1, -10);
        Vector3 v5 = new Vector3(-1, -1, -10);
        Vector3 v6 = new Vector3(-1, 0, -10);
        Vector3 v7 = new Vector3(-1, 1, -10);

        Instantiate(bullet, gameObject.transform.position, Quaternion.LookRotation(v0, Vector3.forward));
        Instantiate(bullet, gameObject.transform.position, Quaternion.LookRotation(v1, Vector3.forward));
        Instantiate(bullet, gameObject.transform.position, Quaternion.LookRotation(v2, Vector3.forward));
        Instantiate(bullet, gameObject.transform.position, Quaternion.LookRotation(v3, Vector3.forward));
        Instantiate(bullet, gameObject.transform.position, Quaternion.LookRotation(v4, Vector3.forward));
        Instantiate(bullet, gameObject.transform.position, Quaternion.LookRotation(v5, Vector3.forward));
        Instantiate(bullet, gameObject.transform.position, Quaternion.LookRotation(v6, Vector3.forward));
        Instantiate(bullet, gameObject.transform.position, Quaternion.LookRotation(v7, Vector3.forward));

        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            bullet_shoot();
        }
    }
}
