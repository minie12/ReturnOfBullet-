using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public bool isFirst;
    public Vector3 direction;
    public float speed;

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
        transform.Translate(direction * speed);
        wallPosition();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            ExitPos = gameObject.transform.position;

            if (isFirst)  //처음 부딪힌 경우
            {
                direction = Vector3.down;
                isFirst = false;  //이젠 더이상 첫번째가 아니라고 함
                Debug.Log("Wall");
            }
        }
        
        if (other.gameObject.CompareTag("outWall")) {
            if (!isFirst)  //처음 부딪힌 경우
            {
                Debug.Log("outWall");
                Destroy(gameObject);
            }
        }

    }

    //화면 이탈 여부---
    void wallPosition(){
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.y > 0.926f || pos.y < 0.076f || pos.x > 0.765f || pos.x < 0.23f) {
            if (isFirst)  //처음 부딪힌 경우
            {
                direction = Vector3.down;
                isFirst = false;  //이젠 더이상 첫번째가 아니라고 함

            } else {
                Destroy(gameObject);
            }
        }
    }    

}