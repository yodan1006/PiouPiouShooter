using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    private int score;
    public int ScoreAdd;
    public int ScoreAddBoss;


    private void Update()
    {
        ScoreText.text = score.ToString();
    }

    public void AddScore()
    {
        score += ScoreAdd;
    }

    public void AddScoreBoss()
    {
        score += ScoreAddBoss;
    }
}
