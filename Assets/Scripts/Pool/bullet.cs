using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float time;
    private Rigidbody2D rb;
    public float speed = 5f;

    private void OnEnable() 
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            Collider2D myCollider = GetComponent<Collider2D>();
            if (playerCollider != null && myCollider != null)
            {
                Physics2D.IgnoreCollision(myCollider, playerCollider, true);
            }
        }

        // Reiniciar tiempo y aplicar velocidad
        time = 0;
        rb.linearVelocity = new Vector2(0, -speed);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 2f)
        {
            gameObject.SetActive(false); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.linearVelocity = Vector2.zero;
    }
}
