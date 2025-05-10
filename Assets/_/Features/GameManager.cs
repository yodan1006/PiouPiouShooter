using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int ajoutBonusMaxLife = 1000;
    public int maxLife = 3;
    public int life;
    public SceneManager sceneManager;
    public ScoreManager scoreManager;
    [SerializeField] private List<int> bonusScores = new List<int>();
    private HashSet<int> bonusScoresSet = new HashSet<int>();

    public GameObject nameInputPanel;
    public TMP_InputField nameInputField;
    public string defaultName = "Player";

    private void Start()
    {
        scoreManager = scoreManager.GetComponent<ScoreManager>();
        if (nameInputPanel != null)
        {
            nameInputPanel.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (life <= 0) EndGame();
    }

    private void Update()
    {
        BonusToScore();
    }


    void BonusToScore()
    {
        int scoreActuelle;
        scoreActuelle = scoreManager.score;
        foreach (var palier in bonusScores)
        {
            if (scoreActuelle >= palier && !bonusScoresSet.Contains(palier))
            {
                bonusScoresSet.Add(palier);
                GiveBonusForScore(palier);
            }
        }
    }

    public void GiveBonusForScore(int palier)
    {
        if (life < maxLife) life++;

        else scoreManager.score = scoreManager.score + ajoutBonusMaxLife;
    }

    public void EndGame()
    {
        if (nameInputPanel != null)
        {
            Time.timeScale = 0;
            nameInputPanel.SetActive(true);
        }
        else
        {
            SaveScoreAndContinue(defaultName);
        }
    }

    public void SaveScoreAndContinue(string name = "")
    {
        if (string.IsNullOrEmpty(name))
        {
            name = nameInputField != null ? nameInputField.text : defaultName;
            if (string.IsNullOrEmpty(name)) name = defaultName;
        }

        Time.timeScale = 1;
        scoreManager.CheckAndAddHighScore(scoreManager.score, name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

