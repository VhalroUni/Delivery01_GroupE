using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UIElements;

public class Coins : MonoBehaviour
{
    public int value;
    private Transform transform;
    private float timer;
    private int dir;
    public static Action<Coins> OnCoinCollected;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        if (value <= 0)
        {
            value = 5;
        }
        timer = 0;
        dir = 1;
    }

    void Update() 
    {
        timer += Time.deltaTime;

        if (timer >= 0.50) 
        {
             if (dir == 1)
            {
                transform.position = new Vector3 (transform.position.x, transform.position.y +0.05f, transform.position.z );
                dir = -1;
            }
            else if (dir == -1)
            {
                transform.position = new Vector3 (transform.position.x, transform.position.y -0.05f, transform.position.z );
                dir = 1; 
            }
            timer = 0;
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
