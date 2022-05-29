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


    public void StartFever()
    {
        for(int i = 0; i < feverPosition.GetLength(0); i++)
        {
            Vector2 pos = new Vector2(feverPosition[i,0], feverPosition[i,1]);
            GameObject temp = Instantiate(enemyP, pos, Quaternion.identity, enemyBag);
        }
    } 
}
