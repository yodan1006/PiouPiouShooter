using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEndGame : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI[] scoreTexts;
    [SerializeField] private GameObject nameInputPanelPrefab;
    [SerializeField] private Transform canvasTransform;

    [Header("Score Settings")]
    [SerializeField] private int maxHighScores = 5;
    
    private List<ScoreEntry> highScores = new List<ScoreEntry>();
    private const string SCORES_KEY = "HighScores";
    private int currentScore = 0;

    private void Start()
    {
        LoadHighScores();
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        currentScore += points;
    }

    public void GameOver()
    {
        if (IsHighScore(currentScore))
        {
            ShowNameInputPanel(currentScore);
        }
        currentScore = 0;
    }

    private bool IsHighScore(int score)
    {
        if (highScores.Count < maxHighScores) return true;
        return score > highScores[highScores.Count - 1].score;
    }

    private void ShowNameInputPanel(int score)
    {
        GameObject panelObj = Instantiate(nameInputPanelPrefab, canvasTransform);
        NameInputPanel panel = panelObj.GetComponent<NameInputPanel>();
        panel.Initialize(score, (name) => AddScoreWithName(name, score));
    }

    private void AddScoreWithName(string name, int score)
    {
        highScores.Add(new ScoreEntry(name, score));
        highScores.Sort((a, b) => b.score.CompareTo(a.score));
        
        if (highScores.Count > maxHighScores)
        {
            highScores.RemoveRange(maxHighScores, highScores.Count - maxHighScores);
        }
        
        SaveHighScores();
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < highScores.Count)
            {
                scoreTexts[i].text = $"{i + 1}. {highScores[i].playerName} - {highScores[i].score:N0}";
            }
            else
            {
                scoreTexts[i].text = $"{i + 1}. ---";
            }
        }
    }

    private void SaveHighScores()
    {
        SerializableHighScores serializableScores = new SerializableHighScores { scores = highScores };
        string json = JsonUtility.ToJson(serializableScores);
        PlayerPrefs.SetString(SCORES_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadHighScores()
    {
        if (PlayerPrefs.HasKey(SCORES_KEY))
        {
            string json = PlayerPrefs.GetString(SCORES_KEY);
            SerializableHighScores scores = JsonUtility.FromJson<SerializableHighScores>(json);
            highScores = scores.scores ?? new List<ScoreEntry>();
        }
    }

    public void ResetHighScores()
    {
        highScores.Clear();
        PlayerPrefs.DeleteKey(SCORES_KEY);
        UpdateScoreUI();
    }
}

[System.Serializable]
class SerializableHighScores
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}

