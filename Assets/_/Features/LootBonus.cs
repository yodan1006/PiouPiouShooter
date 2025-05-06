using System;
using UnityEngine;

public class LootBonus : MonoBehaviour
{
    [SerializeField]private GameObject prefabToBonus;
    private Enemy enemy;
    int conteurBonus;
    [SerializeField] private int conteurBonusMax;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.OnDeath += SpawnBonus;
        }
    }

    private void OnDestroy()
    {
        if (enemy != null) enemy.OnDeath -= SpawnBonus;
    }

    void SpawnBonus(Enemy deathEnemy)
    {
        if(conteurBonus < conteurBonusMax)
            Instantiate(prefabToBonus, deathEnemy.transform.position, Quaternion.identity);
    }
}
