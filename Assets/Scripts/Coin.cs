using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float turnspeed;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(turnspeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "viking")
        {
            Destroy(gameObject);
            ScoreController.Instance.AddScore(1);
        }
    }
}
