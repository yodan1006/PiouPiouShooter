using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public int score;


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
}
