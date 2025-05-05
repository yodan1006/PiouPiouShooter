using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamage
{
    [SerializeField]private int life = 1;
    public event Action<Enemy> OnDeath;
    private Vector3 movePosition;
    [SerializeField] private float speedMove;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePosition, speedMove);
    }

    public void Damage()
    {
        life -= 1;
        if (life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public void SetToMovePosition(Vector3 position)
    {
        movePosition = position;
    }
}
