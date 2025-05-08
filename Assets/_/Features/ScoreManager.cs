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
    
    public void CheckAndAddHighScore(int playerScore)
    {
        int[] highScores = LoadHighScores();
        
        for (int i = 0; i < _maxHightScore; i++)
        {
            if (playerScore > highScores[i])
            {
                for (int j = _maxHightScore - 1; j > i; j--)
                {
                    highScores[j] = highScores[j - 1];
                }
                
                highScores[i] = playerScore;
                
                SaveHighScores(highScores);
                return;
            }
        }
    }
    
    private int[] LoadHighScores()
    {
        int[] highScores = new int[_maxHightScore];
        for (int i = 0; i < _maxHightScore; i++)
        {
            highScores[i] = PlayerPrefs.GetInt("HighScore" + i, 0);
        }
        return highScores;
    }
    
    private void SaveHighScores(int[] highScores)
    {
        for (int i = 0; i < _maxHightScore; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }
        PlayerPrefs.Save();
    }
}
