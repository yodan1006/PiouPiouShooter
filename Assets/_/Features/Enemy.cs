using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    [SerializeField] private int life = 1;
    [SerializeField] private float speedMove;
    [SerializeField] private float shootInterval;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<Transform> firePoint = new List<Transform>();
    [SerializeField] private int gainScore;
    [SerializeField] private float multiplieScale;
    [SerializeField] private Color _colorForBullet;
    public event Action<Enemy> OnDeath;

    private int maxLife;
    private Vector3 movePosition;
    private bool isMoving;
    private Collider2D myCol;
    private Transform player;
    private float shootTimer;
    private ScoreManager scoreManager;
    [SerializeField] private float distanceToInteract;
    private bool shootPlayer;
    [SerializeField]private bool isDoubleShoot;
    [SerializeField] private int spreadAngle;
    [SerializeField] private int missileCount;


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
        if (!isDoubleShoot)
        {
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

            ShootAtPlayer();
        }

        if (isDoubleShoot)
        {
            if(isMoving)
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
        }
        DoubleShoot();
    }

    void DoubleShoot()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootTimer = shootInterval;
            
            Vector2 baseDir = (player.position - transform.position).normalized;
            float angleStep = spreadAngle / (missileCount - 1);
            float startAngle = -spreadAngle / 2;
                for (int i = 0; i < missileCount; i++)
                {
                    float angle = startAngle + (angleStep * i);
                    Vector2 direction = Quaternion.Euler(0, 0, angle) * baseDir;

                    ShootMissile(direction);
            }

        }
    }

    private void ShootAtPlayer()
    {

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootTimer = shootInterval;
            foreach (Transform points in firePoint)
            {
                Vector3 direction = (player.position - points.position).normalized;
                GameObject bullet = Instantiate(bulletPrefab, points.position, Quaternion.identity);
                //bullet.GetComponent<SpriteRenderer>().color = _colorForBullet;
                bullet.transform.localScale = new Vector3(bullet.transform.localScale.x, bullet.transform.localScale.y * -1, bullet.transform.localScale.z);
                bullet.transform.localScale *= multiplieScale;
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

    public void AddLife(int amount)
    {
        life += amount;
        maxLife = life;
    }

    public void AddSpeed(float amount)
    {
        speedMove += amount;
    }

    private void ShootMissile(Vector2 direction)
    {
        foreach (Transform points in firePoint)
        {
            GameObject bullet = Instantiate(bulletPrefab, points.position, Quaternion.identity);
            //bullet.GetComponent<SpriteRenderer>().color = _colorForBullet;
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x, bullet.transform.localScale.y * -1, bullet.transform.localScale.z);
            bullet.transform.localScale *= multiplieScale;
            bullet.GetComponent<ShootEnemy>().SetDirection(direction);
        }
    }

    public int GetCurrentLife()
    {
        return life;
    }

    public int GetMaxLife()
    {
        return maxLife;
    }
}
