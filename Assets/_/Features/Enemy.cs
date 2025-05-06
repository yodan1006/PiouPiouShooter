using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    [SerializeField]private int life = 1;
    [SerializeField] private float speedMove;
    [SerializeField] private float shootInterval;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<Transform> firePoint =  new List<Transform>();
    [SerializeField] private int gainScore;
    public event Action<Enemy> OnDeath;
    
    private Vector3 movePosition;
    private bool isMoving;
    private Collider2D myCol;
    private Transform player;
    private float shootTimer;
    private ScoreManager scoreManager;
    [SerializeField] private float distanceToInteract;
    private bool shootPlayer;


    private void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
        myCol = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isMoving = true;
        myCol.enabled = false;
    }

    private void Update()
    {
        ShootAtPlayer();
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePosition, speedMove);
            if (Vector3.Distance(transform.position, movePosition) < distanceToInteract)
            {
                isMoving = false;
                shootPlayer = true;
                myCol.enabled = true;
                shootTimer = shootInterval;
            }
        }
        // if(shootPlayer)
        // {
        //     ShootAtPlayer();
        // }
    }

    private void ShootAtPlayer()
    {
        
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootTimer = shootInterval;
            foreach (Transform points in firePoint)
            {
                Vector3 direction = (player.position -points.position).normalized;
                            GameObject bullet = Instantiate(bulletPrefab, points.position, Quaternion.identity);
                            bullet.GetComponent<ShootEnemy>().SetDirection(direction);
            }
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
        if (LayerMask.LayerToName(gameObject.layer) == "Boss") scoreManager.AddScoreBoss();
        if (LayerMask.LayerToName(gameObject.layer) == "Enemy") scoreManager.AddScore();
    }

    public void SetToMovePosition(Vector3 position)
    {
        movePosition = position;
    }
}
