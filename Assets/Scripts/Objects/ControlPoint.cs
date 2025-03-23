using System;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public static Action<ControlPoint> OnEnter;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("WeIn");
        OnEnter?.Invoke(this);
    }
}
