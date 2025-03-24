using System;
using UnityEngine;

public class JumpBooster : MonoBehaviour
{
    public static Action OnBoosterTouched;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        OnBoosterTouched?.Invoke();  
    }
}
