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
        ControlPoint.OnEnter += ResetHP;
    }
    private void OnDisable()
    {
        DmgCollisions.OnDamagingCollision -= TakeDamage;
        ControlPoint.OnEnter += ResetHP;
    }

    public void TakeDamage()
    {
        playerCurrentHP--;
        CheckDeathCondition();
    }

    public void ResetHP(ControlPoint ct) 
    {
        if (ct.firstTime)
        {
            playerCurrentHP = playerMaxHP;
        }
        ct.firstTime = false;
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
