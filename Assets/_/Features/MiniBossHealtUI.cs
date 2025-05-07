using System;
using UnityEngine;
using UnityEngine.UI;

public class MiniBossHealtUI : MonoBehaviour
{
    public Slider healthSlider;
    private Enemy miniBoss;

    public void SetMiniBoss(Enemy boss)
    {
        miniBoss = boss;
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = miniBoss.GetMaxLife();
        healthSlider.value = miniBoss.GetCurrentLife();

        miniBoss.OnDeath += HandleMiniBossDeath;

    }

    private void Update()
    {
        if (miniBoss != null)
        {
            healthSlider.value = miniBoss.GetCurrentLife();
        }
    }

    private void HandleMiniBossDeath(Enemy enemy)
    {
        healthSlider.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        miniBoss.OnDeath -= HandleMiniBossDeath;
    }
}
