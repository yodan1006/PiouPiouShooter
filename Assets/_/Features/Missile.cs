using System;
using UnityEngine;
using UnityEngine.Events;

public class Missile : MonoBehaviour
{
    public UnityEvent onEnemyHit;

    private LoopingMissile _loopingMissile;
    private Camera mainCamera;
    private Vector2 _screenBounds;

    private void Start()
    {
        _loopingMissile = FindObjectOfType<LoopingMissile>();
        mainCamera = Camera.main;
        _screenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.transform.position.z));

        if (onEnemyHit == null)
        {
            onEnemyHit = new UnityEvent();
        }
    }

   

    private void Update()
    {
        if (transform.position.x > _screenBounds.x || transform.position.x < -_screenBounds.x || 
            transform.position.y > _screenBounds.y || transform.position.y < -_screenBounds.y)
        {
            _loopingMissile.ReturnToPool(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamage damageable = other.gameObject.GetComponent<IDamage>();
        if (damageable != null)
        {
            damageable.Damage();
            onEnemyHit.Invoke();
        } 
        
        _loopingMissile.ReturnToPool(gameObject);
    }
}
