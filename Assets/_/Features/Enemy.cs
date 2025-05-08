using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField]private bool isDoubleShoot;
    [SerializeField] private int spreadAngle;
    [SerializeField] private int missileCount;
    [SerializeField] private float distanceToInteract;
    public event Action<Enemy> OnDeath;

    private bool isDead;
    private int maxLife;
    private Vector3 movePosition;
    private bool isMoving;
    private Collider2D myCol;
    private Transform player;
    private float shootTimer;
    private ScoreManager scoreManager;
    private bool shootPlayer;
    private bool isDaying;
    [SerializeField] private Sprite deathSprite;
    [SerializeField] private float deathTimer;


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
        if (isDaying) return;
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
            foreach (Transform points in firePoint)
            {
                Vector3 direction = (player.position - points.position).normalized;
                GameObject bullet = Instantiate(bulletPrefab, points.position, Quaternion.identity);
                //bullet.GetComponent<SpriteRenderer>().color = _colorForBullet;
                bullet.transform.localScale = new Vector3(bullet.transform.localScale.x, bullet.transform.localScale.y * -1, bullet.transform.localScale.z);
                bullet.transform.localScale *= multiplieScale;
                bullet.GetComponent<ShootEnemy>().SetDirection(direction);
            }
            shootTimer = shootInterval;
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
        if(isDead) return;
        isDead = true;
        isDaying = true;
        myCol.enabled = false;
        OnDeath?.Invoke(this);
        StartCoroutine(HandleDeathSequence());
        //OnDeath?.Invoke(this);
        //Destroy(gameObject);
        if (LayerMask.LayerToName(gameObject.layer) == "Boss") scoreManager.AddScoreBoss(gainScore);
        if (LayerMask.LayerToName(gameObject.layer) == "Enemy") scoreManager.AddScore(gainScore);
    }

    private IEnumerator HandleDeathSequence()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector2 offScreenPos = GetRandomScreenPos();
        if (sr != null && deathSprite != null)
        {
            sr.sprite = deathSprite;
        }
        while (Vector3.Distance(transform.position, offScreenPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, offScreenPos, speedMove * 2f);
            yield return null;
        }
        
        yield return new WaitForSeconds(deathTimer);
        Destroy(gameObject);
    }

    private Vector2 GetRandomScreenPos()
    {
        float distance = 100f;
        Vector2 randompos = Random.insideUnitCircle.normalized;
        return transform.position + (Vector3)(randompos * distance);
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

    public void AddSpeedShoot(float amount)
    {
        shootInterval -= amount;
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
