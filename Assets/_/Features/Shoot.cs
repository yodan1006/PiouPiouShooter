using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] LoopingMissile missilePool;
    public int missileCount = 5;
    [SerializeField] float spreadAngle = 30f;
    [SerializeField] float missileSpeed = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) FireMissile();
    }

    public void FireMissile()
    {
        float angleStep = spreadAngle / (missileCount - 1);
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < missileCount; i++)
        {
            float angle = startAngle + (angleStep * i);
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;

            GameObject missile = missilePool.GetMissiles();
            missile.transform.position = transform.position;

            Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction.normalized * missileSpeed;
        }
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
