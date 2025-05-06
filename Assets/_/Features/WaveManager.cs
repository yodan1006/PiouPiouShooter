using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();
    public Transform[] spawnPoints;
    public List<Transform> pointsToMove;
    public float timeForNextWave;

    private List<Enemy> activeWaveObjects;
    private int currentWave = 0;
    private bool IsSpawning = false;
    private int enemiesLifeBonus;
    private int miniBossLifeBonus;
    [SerializeField] private int bonusToLifeBonus;
    [SerializeField] private int bonusToMiniBossLifeBonus;
    [SerializeField] private Transform spawnPointMiniBoss;
    [SerializeField] private Transform movePointMiniBoss;

    private void Awake()
    {
        activeWaveObjects = new List<Enemy>();
    }

    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnWave(waves[currentWave]));
            currentWave++;
            if (currentWave % 5 == 0)
            {
                enemiesLifeBonus += bonusToLifeBonus;
                miniBossLifeBonus += bonusToMiniBossLifeBonus;
            }
            if (currentWave >= waves.Count)
                currentWave = 0;
            
            yield return new WaitForSeconds(timeForNextWave);
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        IsSpawning = true;
        
        List<Transform> availablePoints = new List<Transform>(pointsToMove);
        Shuffle(availablePoints);
        for (int i = 0; i < wave.numberOfEnemies; i++)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            GameObject enemyPrefab = wave.prefabs[Random.Range(0, wave.prefabs.Count)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.AddLife(enemiesLifeBonus);
                if (availablePoints.Count > 0)
                {
                    Transform pointToAssign = availablePoints[0];
                    enemyScript.SetToMovePosition(pointToAssign.position);
                    availablePoints.RemoveAt(0);
                }

                activeWaveObjects.Add(enemyScript);
                enemyScript.OnDeath += OnEnemyDeath;
            }
            
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        if (wave.spawnMiniBoss)
        {
            GameObject miniBoss = Instantiate(wave.prefabsMiniBoss, spawnPointMiniBoss);
            Enemy miniBossScript = miniBoss.GetComponent<Enemy>();
            miniBossScript.AddLife(miniBossLifeBonus);
            miniBossScript.SetToMovePosition(movePointMiniBoss.position);
            activeWaveObjects.Add(miniBossScript);
            miniBossScript.OnDeath += OnEnemyDeath;
        }
        
        IsSpawning = false;
        
        yield return new WaitUntil(() => activeWaveObjects.Count == 0);
    }

    void OnEnemyDeath(Enemy ennemy)
    {
        activeWaveObjects.Remove(ennemy);
    }

    private void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1 );
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
