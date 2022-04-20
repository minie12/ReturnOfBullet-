using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject monster;
    // Start is called before the first frame update

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(monster, RandomPos(), Quaternion.identity);
        }
    }

    Vector3 RandomPos()
    {
        Vector3 min = new Vector3(-5, -4.5f, 0);
        Vector3 max = new Vector3(1.2f, 1.5f, 0);
        Vector3 myVector = new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
        return myVector;
    }
}
