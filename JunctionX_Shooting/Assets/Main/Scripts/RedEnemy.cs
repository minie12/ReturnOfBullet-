﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RedEnemy : MonoBehaviour
{
    
    public float velocity;
    public Transform target;
    public Vector3 direction;
    Animator anim;

    GameObject Manager;

    void Start()
    {
        velocity = 0.08f;
        anim = gameObject.GetComponent<Animator>();

        Manager = GameObject.Find("GameObjects");
    }
    void Update()
    {
        // Player의 현재 위치를 받아오는 Object
        target = GameObject.Find("Player").transform;
        MoveToTarget();
    }

    public void MoveToTarget()
    {
        // Player의 위치와 이 객체의 위치를 빼고 단위 벡터화 한다.
        direction = (target.position - transform.position).normalized;
        // 초가 아닌 한 프레임으로 가속도 계산하여 속도 증가
        //velocity = (velocity + accelaration * Time.deltaTime);
        // 해당 방향으로 무빙
        this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
                                               transform.position.y + (direction.y * velocity),
                                                  transform.position.z);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("bullet"))
        {
            this.anim.SetBool("bulletHit", true);
            Destroy(other.gameObject);
            Invoke("disableCollider", 0.3f);
            Invoke("destroy", 0.4f);
        }
    }
    void disableCollider()
    {
        this.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    void destroy()
    {
        Manager.GetComponent<totalManager>().monNumb--;
        Manager.GetComponent<totalManager>().red++;
        Destroy(gameObject);
    }


}