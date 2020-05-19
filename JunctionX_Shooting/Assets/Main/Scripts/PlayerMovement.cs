using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Camera camera;

    public GameObject bullet;

    public bool monCollide, bulletCollide;

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
        //Debug.Log(mouseWorld);
        int degreeAddition = forward.x < 0 ? 180 : 0;
        float angle = degreeAddition + Mathf.Atan(forward.y / forward.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // when mouse button is clicked, shoot the bullet toward the mouse pos
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle - 90));
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    //rigidBody가 무언가와 충돌할때 호출되는 함수 입니다.
    //Collider2D other로 부딪힌 객체를 받아옵니다.
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            monCollide = true;
        }
        if (other.gameObject.tag.Equals("bullet") && !other.gameObject.GetComponent<Bullet>().isFirst)
        {
            bulletCollide = true;
            Debug.Log(monCollide);
        }
    }
}
