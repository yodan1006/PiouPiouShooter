using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    [SerializeField]private int life = 1;
    [SerializeField] private float speedMove;
    [SerializeField] private float shootInterval;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    public event Action<Enemy> OnDeath;
    
    private Vector3 movePosition;
    private bool isMoving;
    private Collider2D myCol;
    private Transform player;
    private float shootTimer;


    private void Start()
    {
        myCol = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePosition, speedMove);
            if (Vector3.Distance(transform.position, movePosition) < 0.01f)
            {
                isMoving = false;
                myCol.enabled = true;
                shootTimer = shootInterval;
            }
        }
        else
        {
            ShootAtPlayer();
        }
    }

    private void ShootAtPlayer()
    {
        if (player == null) Debug.LogError("Player object is null");
        
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootTimer = shootInterval;
            Vector3 direction = (player.position -firePoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<ShootEnemy>().SetDirection(direction);
        }
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
