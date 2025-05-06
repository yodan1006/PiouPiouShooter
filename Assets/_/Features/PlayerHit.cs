using UnityEngine;

public class PlayerHit : MonoBehaviour, IDamagePlayer
{
    [SerializeField] private GameManager gameManager;
    private Shoot shootplayer;

    public void DamagePlayer()
    {
        gameManager.life -= 1;
        shootplayer.missileCount = 1;
    }
}
