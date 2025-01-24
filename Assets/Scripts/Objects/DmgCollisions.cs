using System;
using UnityEngine;

public class DmgCollisions : MonoBehaviour
{
    [SerializeField] private int damage;
    public static Action<int> OnDamagingCollision;

    private void Awake()
    {
        if (damage <= 0)
        {
            damage = 1; //if no value is specified, damage will be 1 by default
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnDamagingCollision?.Invoke(damage);
        }
    }
}
