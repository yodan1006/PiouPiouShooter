using System.Collections.Generic;
using UnityEngine;

public class LoopingMissile : MonoBehaviour
{
    [SerializeField] List<GameObject> missilePrefab;
    [SerializeField] int missileCount;
    public Queue<GameObject> missiles;
    public Transform m_parentTransform;

    private void Start()
    {
        GeneratPool();
    }

    private void GeneratPool()
    {
        missiles = new Queue<GameObject>();
        int randomIndex = Random.Range(0, missilePrefab.Count);
        GameObject missile = Instantiate(missilePrefab[(randomIndex)]);
        missile.SetActive(false);
        missile.transform.SetParent(m_parentTransform);
        missiles.Enqueue(missile);
    }
    
    public void ReturnToPool(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(m_parentTransform);
    }

    public GameObject GetMissiles()
    {
        if (missiles.Count > 0)
        {
            GameObject missile = missiles.Dequeue();
            missile.SetActive(true);
            return missile;
        }
        else
        {
            int index = Random.Range(0, missilePrefab.Count);
            GameObject newMissile = Instantiate(missilePrefab[(index)], m_parentTransform);
            return newMissile;
        }
    }
    
}
