using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    public GameObject Enemy;
    //상수로 움직일 속도를 지정해 줍니다.
    Transform target;
    public float velocity;
    public Vector3 direction;
    float randomX;  //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
    float randomY;  //적이 나타날 Y좌표를 랜덤으로 생성해 줍니다.
    Vector3 randomVector;

    Animator anim;
    Vector3 Pos;

    GameObject Manager;


    void Start()
    {
        velocity = 0.05f;
        //target = GameObject.Find("player").transform;
        RandomXY();
        anim = gameObject.GetComponent<Animator>();

        Manager = GameObject.Find("GameObjects");
    }
    void Update()
    {
        RandomMove();
    }

    //public void Growing()
    //{
    //    if(gameObject.)
    //}

    public void RandomXY()
    {
        randomX = Random.Range(-7f, 7f);
        randomY = Random.Range(-7.23f, 7f);
        randomVector = new Vector3(randomX, randomY, transform.position.z);
        //Invoke("RandomXY", 1f);
       // Debug.Log("New Location: " + randomVector);

    }

    public void RandomMove()
    {
        direction = (randomVector - gameObject.transform.position).normalized;
        //this.transform.position = new Vector3(transform.position.x + (randomX * velocity),
        //                                           transform.position.y + (randomY * velocity),
        //                                           transform.position.z);
        this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
                                       transform.position.y + (direction.y * velocity),
                                          transform.position.z);

      

        if (Mathf.Abs(transform.position.x - randomVector.x) <= 0.1f && Mathf.Abs(transform.position.y - randomVector.y) <= 0.1f)
        {
           
            RandomXY();
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("bullet"))
        {
            this.anim.SetBool("bulletHit", true);
            Destroy(other.gameObject);
            Invoke("destroy", 0.7f);
        }
    }
    void destroy()
    {
        Pos = gameObject.transform.position;
        Destroy(gameObject);
        //분열
        Manager.GetComponent<totalManager>().monNumb++;
        Manager.GetComponent<totalManager>().purple++;
        GameObject enemy1 = (GameObject)Instantiate(Enemy, gameObject.transform.position, Quaternion.identity);
        GameObject enemy2 = (GameObject)Instantiate(Enemy, gameObject.transform.position, Quaternion.identity);
    }


}