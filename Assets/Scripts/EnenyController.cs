using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenyController : MonoBehaviour
{
    GameObject player;
    float speed;
    BoxCollider box;
    Animator animator;
    bool run;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("viking");
        speed = 0;
        run = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        animator.SetBool("run", run);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "viking")
        {
            speed = 15.15f;
            if(box.center != Vector3.zero)
            {
                box.center = Vector3.zero;
            }
            else
            {
                speed = 0;
                run = false;
            }
        }
        if(other.transform.name == "CubeER")
        {
            transform.Rotate(0, 90, 0);
        }
        if(other.transform.name == "CubeEL")
        {
            transform.Rotate(0, -90, 0);
        }

    }
}
