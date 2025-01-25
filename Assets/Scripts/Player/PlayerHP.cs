using System;
using UnityEngine;


public class PlayerHP : MonoBehaviour
{
    private PlayerRespawn playerRespawn;
    [SerializeField] private int playerMaxHP;

    public delegate void DeathDelegate();
    public static DeathDelegate OnDeath;

    public int playerCurrentHP { get; private set; }

    private void Awake()
    {
        playerRespawn = GetComponent<PlayerRespawn>();
        playerCurrentHP = playerMaxHP;
    }

    private void OnEnable()
    {
        DmgCollisions.OnDamagingCollision += TakeDamage;
    }
    private void OnDisable()
    {
        DmgCollisions.OnDamagingCollision -= TakeDamage;
    }

    public void TakeDamage()
    {
        playerCurrentHP--;
        CheckDeathCondition();
    }
    public void CheckDeathCondition()
    {
        if (playerCurrentHP <= 0)
        {
            OnDeath?.Invoke();
        }
        else
        {
            playerRespawn.Respawn();
        }
    }
}
