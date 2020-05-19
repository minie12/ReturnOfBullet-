using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class A_enemy2 : MonoBehaviour
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
    public GameObject GrownUp;
    

    void Start()
    {
        velocity = 0.05f;
        //target = GameObject.Find("player").transform;
        RandomXY();
        Invoke("Growing", 2);
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        RandomMove();
    }

    void Growing()
    {
        Debug.Log("Before pasing");
        Pos = gameObject.transform.position;
        Debug.Log("Before pasing");
        this.anim.SetBool("evolve", true);
        Destroy(gameObject);
        Debug.Log("PassingTrhoug");
        GameObject enemy3 = (GameObject)Instantiate(Enemy, gameObject.transform.position, Quaternion.identity);
    }

    public void RandomXY()
    {
        randomX = Random.Range(-6f, 6f);
        randomY = Random.Range(-3f, 3f);
        randomVector = new Vector3(randomX, randomY, transform.position.z);
        //Invoke("RandomXY", 1f);
        //Debug.Log("New Location: " + randomVector);

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

        //Debug.Log(gameObject.transform.position);

        if (Mathf.Abs(transform.position.x - randomVector.x) <= 0.1f && Mathf.Abs(transform.position.y - randomVector.y) <= 0.1f)
        {
            //Debug.Log("NewPlace");
            RandomXY();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            //분열
            GameObject enemy1 = (GameObject)Instantiate(Enemy, gameObject.transform.position, Quaternion.identity);
            //enemy1.anim.Controller = aOrange;
            GameObject enemy2 = (GameObject)Instantiate(Enemy, gameObject.transform.position, Quaternion.identity);
            //enemy2.anim.Controller = aOrange;
        }
    }


}