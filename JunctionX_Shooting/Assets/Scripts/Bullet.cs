using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public bool isFirst;
    public Vector3 direction;

    Transform wall_east, wall_west, wall_south, wall_north;
    // Transform player;
    Vector2 MousePos, EnterPos, ExitPos, NextPos;

    void Start()
    {
        wall_east = GameObject.Find("wall_east").GetComponent<Transform>();
        wall_west = GameObject.Find("wall_west").GetComponent<Transform>();
        wall_south = GameObject.Find("wall_south").GetComponent<Transform>();
        wall_north = GameObject.Find("wall_north").GetComponent<Transform>();

        // player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        isFirst = true;
        direction = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(direction * 0.5f);

    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
    //     {
    //         EnterPos = gameObject.transform.position;
    //     }

    // }

    void OnCollisionEnter(Collision collision){
        Test();
    }

    void Test(){
        Debug.Log("ok!!!!");
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            ExitPos = gameObject.transform.position;

            if (isFirst)  //처음 부딪힌 경우
            {
                isFirst = false;  //이젠 더이상 첫번째가 아니라고 함
                direction = Vector3.down;
            }
        }
        
        if (other.gameObject.CompareTag("outWall")) {
            if (!isFirst)  //처음 부딪힌 경우
            {
                Destroy(gameObject);
            }
        }

    }
}