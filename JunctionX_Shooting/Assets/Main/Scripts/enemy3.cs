using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class enemy3 : MonoBehaviour
{
    public GameObject Enemy;
    //상수로 움직일 속도를 지정해 줍니다.
    Transform target;
    public float velocity;
    public Vector3 direction;

    float randomX;  //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
    float randomY;  //적이 나타날 Y좌표를 랜덤으로 생성해 줍니다.
    Vector3 randomVector;

    Vector3 Pos;
    public GameObject GrownUp;
    Animator anim;

    GameObject Manager;

    void Start()
    {
        velocity = 0.04f;
        //target = GameObject.Find("player").transform;
        RandomXY();
        Invoke("Growing", 6);
        anim = gameObject.GetComponent<Animator>();

        Manager = GameObject.Find("GameObjects");
    }
    void Update()
    {
        RandomMove();
    }

    void Growing()
    {
        
        this.anim.SetBool("evolve", true);
        Invoke("changeAnim",0.5f);
       
    }

    void changeAnim()
    {
        Pos = gameObject.transform.position;
        Destroy(gameObject);

        GameObject enemy3 = (GameObject)Instantiate(GrownUp, Pos, Quaternion.identity);
    }

    public void RandomXY()
    {
        randomX = Random.Range(-7f, 7f);
        randomY = Random.Range(-7.23f, 7f);
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
            this.anim.SetBool("bulletHit", true);
            Destroy(other.gameObject);
            Invoke("destroy", 0.3f);

            //enemy1.anim.Controller = aOrange;
        }
    }

     void destroy()
    {
        Pos = gameObject.transform.position;
        Destroy(gameObject);
        //분열
        Manager.GetComponent<totalManager>().green++;
        GameObject enemy1 = (GameObject)Instantiate(Enemy, gameObject.transform.position, Quaternion.identity);
    }

}