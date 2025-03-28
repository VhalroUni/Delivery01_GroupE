using System;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public static Action<ControlPoint> OnEnter;
    public bool firstTime = true;

    public void OnTriggerEnter2D(Collider2D other)
    {
        OnEnter?.Invoke(this);
    }
}
