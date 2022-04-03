using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isFirst;


    // Start is called before the first frame update
    private void OnEnable()
    {
        isFirst = true;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up*0.35f);
 
    }
}
