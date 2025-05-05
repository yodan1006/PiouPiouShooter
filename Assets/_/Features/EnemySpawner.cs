using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float enemySpeed = 2f;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> _enemyPool;

    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] public List<Transform> _moveToPoint;
    [SerializeField] private Transform parentTransform;


    private void Start()
    {
        GeneratEnemy();
    }
    
    void GeneratEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject enemy = Instantiate(enemyPrefabs[randomIndex]);
        enemy.SetActive(false);
        enemy.transform.SetParent(parentTransform);
        _enemyPool.Add(enemy);
    }
    
    public void ReturnPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemy.transform.SetParent(parentTransform);
    }
}
