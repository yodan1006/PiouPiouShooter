using UnityEngine;
using UnityEngine.Events;

public class Missile : MonoBehaviour
{
    public UnityEvent onEnemyHit;

    private LoopingMissile _loopingMissile;
    private Camera mainCamera;
    private Vector2 _screenBounds;
    private ScoreManager scoreManage;

    private void Start()
    {
        _loopingMissile = FindObjectOfType<LoopingMissile>();
        scoreManage = FindObjectOfType<ScoreManager>();
        mainCamera = Camera.main; // Stocker la référence à la caméra
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
            IDamage damageable = collision.gameObject.GetComponent<IDamage>();
            if (damageable != null)
            {
                damageable.Damage();
                onEnemyHit.Invoke();
                scoreManage.AddScore();
            }
        _loopingMissile.ReturnToPool(gameObject);
    }

    public void SetScoreManager(ScoreManager scoreManage1)
    {
        scoreManage = scoreManage1;
    }
}
