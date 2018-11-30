using System;
using System.Collections.Generic;

[Serializable]
public class ScoreAddedResponse
{
    public bool scoreAdded;
    public bool GetIfScoreAdded()
    {
        return scoreAdded;
    }
}

[Serializable]
public class LeaderboardResponse
{
    public List<TopPlayer> leaderboard;
    public List<TopPlayer> GetTopPlayers()
    {
        return leaderboard;
    }
}

[Serializable]
public class TopPlayer
{
    public int id;
    public string name;
    public int score;
}