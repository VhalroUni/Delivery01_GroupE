using System;
using UnityEngine;

public class PowerJump : MonoBehaviour
{
    public static Action<PowerJump> OnContact;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        OnContact?.Invoke(this);  
    }
}

