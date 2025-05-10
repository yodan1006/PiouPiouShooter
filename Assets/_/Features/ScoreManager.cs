using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public int score;
    private int _maxHightScore = 5;


    private void Update()
    {
        ScoreText.text = score.ToString();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void AddScoreBoss(int scoreToAdd)
    {
        score += scoreToAdd;
    }
    
    public void CheckAndAddHighScore(int playerScore, string playerName)
    {
        ScoreEntry[] highScores = LoadHighScores();
        
        for (int i = 0; i < _maxHightScore; i++)
        {
            if (playerScore > highScores[i].score)
            {
                for (int j = _maxHightScore - 1; j > i; j--)
                {
                    highScores[j] = highScores[j - 1];
                }
                
                highScores[i] = new ScoreEntry(playerName, playerScore);
                
                SaveHighScores(highScores);
                return;
            }
        }
    }
    
    private ScoreEntry[] LoadHighScores()
    {
        ScoreEntry[] highScores = new ScoreEntry[_maxHightScore];
        for (int i = 0; i < _maxHightScore; i++)
        {
            string name = PlayerPrefs.GetString("HighScoreName" + i, "---");
            int score = PlayerPrefs.GetInt("HighScore" + i, 0);
            highScores[i] = new ScoreEntry(name, score);
        }
        return highScores;
    }
    
    private void SaveHighScores(ScoreEntry[] highScores)
    {
        for (int i = 0; i < _maxHightScore; i++)
        {
            PlayerPrefs.SetString("HighScoreName" + i, highScores[i].playerName);
            PlayerPrefs.SetInt("HighScore" + i, highScores[i].score);
        }
        PlayerPrefs.Save();
    }
}
