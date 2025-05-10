using UnityEngine;
using TMPro;

public class uiScore : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;
    private int maxHighScores = 5;       

    private void Start()
    {
        DisplayHighScores();
    }

    private void DisplayHighScores()
    {
        ScoreEntry[] highScores = LoadHighScores();

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < highScores.Length)
            {
                scoreTexts[i].text = $"#{i + 1}: {highScores[i].playerName} - {highScores[i].score}"; 
            }
            else
            {
                scoreTexts[i].text = $"#{i + 1}: --- - 0";
            }
        }
    }

    private ScoreEntry[] LoadHighScores()
    {
        ScoreEntry[] highScores = new ScoreEntry[maxHighScores];
        for (int i = 0; i < maxHighScores; i++)
        {
            string name = PlayerPrefs.GetString("HighScoreName" + i, "---");
            int score = PlayerPrefs.GetInt("HighScore" + i, 0);
            highScores[i] = new ScoreEntry(name, score);
        }
        return highScores;
    }
}
