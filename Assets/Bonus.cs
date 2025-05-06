using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bonus : MonoBehaviour
{
    public Shoot shootplayer;
    private Vector2 direction;
    private Camera cam;
    private float minX, maxX, minY, maxY;
    [SerializeField] private float speedMove;
    [SerializeField] private int numberOfAddMissile;
    [SerializeField] private float timeToDestroy;

    private void Start()
    {
        shootplayer = FindFirstObjectByType<Shoot>();
        cam = Camera.main;
        
        Vector2 bottomLeft = cam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topright = cam.ViewportToWorldPoint(new Vector2(1, 1));
        
        minX = bottomLeft.x;
        maxX = topright.x;
        minY = bottomLeft.y;
        maxY = topright.y;
        
        direction = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)Destroy(gameObject);
        transform.Translate(direction * speedMove * Time.deltaTime);
        
        Vector2 pos = transform.position;
        
        if (pos.x <= minX || pos.x >= maxX)
            direction.x = -direction.x;
        if (pos.y <= minY || pos.y >= maxY)
            direction.y = -direction.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        shootplayer.missileCount += numberOfAddMissile;
        Destroy(gameObject);
    }
}
