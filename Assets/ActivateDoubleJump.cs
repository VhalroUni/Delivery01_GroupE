using System;
using UnityEngine;

public class ActivateDoubleJump : MonoBehaviour
{
    public static Action<ActivateDoubleJump> OnEnter;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("WeIn");
        OnEnter?.Invoke(this);
    }
}
