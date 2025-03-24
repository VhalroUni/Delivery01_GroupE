using System;
using UnityEngine;

public class PowerJump : MonoBehaviour
{
    public static Action OnEnter;
    public static Action OnExit;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        OnEnter?.Invoke();  
    }
    public void OnTriggerExit2D(Collider2D other) 
    {
        OnExit?.Invoke();  
    }
}

