using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dead : MonoBehaviour
{
    int score;
    private Text m_text;
    // Start is called before the first frame update
    void Start()
    {
        score = ScoreController.Instance.GetScore();
        m_text = GetComponent<Text>();
        m_text.text = "You are dead!!!! score : " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
