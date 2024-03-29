﻿using UnityEngine;
using System.Collections;

public class EnemyNormal : MonoBehaviour
{
    public float velocity;
    public GameObject enemyBig;
    public GameObject enemySmall;
    public Animator anim;
    public float growDelay;
    public float splitDelay;

    TotalManager manager;
    Transform enemyBag;
    Vector3 targetPos;
    bool hit;

    void Start(){
        hit = false;
        manager = GameObject.Find("BlueBox").GetComponent<TotalManager>();
        enemyBag = GameObject.Find("EnemyBag").GetComponent<Transform>();

        SetTargetPos(); // set target position before starting to move

        // purple monster can't grow anymore
        if(!gameObject.CompareTag("enemyPurple"))
            Invoke("GrowUp",growDelay);
    }

    void Update(){
        Vector3 direction = (targetPos - gameObject.transform.position).normalized;
        this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
                                       transform.position.y + (direction.y * velocity),
                                          transform.position.z);

        if (Mathf.Abs(transform.position.x - targetPos.x) <= 0.1f && Mathf.Abs(transform.position.y - targetPos.y) <= 0.1f)
        {
            SetTargetPos();
        }
    }

    public void SetTargetPos()
    {
        float randomX = Random.Range(-7f, 7f);
        float randomY = Random.Range(-7.23f, 7f);
        targetPos = new Vector3(randomX, randomY, transform.position.z);
    }

    void GrowUp(){
        anim.SetBool("evolve",true);
        Invoke("GrowUp_", 0.45f);
    }
    void GrowUp_(){
        Destroy(gameObject);
        Instantiate(enemyBig, gameObject.transform.position, Quaternion.identity, enemyBag);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hit && other.gameObject.tag.Equals("bullet")){
            hit = true;
            Destroy(other.gameObject);
            SplitTwo();
        }
    }

    void SplitTwo(){
        if(gameObject.CompareTag("enemyPurple")) manager.purple++;
        else if(gameObject.CompareTag("enemyOrange")) manager.orange++;
        else if(gameObject.CompareTag("enemyGreen")) manager.green++;

        anim.SetBool("bulletHit", true);
        if(!gameObject.CompareTag("enemyGreen")){
            Invoke("SplitTwo_",splitDelay);
        }
        else
            Invoke("SplitTwoG_",splitDelay);
    }
    void SplitTwo_(){
        Destroy(gameObject);

        // divide into two
        Instantiate(enemySmall, gameObject.transform.position, Quaternion.identity, enemyBag);
        Instantiate(enemySmall, gameObject.transform.position, Quaternion.identity, enemyBag);
    }

    void SplitTwoG_(){
        // for green enemy
        Destroy(gameObject);
        Instantiate(enemySmall, gameObject.transform.position, Quaternion.identity, enemyBag);
    }

    public void DestroyEnemy(){
        CancelInvoke();
        StartCoroutine("DestroyEnemy_");
    }
    IEnumerator DestroyEnemy_(){
        anim.SetBool("bulletHit", true);

        yield return new WaitForSeconds(splitDelay);
        Destroy(gameObject);
    }

    public void StopMovement()
    {
        CancelInvoke();
        this.enabled = false;
    }
}