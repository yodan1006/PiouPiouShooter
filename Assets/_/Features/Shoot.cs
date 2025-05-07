using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] LoopingMissile missilePool;
    public int missileCount = 5;
    [SerializeField] float spreadAngle = 30f;
    [SerializeField] float missileSpeed = 10f;
    [SerializeField] private float timeToShoot = 1f;
    private float shootCooldown = 0f;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            shootCooldown -= Time.deltaTime;
            if (shootCooldown <= 0f)
            {
                FireMissile();
                shootCooldown = timeToShoot;
            }
        }
        else shootCooldown = 0;
    }

    public void FireMissile()
    {
        float angleStep = spreadAngle / (missileCount - 1);
        float startAngle = -spreadAngle / 2;

        if (missileCount == 1)
        {
            Vector2 direction =  Vector2.up;
            ShootMissile(direction);
        }

        if (missileCount > 1)
        {
            for (int i = 0; i < missileCount; i++)
                    {
                        float angle = startAngle + (angleStep * i);
                        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
            
                        ShootMissile(direction);
                    }
        }
        
    }

    private void ShootMissile(Vector2 direction)
    {
        GameObject missile = missilePool.GetMissiles();
        missile.transform.position = transform.position;

        Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction.normalized * missileSpeed;
    }

    public void IncreaseMissileCount(int amount)
    {
        missileCount += amount;
    }

    public void IncreaseMissileSpeed(float speed)
    {
        missileSpeed *= speed;
    }
}
