using System;
using UnityEngine;

public class PlayerHit : MonoBehaviour, IDamagePlayer
{
    [SerializeField] private GameManager gameManager;
    private Shoot shootplayer;

    private void Start()
    {
        shootplayer = GetComponent<Shoot>();
    }

    public void DamagePlayer()
    {
        gameManager.life -= 1;
        if (shootplayer.missileCount > 1)
            shootplayer.missileCount = 1;
    }
}
