using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform obstacle1, obstacle2, obstacle3, obstacle4;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 3; i++)
        {
            int a = i * 7 + 3;
            Transform p = transform.GetChild(a);
            int ran = Random.Range(1, 5);
            if(ran == 1)
            {
                Transform t = Instantiate(obstacle1);
                t.parent = p;
                t.localPosition = Vector3.zero;
            }
            if (ran == 2)
            {
                Transform t = Instantiate(obstacle2);
                t.parent = p;
                t.localPosition = Vector3.zero;
            }
            if (ran == 3)
            {
                Transform t = Instantiate(obstacle3);
                t.parent = p;
                t.localPosition = Vector3.zero;
            }
            if (ran == 4)
            {
                Transform t = Instantiate(obstacle4);
                t.parent = p;
                t.localPosition = Vector3.zero;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
