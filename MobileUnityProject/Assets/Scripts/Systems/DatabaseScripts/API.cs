using System.Collections;
using UnityEngine;

public class API : MonoBehaviour
{
    private const string m_Link = "http://fabiovanhaarlem.eu/databasescripts/drekon/api.php";

    public void SendScoreToDatabase(string name, int score, AddScoreToLeaderboardMenu sender)
    {
        StartCoroutine(IESendScoreToDatabase(name, score, sender));
    }

    public void GetLeaderboardData(LoadLeaderboard sender)
    {
        StartCoroutine(IEGetLeaderboardData(sender));
    }

    public IEnumerator IESendScoreToDatabase(string name, int score, AddScoreToLeaderboardMenu sender)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "addScore");
        form.AddField("name", name);
        form.AddField("score", score);
        WWW www = new WWW(m_Link, form);
        yield return www;
        ScoreAddedResponse response = JsonUtility.FromJson<ScoreAddedResponse>(www.text);
        sender.ReactToServerResponse(response);
    }

    public IEnumerator IEGetLeaderboardData(LoadLeaderboard sender)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "getScore");
        WWW www = new WWW(m_Link, form);
        yield return www;
        LeaderboardResponse response = JsonUtility.FromJson<LeaderboardResponse>(www.text);
        sender.SetData(response);
    }
}
