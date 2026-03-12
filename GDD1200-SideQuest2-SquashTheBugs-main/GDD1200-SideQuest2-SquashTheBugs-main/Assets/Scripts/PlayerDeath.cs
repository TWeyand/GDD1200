using System;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public delegate void KillPlayer();
    public static event KillPlayer OnKillPlayer;
    
    public delegate void RevivePlayer();
    public static event RevivePlayer OnRevivePlayer;

    [SerializeField] private ParticleSystem bloodVFX;

    private bool isDead;
    public static float deathTimer = 3.0f;
    private float deathTime = 3.0f

    private void Update()
    {
        if (isDead)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
            {
                ToggleDeathVFX();
                OnRevivePlayer?.Invoke();
                isDead = false;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vehicle"))
        {
            OnKillPlayer?.Invoke();
            ToggleDeathVFX();
            isDead = true;
            deathTimer = deathTime;
        }
    }

    private void ToggleDeathVFX()
    {
        if (bloodVFX.gameObject.activeSelf)
        {
            bloodVFX.gameObject.SetActive(false);
            return;
        }
        
        bloodVFX.gameObject.SetActive(true);
        bloodVFX.Play();
    }
}
