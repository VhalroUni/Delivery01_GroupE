using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHP : MonoBehaviour
{
    private PlayerRespawn playerRespawn;
    [SerializeField] private int playerMaxHP;
    private int playerCurrentHP;

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

    private void Update()
    {
        if (playerCurrentHP <= 0)
        {
            Die();
        }
    }


    public void TakeDamage(int damageTaken)
    {
        playerCurrentHP -= damageTaken;
    }
    public void Die()
    {
        playerRespawn.Respawn();
        playerCurrentHP = playerMaxHP;
    }


}
