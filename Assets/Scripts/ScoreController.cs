using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    PlayerController playerController;
    private int m_score;
    private Text m_text;
    public static ScoreController Instance;
    // Use this for initialization
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        m_text = GetComponent<Text>();
        m_score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_text.text = "score : " + m_score.ToString();
    }

    public int GetScore()
    {
        return m_score;
    }
    public void AddScore(int point)
    {
        m_score = m_score + point;
    }
}
