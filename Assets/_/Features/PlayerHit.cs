using UnityEngine;

public class PlayerHit : MonoBehaviour, IDamagePlayer
{
    [SerializeField] private GameManager gameManager;

    public void DamagePlayer()
    {
        gameManager.life -= 1;
    }
}
