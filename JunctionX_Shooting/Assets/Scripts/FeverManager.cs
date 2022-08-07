using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverManager : MonoBehaviour
{
    private float[,] feverPosition = new float[16, 2] { {-6, 6}, {-2.5f, 6}, {2.5f, 6}, {6, 6},
                                                        {-6, 1.5f}, {-2.5f, 1.5f}, {2.5f, 1.5f}, {6, 1.5f},
                                                        {-6, -1.5f}, {-2.5f, -1.5f}, {2.5f, -1.5f}, {6, -1.5f},
                                                        {-6, -6}, {-2.5f, -6}, {2.5f, -6}, {6, -6}};
    public TotalManager manager;
    public GameObject enemyP;
    public Transform enemyBag;


    private float feverTime = 0.0f;
    private const float totalFeverTime = 7.0f;

    public void StartFever()
    {
        feverTime = totalFeverTime;
        for (int i = 0; i < feverPosition.GetLength(0); i++)
        {
            Vector2 pos = new Vector2(feverPosition[i, 0], feverPosition[i, 1]);
            GameObject temp = Instantiate(enemyP, pos, Quaternion.identity, enemyBag);
        }
    }
    public void SetAndCheckFever(float t) 
    { 
        feverTime -= t;
        if (feverTime < 0)
        {
            EndFever();
        }
    }


    public void EndFever(){
        foreach(Transform child in enemyBag){
            if(child.gameObject.tag == "enemyRed"){
                child.gameObject.GetComponent<EnemyRed>().DestroyEnemy();
            }
            else{
                child.gameObject.GetComponent<EnemyNormal>().DestroyEnemy();
            }
        }
    }
}
