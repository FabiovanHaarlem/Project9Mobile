using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEnrty : MonoBehaviour
{
    [SerializeField]
    private Text m_PlayerName;
    [SerializeField]
    private Text m_PlayerScore;

    public void SetData(string name, int score)
    {
        m_PlayerName.text = name;
        m_PlayerScore.text = score.ToString();
    }
}
