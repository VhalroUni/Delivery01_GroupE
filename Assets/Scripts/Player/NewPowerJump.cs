using System;
using UnityEngine;

public class NewPowerJump : MonoBehaviour
{
    public static Action<NewPowerJump> OnEnter;

    public void OnTriggerEnter2D(Collider2D other)
    {
        OnEnter?.Invoke(this);

        Destroy(gameObject);
    }
}
