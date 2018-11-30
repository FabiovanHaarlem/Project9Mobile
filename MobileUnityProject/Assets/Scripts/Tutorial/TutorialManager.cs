using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_LeftArrow;
    [SerializeField]
    private GameObject m_RightArrow;

    private int m_BoardsHit;

    public void BoardHit()
    {
        m_BoardsHit++;
        if (m_BoardsHit == 3)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void MovedLeft()
    {
        m_LeftArrow.SetActive(false);
    }

    public void MovedRight()
    {
        m_RightArrow.SetActive(false);
    }

}
