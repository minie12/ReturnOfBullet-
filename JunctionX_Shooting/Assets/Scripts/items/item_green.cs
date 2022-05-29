using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_green : MonoBehaviour
{
    public GameObject green;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            green_emerge();
            Destroy(gameObject);
        }
    }

    void green_emerge()
    {
        float X = gameObject.transform.position.x;
        float Y = gameObject.transform.position.y;
        float Z = gameObject.transform.position.z;
        Instantiate(green, new Vector3(X + 2, Y + 2, Z), Quaternion.identity);
        Instantiate(green, new Vector3(X - 2, Y + 2, Z), Quaternion.identity);
        Instantiate(green, new Vector3(X + 2, Y - 2, Z), Quaternion.identity);
        Instantiate(green, new Vector3(X - 2, Y - 2, Z), Quaternion.identity);

    }
}
