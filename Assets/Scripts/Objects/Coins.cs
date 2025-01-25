using System;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UIElements;

public class Coins : MonoBehaviour
{
    public int value;
    private Transform transform;
    private double maxHeight;
    private double minHeight;
    private double speed;
    private int dir;
    public static Action<Coins> OnCoinCollected;

    private void Awake()
    {
        if (value <= 0)
        {
            value = 5;
        }

        dir = 1;
        transform = GetComponent<Transform>();
        maxHeight = transform.position.y + 3;
        minHeight = transform.position.y - 3;
    }

    void Update() 
    {
        if (transform.position.y >= maxHeight)
        {
            dir = -1;
        }
        else if (transform.position.y <= minHeight)
        {
            dir = 1; 
        }

        transform.position = new Vector3 (transform.position.x, transform.position.y + (float)(dir * speed * Time.deltaTime), 
        transform.position.z );
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
