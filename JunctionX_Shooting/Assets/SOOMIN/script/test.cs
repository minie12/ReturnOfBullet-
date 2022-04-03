using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class test : MonoBehaviour
{

    float angle;
    bool isWall, isFirst;
    Vector3 MousePos, EnterPos, ExitPos, NextPos;
    public GameObject bullet, player, wall_east, wall_west, wall_north, wall_south;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))

        {
            bullet_shoot();
        }


    }

    /*
    private void OnTriggerEnter(Collider bullet1)
    {
        if (bullet1.gameObject.CompareTag("Pass"))
        {
            EnterPos = bullet1.gameObject.transform.position;
        }
    }

    
    private void OnTriggerExit(Collider bullet1)
    {
        if (bullet1.gameObject.CompareTag("bullet"))
        {
            ExitPos = bullet1.gameObject.transform.position;
            if (bullet1.gameObject.GetComponent<Bullet>().isFirst)  //처음 부딪힌 경우
            {
                bullet1.gameObject.GetComponent<Bullet>().isFirst = false;  //이젠 더이상 첫번째가 아니라고 함
                //각도 계산
                angle = (ExitPos.x - EnterPos.x) / (ExitPos.y - EnterPos.y);
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
                        distance = wall_east.transform.position.y - EnterPos.y;
                        NextPos = new Vector2(wall_east.transform.position.x, wall_east.transform.position.y + distance);
                        break;

                    case "north":
                        distance = wall_east.transform.position.x - EnterPos.x;
                        NextPos = new Vector2(wall_south.transform.position.x + distance, wall_south.transform.position.y);
                        break;

                    case "south":
                        distance = wall_east.transform.position.x - EnterPos.x;
                        NextPos = new Vector2(wall_north.transform.position.x + distance, wall_west.transform.position.y);
                        break;

                }
            }
            //새로운 벽에서 쏴주기
            bullet1.gameObject.transform.position = NextPos;
        }
        else
        {
            Destroy(bullet1);
        }
        bullet1.gameObject.SetActive(false);
    }
    */


    public void bullet_shoot()
    {
        int x_side, y_side;
        MousePos = Input.mousePosition;     //마우스 포지션 받아오기
        
        angle = (MousePos.x - player.transform.position.x) / (MousePos.y - player.transform.position.y);
        Vector3 dirToTar = MousePos - player.transform.position;
        double degree = Math.Atan(angle) * Mathf.Rad2Deg;



        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
                                                            mouse.x,
                                                            mouse.y,
                                                            player.transform.position.z));
        Vector3 forward = mouseWorld - player.transform.position;
 

        if (MousePos.x < player.transform.position.x)
        {
            x_side = -1;
        }
        else
        {
            x_side = 1;
        }
        if (MousePos.y < player.transform.position.y)
        {
            y_side = -1;
        }
        else
        {
            y_side = 1;
        }
        Instantiate(bullet, player.transform.position, Quaternion.LookRotation(forward, Vector3.forward));      //총알 생성

    }

}
