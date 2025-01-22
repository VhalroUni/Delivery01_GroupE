using System;
using UnityEngine;

public class JumpBooster : MonoBehaviour
{
    public static Action<JumpBooster> OnBoosterTouched;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        OnBoosterTouched?.Invoke(this);  
    }
}
