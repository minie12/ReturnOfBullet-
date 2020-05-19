using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Player : MonoBehaviour
{
    public GameObject laserPrefab; //발사할 레이저를 저장합니다.
    public bool canShoot = true; //레이저를 쏠 수 있는 상태인지 검사합니다.
    const float shootDelay = 0.5f; //레이저를 쏘는 주기를 정해줍니다.
    float shootTimer = 0; //시간을 잴 타이머를 만들어줍니다.

    //player movement
    public float speed = 6.0f;
    private Vector3 moveDirection = Vector3.zero;
    public Camera camera;

    bool monCollide;
    bool bulletCollide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot) // 쏠 수 있는 상태인지 검사합니다.
        {
            if (shootTimer > shootDelay && Input.GetKey(KeyCode.Space)) //쿨타임이 지났는지와, 공격키인 스페이스가 눌려있는지 검사합니다.
            {
                Instantiate(laserPrefab, transform.position, Quaternion.identity); //레이저를 생성해줍니다.
                shootTimer = 0; //쿨타임을 다시 카운트 합니다.
            }
            shootTimer += Time.deltaTime; //쿨타임을 카운트 합니다.
        }

        // moving with keyboard
        float translation_X = Input.GetAxis("Horizontal") * speed;
        float translation_Y = Input.GetAxis("Vertical") * speed;
        translation_X *= Time.deltaTime;
        translation_Y *= Time.deltaTime;

        transform.position += new Vector3(translation_X, translation_Y, 0);

        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
                                                            mouse.x,
                                                            mouse.y,
                                                            this.transform.position.z - 7));
        Vector3 forward = mouseWorld - this.transform.position;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D other)
    //rigidBody가 무언가와 충돌할때 호출되는 함수 입니다.
    //Collider2D other로 부딪힌 객체를 받아옵니다.
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            monCollide = true;
            Debug.Log(monCollide);
        }
        if (other.gameObject.tag.Equals("bullet"))
        {
            bulletCollide = true;
        }
    }
}
