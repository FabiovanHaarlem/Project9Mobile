using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]
    private Text m_ScoreText;

    [SerializeField]
    private List<GameObject> m_Princesses;

    private int m_PrincessesLeft;
    private int m_Score;
    private int m_PointsStillToAdd;

    private void Awake()
    {
        m_PointsStillToAdd = 0;
        m_PrincessesLeft = 3;
    }

    public void AddPoints()
    {
        m_PointsStillToAdd += 50;
    }

    public void LosePrincess()
    {
        m_Princesses[m_PrincessesLeft - 1].SetActive(false);
        m_PrincessesLeft -= 1;
    }

    private void Update()
    {
        if (m_PointsStillToAdd != 0)
        {
            m_Score += 1;
            m_PointsStillToAdd -= 1;
            m_ScoreText.text = "Score: " + m_Score;
        }
    }
}
