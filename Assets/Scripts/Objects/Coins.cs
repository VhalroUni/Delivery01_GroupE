using System;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private float timer;
    public int Value;
    public static Action<Coins> OnCoinCollected;

    private void Awake()
    {
        if (Value <= 0)
        {
            Value = 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnCoinCollected?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
