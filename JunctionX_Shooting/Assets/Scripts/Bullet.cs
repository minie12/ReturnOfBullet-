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
        // transform.Translate(Vector3.up*0.35f);
        // transform.Translate(Vector3.up*0.2f);

        // Vector3 direction = new Vector3(0,1,0);

        transform.Translate(direction * 0.35f);

        // if (isFirst)  transform.Translate(direction*0.2f);
        // else  transform.Translate(Vector3.left*0.2f);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            EnterPos = gameObject.transform.position;
        }

    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ExitPos = gameObject.transform.position;

            if (isFirst)  //처음 부딪힌 경우
            {
                isFirst = false;  //이젠 더이상 첫번째가 아니라고 함

                //각도 계산
                // float angle = (ExitPos.x - EnterPos.x) / (ExitPos.y - EnterPos.y);
                // float dx = (ExitPos.x - EnterPos.x);

                // float dy = ExitPos.y - EnterPos.y;
                // double radian = Math.Atan(dx / dy);
                // float distance;

                // switch (other.gameObject.tag)
                // {
                //     //next bullet의 시작지점 정하기 
                //     case "east":
                //         // distance = wall_east.transform.position.y - EnterPos.y;
                //         // NextPos = new Vector2(0, 0);
                //         NextPos = new Vector2(ExitPos.x, ExitPos.y);

                //         if(ExitPos.y < wall_east.transform.position.y) direction = Vector3.right;
                //         else direction = Vector3.left;

                //         // NextPos = new Vector2(wall_west.transform.position.x, wall_west.transform.position.y + distance);
                //         Debug.Log("Modified East");
                //         break;

                //     case "west":
                //         distance = wall_west.transform.position.y - EnterPos.y;
                //         NextPos = new Vector2(wall_east.transform.position.x, wall_east.transform.position.y + distance);
                //         // Debug.Log("West to East");
                //         break;

                //     case "north":
                //         distance = wall_north.transform.position.x - EnterPos.x;
                //         NextPos = new Vector2(wall_south.transform.position.x + distance, wall_south.transform.position.y);
                //         // Debug.Log("North to South");
                //         break;

                //     case "south":
                //         distance = wall_south.transform.position.x - EnterPos.x;
                //         NextPos = new Vector2(wall_north.transform.position.x + distance, wall_north.transform.position.y);
                //         // Debug.Log("South to North");
                //         break;
                // }

                // gameObject.transform.position = NextPos;



                direction = Vector3.down;
            }
        }
        else if (other.gameObject.CompareTag("outWall")) Destroy(gameObject);
    }
}