using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 미완성
public class CameraCollider : MonoBehaviour
{

    public float north, south, east, west;

    // Start is called before the first frame update
    void Start()
    {

        if (north == 0.0f) north = 0.926f;
        if (south == 0.0f) south = 0.076f;
        if (east == 0.0f) east = 0.765f;
        if (west == 0.0f) west = 0.23f;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        
    }

    //플레이어 화면 이탈 방지---
    void PlayerMoveRange(Vector3 pos){

        if(pos.y > north) pos.y = north; // 화면 상단으로 이탈했을 때
        if(pos.y < south) pos.y = south; // 화면 하단으로 이탈했을 때
        if(pos.x > east) pos.x = east; // 우측
        if(pos.x < west) pos.x = west; // 좌측

        // 위치 보정
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    //총알 화면 이탈 여부---
    //void BulletMoveRange(Vector3 pos){

    //    bool trigger = false;

    //    if(pos.y > north) trigger = true;
    //    if(pos.y < south) trigger = true;
    //    if(pos.x > east) trigger = true;
    //    if(pos.x < west) trigger = true;

    //    // if (trigger) {
    //    //     if (isFirst)  //처음 부딪힌 경우
    //    //     {
    //    //         direction = Vector3.down;
    //    //         isFirst = false;  //이젠 더이상 첫번째가 아니라고 함

    //    //     } else {
    //    //         Destroy(gameObject);
    //    //     }
    //    // }
    //} 

}