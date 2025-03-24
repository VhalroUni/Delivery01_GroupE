using System;
using UnityEngine;

public class ActivateDoubleJump : MonoBehaviour
{
    public static Action OnEnter;

    public void OnTriggerEnter2D(Collider2D other)
    {
        OnEnter?.Invoke();
        Destroy(gameObject);
    }
}
