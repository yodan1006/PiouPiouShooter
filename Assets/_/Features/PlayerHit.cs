using System;
using System.Collections;
using UnityEngine;

public class PlayerHit : MonoBehaviour, IDamagePlayer
{
    [SerializeField] private GameManager gameManager;
    private Shoot shootplayer;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;
    [SerializeField] private float timeInvincibilité;
    
    [Header("Sprites")]
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite hitSprite;
    
    private bool _isInvincible;

    private void Start()
    {
        shootplayer = GetComponent<Shoot>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    public void DamagePlayer()
    {
        gameManager.life -= 1;
        if (shootplayer.missileCount > 1)
            shootplayer.missileCount = 1;

        StartCoroutine(InvincibilityCoroutine());
    }

    private IEnumerator InvincibilityCoroutine()
    {
        _isInvincible = true;
        //shootplayer.enabled = false;
        collider2D.enabled = false;

        float blinkDuration = timeInvincibilité;
        float blinkInterval = 0.2f;
        float timer = 0f;
        
        spriteRenderer.sprite = hitSprite;

        while (timer < blinkDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }
        
        spriteRenderer.sprite = normalSprite;
        spriteRenderer.enabled = true;
        
        //shootplayer.enabled = true;
        _isInvincible = false;
        collider2D.enabled = true;
    }
}
