using UnityEngine;
using System.Collections;

public class EnemyRed : MonoBehaviour
{
    public float velocity;
    public Animator anim;

    Transform target;
    TotalManager manager; 
    bool hit;

    void Start(){
        hit = false;
        manager = GameObject.Find("GameObjects").GetComponent<TotalManager>();
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
        Vector3 direction = (target.position - transform.position).normalized;
        // 초가 아닌 한 프레임으로 가속도 계산하여 속도 증가
        //velocity = (velocity + accelaration * Time.deltaTime);
        // 해당 방향으로 무빙
        this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
                                               transform.position.y + (direction.y * velocity),
                                                  transform.position.z);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hit && other.gameObject.tag.Equals("bullet")){
            hit = true;
            Destroy(other);
            EnemyDead();
        }
    }

    void EnemyDead(){
        anim.SetBool("bulletHit", true);
        gameObject.GetComponent<Collider2D>().enabled = false;

        manager.red++;

        Invoke("EnemyDead_",0.4f);
    }
    void EnemyDead_(){
        Destroy(gameObject);
    }
}