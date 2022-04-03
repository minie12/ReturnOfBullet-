using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wall : MonoBehaviour
{
    public GameObject wall_east, wall_west, wall_south, wall_north;
    Vector2 MousePos, EnterPos, ExitPos, NextPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D bullet1)
    {

        if (bullet1.gameObject.CompareTag("bullet"))
        {

            EnterPos = bullet1.gameObject.transform.position;
        }
    }


    public void OnTriggerExit2D(Collider2D bullet1)
    {
        if (bullet1.gameObject.CompareTag("bullet"))
        {
            ExitPos = bullet1.gameObject.transform.position;
            if (bullet1.gameObject.GetComponent<Bullet>().isFirst)  //처음 부딪힌 경우
            {
                bullet1.gameObject.GetComponent<Bullet>().isFirst = false;  //이젠 더이상 첫번째가 아니라고 함

                //각도 계산
                float angle = (ExitPos.x - EnterPos.x) / (ExitPos.y - EnterPos.y);
                float dx = (ExitPos.x - EnterPos.x);

                float dy = ExitPos.y - EnterPos.y;
                double radian = Math.Atan(dx / dy);
                float distance;
                switch (gameObject.tag)
                {
                    //next bullet의 시작지점 정하기 
                    case "east":
                        distance = wall_east.transform.position.y - EnterPos.y;
                        NextPos = new Vector2(wall_west.transform.position.x, wall_west.transform.position.y + distance);
                        break;

                    case "west":
                        distance = wall_west.transform.position.y - EnterPos.y;
                        NextPos = new Vector2(wall_east.transform.position.x, wall_east.transform.position.y + distance);
                        break;

                    case "north":
                        distance = wall_north.transform.position.x - EnterPos.x;
                        NextPos = new Vector2(wall_south.transform.position.x + distance, wall_south.transform.position.y);
                        break;

                    case "south":
                        distance = wall_south.transform.position.x - EnterPos.x;
                        NextPos = new Vector2(wall_north.transform.position.x + distance, wall_north.transform.position.y);
                        break;


                }
                bullet1.gameObject.transform.position = NextPos;
            }
            else
            {
                //Destroy(bullet1.gameObject);
                //bullet1.gameObject.SetActive(false);

            }

        }
    }
}
