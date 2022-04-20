﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerMovement : MonoBehaviour
{
    public TotalManager manager;

    public float speed;
    public GameObject bullet;

    // shooting 
    public int magazine;
    public Slider magazines;
    const float shootDelay = 0.001f; //레이저를 쏘는 주기를 정해줍니다.
    float shootTimer = 1; //시간을 잴 타이머를 만들어줍니다.

    void Start(){
        magazine = 10;
    }

    // Update is called once per frame
    void Update()
    {
        // moving with keyboard
        float translation_X = Input.GetAxis("Horizontal") * speed;
        float translation_Y = Input.GetAxis("Vertical") * speed;
        translation_X *= Time.deltaTime;
        translation_Y *= Time.deltaTime;
        transform.position += new Vector3(translation_X, translation_Y, 0);
        

        // get mouse pos and compare with player pos
        // player gun rotate toward mouse
        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
                                                            mouse.x,
                                                            mouse.y,
                                                            10));
        Vector3 forward = mouseWorld - this.transform.position;
        int degreeAddition = forward.x < 0 ? 180 : 0;
        float angle = degreeAddition + Mathf.Atan(forward.y / forward.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        
        // bullet number limit
        magazines.value = (float)(0.1*magazine);
        if (Input.GetMouseButtonDown(0))
        {
            if (magazine > 0)
            {
                if (shootTimer > shootDelay ) //쿨타임이 지났는지와, 공격키인 스페이스가 눌려있는지 검사합니다.
                {
                    magazine--;
                    bullet_shoot(); // when mouse button is clicked, shoot the bullet toward the mouse pos
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
        magazine++;

    }

    void bullet_shoot()
    {
        Vector3 MousePos = Input.mousePosition;     //마우스 포지션 받아오기

        float angle = (MousePos.x - gameObject.transform.position.x) / (MousePos.y - gameObject.transform.position.y);
        Vector3 dirToTar = MousePos - gameObject.transform.position;
        double degree = Math.Atan(angle) * Mathf.Rad2Deg;

        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
                                                            mouse.x,
                                                            mouse.y,
                                                            gameObject.transform.position.z));
        Vector3 forward = mouseWorld - gameObject.transform.position;

        Instantiate(bullet, gameObject.transform.position, Quaternion.LookRotation(forward, Vector3.forward));      //총알 생성

    }

    void OnTriggerEnter2D(Collider2D other)
    //rigidBody가 무언가와 충돌할때 호출되는 함수 입니다.
    //Collider2D other로 부딪힌 객체를 받아옵니다.
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Player: Enemy End");
            manager.GameOver();
        }
        if (other.gameObject.tag.Equals("bullet") && !other.gameObject.GetComponent<Bullet>().isFirst)
        {
            Debug.Log("Bullet End");
            manager.GameOver();
        }
    }
}
