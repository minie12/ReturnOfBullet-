using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Teleport: MonoBehaviour
{

    public int magazine;
    public Image[] bullets;
    public Slider magazines;
    const float shootDelay = 0.001f; //레이저를 쏘는 주기를 정해줍니다.
    float shootTimer = 1; //시간을 잴 타이머를 만들어줍니다.
    float angle;
    Vector3 MousePos, EnterPos, ExitPos, NextPos;
    public GameObject bullet, player, wall_east, wall_west, wall_north, wall_south;

    // Start is called before the first frame update
    void Start()
    {
        magazine = 10;
    }

    // Update is called once per frame
    void Update()
    {
        magazines.value = (float)(0.1*magazine);
        if (Input.GetMouseButtonDown(0))
        {
            if (magazine > 0)
            {
                if (shootTimer > shootDelay ) //쿨타임이 지났는지와, 공격키인 스페이스가 눌려있는지 검사합니다.
                {
                    magazine--;
                    //bullets[magazine].gameObject.SetActive(false);
                    bullet_shoot();
                    StartCoroutine("NewBullet");
                    shootTimer = 0; //쿨타임을 다시 카운트 합니다.
                }
                shootTimer += Time.deltaTime; //쿨타임을 카운트 합니다.
            }
            
        }

    }
    IEnumerator NewBullet()
    {
        yield return new WaitForSeconds(2f);
        //bullets[magazine].gameObject.SetActive(true);
        magazine++;

    }


    private void OnTriggerEnter(Collider bullet1)
    {
        if (bullet1.gameObject.CompareTag("bullet"))
        {
            EnterPos = bullet1.gameObject.transform.position;
        }
    }

    
    //private void OnTriggerExit(Collider bullet1)
    //{
    //    if (bullet1.gameObject.CompareTag("bullet"))
    //    {
    //        ExitPos = bullet1.gameObject.transform.position;
    //        if (bullet1.gameObject.GetComponent<Bullet>().isFirst)  //처음 부딪힌 경우
    //        {
    //            bullet1.gameObject.GetComponent<Bullet>().isFirst = false;  //이젠 더이상 첫번째가 아니라고 함
    //            //각도 계산
    //            angle = (ExitPos.x - EnterPos.x) / (ExitPos.y - EnterPos.y);
    //            float dx = (ExitPos.x - EnterPos.x);

    //            float dy = ExitPos.y - EnterPos.y;
    //            double radian = Math.Atan(dx / dy);
    //            float distance;
    //            switch (gameObject.tag)
    //            {
    //                //next bullet의 시작지점 정하기 
    //                case "east":
    //                    distance = wall_east.transform.position.y - EnterPos.y;
    //                    NextPos = new Vector2(wall_west.transform.position.x, wall_west.transform.position.y + distance);
    //                    break;

    //                case "west":
    //                    distance = wall_east.transform.position.y - EnterPos.y;
    //                    NextPos = new Vector2(wall_east.transform.position.x, wall_east.transform.position.y + distance);
    //                    break;

    //                case "north":
    //                    distance = wall_east.transform.position.x - EnterPos.x;
    //                    NextPos = new Vector2(wall_south.transform.position.x + distance, wall_south.transform.position.y);
    //                    break;

    //                case "south":
    //                    distance = wall_east.transform.position.x - EnterPos.x;
    //                    NextPos = new Vector2(wall_north.transform.position.x + distance, wall_west.transform.position.y);
    //                    break;

    //            }
    //        }
    //        //새로운 벽에서 쏴주기
    //        bullet1.gameObject.transform.position = NextPos;
    //    }
    //    else
    //    {
    //    }

    //}


    public void bullet_shoot()
    {

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

        Instantiate(bullet, player.transform.position, Quaternion.LookRotation(forward, Vector3.forward));      //총알 생성

    }


}
