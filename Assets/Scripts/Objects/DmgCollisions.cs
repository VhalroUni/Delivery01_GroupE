using System;
using UnityEngine;

public class DmgCollisions : MonoBehaviour
{
    public delegate void DamagingCollisionDelegate();
    public static event DamagingCollisionDelegate OnDamagingCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnDamagingCollision?.Invoke();
        }
    }
}
