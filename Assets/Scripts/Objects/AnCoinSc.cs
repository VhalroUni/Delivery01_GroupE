using UnityEngine;

public class AnCoinSc : MonoBehaviour
{
    private Rigidbody rb;
    public Transform spriteTrasnform;

    private float timePassed;
    public float bounceHeight = 2f;
    public float bounceSpeed = 3.5f;
    public float flipSpeed = 5f;

    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        spriteTrasnform = GetComponent<SpriteRenderer>().transform;
        startPosition = transform.position;
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        float bounce = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = startPosition + new Vector3(0, bounce, 0);

        float scale = Mathf.Sin(Time.time * flipSpeed);
        transform.localScale = new Vector3(scale, 1, 1);

        if (timePassed >= 1.5f) 
        {
            Destroy(gameObject);    
        } 
    }
}
