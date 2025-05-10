using UnityEngine;

[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;

    public ScoreEntry(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }
}