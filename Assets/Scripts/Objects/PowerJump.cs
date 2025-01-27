using System;
using UnityEngine;

public class PowerJump : MonoBehaviour
{
    public static Action<PowerJump> OnEnter;
    public static Action<PowerJump> OnExit;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        OnEnter?.Invoke(this);  
    }
    public void OnTriggerExit2D(Collider2D other) 
    {
        OnExit?.Invoke(this);  
    }
}

