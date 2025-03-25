using System;
using System.Drawing;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float time;
    private Rigidbody2D rb;
    public float speed = 5f;
    public float detectionRange = 5.0f;
    private GameObject player;
    private bool inRange = false;

    public static Action<Vector2> Burning;

    private void OnEnable() 
    {
        rb = GetComponent<Rigidbody2D>();

        //Ignore Collisions with player and between Bullets
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            Collider2D myCollider = GetComponent<Collider2D>();
            if (playerCollider != null && myCollider != null)
            {
                Physics2D.IgnoreCollision(myCollider, playerCollider, true);
            }
        }

        int bulletLayer = LayerMask.NameToLayer("bullet");
        Physics2D.IgnoreLayerCollision(bulletLayer, bulletLayer);

        rb.linearVelocity = new Vector2(0, -speed);
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= detectionRange)
        {
            inRange = true;
        }
        else 
        {
            inRange = false;
        }

        time += Time.deltaTime;
        if (time >= 2f)
        {
            time = 0;
            gameObject.SetActive(false); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.linearVelocity = Vector2.zero;
    }

    void OnIronBurn() 
    {
        if (inRange && rb.linearVelocity == Vector2.zero) 
        {
            Vector2 directionBurned;
            directionBurned = transform.position - player.transform.position;

            Burning.Invoke(directionBurned);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        DrawCircle(transform.position, detectionRange, 18);
    }

    private void DrawCircle(Vector3 center, float radius, int segments)
    {
        Gizmos.color = UnityEngine.Color.red;
        float angleStep = 360f / 18;
        Vector3 previousPoint = center + new Vector3(radius, 0, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 newPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            Gizmos.DrawLine(previousPoint, newPoint);
            previousPoint = newPoint;
        }
    }
}
