using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Transform p1, p2, p3;
    Rigidbody rb;
    GameObject road1, road2, road3, Right, Left, enemy;
    Vector3 currentDir, lastDir;
    public float speed, jumpForce, movingSpeed, RotateSpeed;
    bool run, jump, dead, startTimer;
    public bool start;
    int dir, time, check, counter;
    public int x;
    public Transform Coin1, Coin2, Coin3;
    Animator animator;
    GameObject[] children1 = new GameObject[24];
    GameObject[] children2 = new GameObject[24];
    GameObject[] children3 = new GameObject[24];
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        road1 = GameObject.Find("Road1");
        road2 = GameObject.Find("Road2");
        road3 = GameObject.Find("Road3");
        Right = GameObject.Find("Right");
        Left = GameObject.Find("Left");
        enemy = GameObject.Find("Enemy");
        enemy.SetActive(false);
        run = false;
        jump = false;
        dead = false;
        startTimer = true;
        dir = 1;
        time = 0;
        counter = 0;
        currentDir = Vector3.right;
        lastDir = Vector3.forward;
        check = 0;

        for(int i = 0; i < 24; i++)
        {
            children1[i] = road1.transform.GetChild(i).gameObject;
            children2[i] = road2.transform.GetChild(i).gameObject;
            children3[i] = road3.transform.GetChild(i).gameObject;
        }

        int ran1 = Random.Range(5, 18);
        for(int i = 0; i < 5; i++)
        {
            p1 = children1[ran1 + i].transform;
            CoinSpawn(p1);
        }
        
        int ran2 = Random.Range(1, 18);
        for (int i = 0; i < 5; i++)
        {
            p2 = children2[ran2 + i].transform;
            CoinSpawn(p2);
        }
        
        int ran3 = Random.Range(1, 18);
        for (int i = 0; i < 5; i++)
        {
            p3 = children3[ran3 + i].transform;
            CoinSpawn(p3);
        }

    }

    void CoinSpawn(Transform p)
    {
        int ran = Random.Range(1, 4);
        if (ran == 1)
        {
            Transform t = Instantiate(Coin1);
            t.parent = p;
            t.localPosition = Vector3.zero;
        }
        if (ran == 2)
        {
            Transform t = Instantiate(Coin2);
            t.parent = p;
            t.localPosition = Vector3.zero;
        }
        if (ran == 3)
        {
            Transform t = Instantiate(Coin3);
            t.parent = p;
            t.localPosition = Vector3.zero;
        }
    }

    void ScoreCount()
    {
        if (!dead)
        {
            ScoreController.Instance.AddScore(1);
        }
        else
        {
            CancelInvoke("ScoreCount");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {

            if (run)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            if (!dead)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    time = 0;
                    InvokeRepeating("RotateR", 0, 0.01f);
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    time = 0;
                    InvokeRepeating("RotateL", 0, 0.01f);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (lastDir == Vector3.forward)
                    {
                        transform.localPosition += movingSpeed * Time.deltaTime * Vector3.right;
                    }
                    if (lastDir == Vector3.right)
                    {
                        transform.localPosition += movingSpeed * Time.deltaTime * Vector3.back;
                    }
                    if (lastDir == Vector3.left)
                    {
                        transform.localPosition += movingSpeed * Time.deltaTime * Vector3.forward;
                    }
                    if (lastDir == Vector3.back)
                    {
                        transform.localPosition += movingSpeed * Time.deltaTime * Vector3.left;
                    }
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (lastDir == Vector3.forward)
                    {
                        transform.localPosition += movingSpeed * Time.deltaTime * Vector3.left;
                    }
                    if (lastDir == Vector3.right)
                    {
                        transform.localPosition += movingSpeed * Time.deltaTime * Vector3.forward;
                    }
                    if (lastDir == Vector3.left)
                    {
                        transform.localPosition += movingSpeed * Time.deltaTime * Vector3.back;
                    }
                    if (lastDir == Vector3.back)
                    {
                        transform.localPosition += movingSpeed * Time.deltaTime * Vector3.right;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }
            if (!IsGround())
            {
                jump = true;
            }
            else
            {
                jump = false;
            }

            if (transform.position.y < -5)
            {
                run = false;
                dead = true;
            }
        }
        else
        {
            run = false;
            if (Input.GetKey(KeyCode.D))
            {
                run = true;
                this.transform.Rotate(Vector3.up * Time.deltaTime * RotateSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                run = true;
                this.transform.Rotate(Vector3.up * Time.deltaTime * -RotateSpeed);
            }
            if (Input.GetKey(KeyCode.W))
            {
                run = true;
                transform.Translate(Vector3.forward * Time.deltaTime * 5);
            }
            if (Input.GetKey(KeyCode.S))
            {
                run = true;
                transform.Translate(Vector3.back * Time.deltaTime * 5);
            }
            if (transform.position.y < -5)
            {
                run = false;
                dead = true;
            }

        }

        if (dead)
        {
            if (startTimer)
            {
                StartCoroutine("timer");
                startTimer = false;
            }
        }

        animator.SetBool("Run", run);
        animator.SetBool("Jump", jump);
        animator.SetBool("Dead", dead);
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(2);
        counter++;
        if(counter == 1)
        {
            startTimer = true;
        }
        if(counter > 1)
        {
            SceneManager.LoadScene(3);
        }
    }

    void RotateR()
    {
        time++;
        transform.Rotate(0, 9, 0);
        if(time == 10)
        {
            CancelInvoke("RotateR");
        }
    }

    void RotateL()
    {
        time++;
        transform.Rotate(0, -9, 0);
        if(time == 10)
        {
            CancelInvoke("RotateL");
        }
    }

    void RoadTurn(int lr, GameObject road)
    {
        if(road.transform.name == "Right")
        {
            if(lr == 0)
            {
                road.transform.localPosition = new Vector3(0, -100, 0);
            }
            else if (lr == 1)
            {
                if (lastDir == Vector3.right)
                {
                    road.transform.localEulerAngles = new Vector3(0, 90, 0);
                }
                if (lastDir == Vector3.left)
                {
                    road.transform.localEulerAngles = new Vector3(0, -90, 0);
                }
                if (lastDir == Vector3.forward)
                {
                    road.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                if (lastDir == Vector3.back)
                {
                    road.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
                road.transform.localPosition = road1.transform.localPosition + 150 *lastDir;
            }
            else if (lr == 2)
            {
                road.transform.localPosition = new Vector3(0, -100, 0);
            }
        }
        else if (road.transform.name == "Left")
        {
            if (lr == 0)
            {
                road.transform.localPosition = new Vector3(0, -100, 0);
            }
            else if (lr == 2)
            {
                if (lastDir == Vector3.right)
                {
                    road.transform.localEulerAngles = new Vector3(0, 90, 0);
                }
                if (lastDir == Vector3.left)
                {
                    road.transform.localEulerAngles = new Vector3(0, -90, 0);
                }
                if(lastDir == Vector3.forward)
                {
                    road.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                if(lastDir == Vector3.back)
                {
                    road.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
                road.transform.localPosition = road1.transform.localPosition + 150 * lastDir;
            }
            else if (lr == 1)
            {
                road.transform.localPosition = new Vector3(0, -100, 0);
            }
        }
        else
        {
            if (lr == 0)
            {
                road.transform.localPosition += 132 * currentDir;
            }
            else
            {
                if (lr == 1)
                {
                    road.transform.Rotate(0, 90, 0);
                }
                if (lr == 2)
                {
                    road.transform.Rotate(0, -90, 0);
                }
                if (road.transform.name == "Road1")
                {
                    road.transform.localPosition += lastDir * 150;
                    if(lr == 1)
                    {
                        road.transform.localPosition += currentDir * 19;
                    }
                    else
                    {
                        road.transform.localPosition += currentDir * 19;
                    }
                }
                else if(road.transform.name == "Road2")
                {
                    road.transform.localPosition = road1.transform.localPosition;
                    road.transform.localPosition += 44 * currentDir;
                }
                else if (road.transform.name == "Road3")
                {
                    road.transform.localPosition = road1.transform.localPosition;
                    road.transform.localPosition += 88 * currentDir;
                }
            }
        }
    }

    bool IsGround()
    {
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGround = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f);
        return isGround;
    }

    void Jump()
    {
        if (IsGround())
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "rock_02")
        {
            run = false;
            dead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "CubeW")
        {
            InvokeRepeating("ScoreCount", 0, 1f);
            transform.localPosition = new Vector3(0, 1, -3);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            start = true;
            run = true;
            enemy.SetActive(true);
        }
        if(other.transform.name == "Enemy")
        {
            if(check > 0)
            {
                run = false;
                dead = true;
            }
            check++;
        }
        int index;
        if(other.transform.name == "CubeR")
        {
            RoadTurn(dir, Right);
        }
        if(other.transform.name == "CubeL")
        {
            RoadTurn(dir, Left);
        }
        if(other.transform.name == "Cube1")
        {
            for(int i = 0; i < 24; i++)
            {
                children1[i].SetActive(true);
            }
            RoadTurn(dir, road1);
            for (int i = 0; i < 24; i++)
            {
                int a = children1[i].transform.childCount;
                for(int j = 0; j < a; j++)
                {
                    GameObject child = children1[i].transform.GetChild(j).gameObject;
                    if(child.transform.name == "Coin1(Clone)" || child.transform.name == "Coin2(Clone)" || child.transform.name == "Coin3(Clone)")
                    {
                        Destroy(child);
                    }
                }
            }
            int ran1 = Random.Range(0, 18);
            for (int i = 0; i < 5; i++)
            {
                p1 = children1[ran1 + i].transform;
                CoinSpawn(p1);
            }
            index = Random.Range(0, 30);
            if(index <= 16)
            {
                for (int i = 0; i < 5; i++)
                {
                    children1[index + i].SetActive(false);
                }
            }
        }

        if (other.transform.name == "Cube2")
        {
            for (int i = 0; i < 24; i++)
            {
                children2[i].SetActive(true);
            }
            RoadTurn(dir, road2);
            for (int i = 0; i < 24; i++)
            {
                int a = children2[i].transform.childCount;
                for (int j = 0; j < a; j++)
                {
                    GameObject child = children2[i].transform.GetChild(j).gameObject;
                    if (child.transform.name == "Coin1(Clone)" || child.transform.name == "Coin2(Clone)" || child.transform.name == "Coin3(Clone)")
                    {
                        Destroy(child);
                    }
                }
            }
            int ran2 = Random.Range(0, 18);
            for (int i = 0; i < 5; i++)
            {
                p2 = children2[ran2 + i].transform;
                CoinSpawn(p2);
            }

            
            index = Random.Range(0, 30);
            if (index <= 16)
            {
                for (int i = 0; i < 5; i++)
                {
                    children2[index + i].SetActive(false);
                }
            }
        }

        if (other.transform.name == "Cube3")
        {
            for (int i = 0; i < 24; i++)
            {
                children3[i].SetActive(true);
            }
            
            index = Random.Range(0, 30);
            if (index <= 16)
            {
                for (int i = 0; i < 5; i++)
                {
                    children3[index + i].SetActive(false);
                }
            }
            int last = 0;
            RoadTurn(dir, road3);
            for (int i = 0; i < 24; i++)
            {
                int a = children3[i].transform.childCount;
                for (int j = 0; j < a; j++)
                {
                    GameObject child = children3[i].transform.GetChild(j).gameObject;
                    if (child.transform.name == "Coin1(Clone)" || child.transform.name == "Coin2(Clone)" || child.transform.name == "Coin3(Clone)")
                    {
                        Destroy(child);
                    }
                }
            }
            int ran3 = Random.Range(1, 18);
            for (int i = 0; i < 5; i++)
            {
                p3 = children3[ran3 + i].transform;
                CoinSpawn(p3);
            }
            last = dir;
            dir = Random.Range(0, 3);
            lastDir = currentDir;

            if(dir > 2)
            {
                dir = 0;
            }
            else if(dir == 1)
            {
                currentDir = Quaternion.AngleAxis(90, Vector3.up) * currentDir;
            }
            else if(dir == 2)
            {
                currentDir = Quaternion.AngleAxis(-90, Vector3.up) * currentDir;
            }

            if (last == 0)
            {
                RoadTurn(dir, Left);
                RoadTurn(dir, Right);
            }
            else if (last == 1)
            {
                RoadTurn(dir, Left);
            }
            else if (last == 2)
            {
                RoadTurn(dir, Right);
            }
        }

    }
}
