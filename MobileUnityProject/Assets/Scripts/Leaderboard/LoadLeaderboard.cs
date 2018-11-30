using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLeaderboard : MonoBehaviour
{
    private API m_API;
    [SerializeField]
    private List<LeaderboardEnrty> m_Leaderboard;
    [SerializeField]
    private GameObject m_LoadingIcon;

    private void Awake()
    {
        m_LoadingIcon.SetActive(true);
        for (int i = 0; i < m_Leaderboard.Count; i++)
        {
            m_Leaderboard[i].gameObject.SetActive(false);
        }

        m_API = GetComponent<API>();
        GetLeaderboard();
    }

    private void GetLeaderboard()
    {
        m_API.GetLeaderboardData(this);
    }

    public void SetData(LeaderboardResponse leaderboard)
    {
        for (int i = 0; i < m_Leaderboard.Count; i++)
        {
            m_Leaderboard[i].gameObject.SetActive(true);
            m_Leaderboard[i].SetData((i + 1) + ". " + leaderboard.leaderboard[i].name, leaderboard.leaderboard[i].score);
        }
        m_LoadingIcon.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GoToMainMenu();
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
    }
}
