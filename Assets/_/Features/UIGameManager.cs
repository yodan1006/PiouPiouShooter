using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    public GameManager gameManager;
    public List<GameObject> uiLife;

    private void Start()
    {
        UpdateHeart();
    }

    private void Update()
    {
        UpdateHeart();
    }

    private void UpdateHeart()
    {
        for (int i = 0; i < uiLife.Count; i++)
        {
            uiLife[i].SetActive(i < gameManager.life);
        }
    }
}
