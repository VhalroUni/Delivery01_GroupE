using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{

    private float speed = 8f;
    public float jumping_pow;
    private bool sprinting = false;
    private bool is_grounded = false;
    private int double_jump = 0;
    private Vector2 move_dir = Vector2.zero;

    [SerializeField] private Rigidbody2D rigid_body;
    [SerializeField] private Transform ground_check;
    [SerializeField] private Transform wall_check;
    [SerializeField] private Transform wall_check2;
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private LayerMask wall_layer;
    //[SerializeField] private ParticleSystem ground_particles;
    //[SerializeField] private ParticleSystem trail;



    void Start()
    {
        //trail.Stop();
        //ground_particles.Stop();
    }
    void FixedUpdate()
    {
        LandCollissions();
        //ParticleManager();

        if (sprinting && is_grounded) { rigid_body.linearVelocity = new Vector2(move_dir.x * (speed * 2f), rigid_body.linearVelocityY); }
        else { rigid_body.linearVelocity = new Vector2(move_dir.x * speed, rigid_body.linearVelocityY); }
    }


     void OnEnable()
    {
        JumpBooster.OnBoosterTouched += RestartJump;
    }
    void OnDisable()
    {
        JumpBooster.OnBoosterTouched -= RestartJump;
    }

    void OnMove(InputValue value)
    {
        var input_value = value.Get<Vector2>();
        move_dir = input_value;
    }

    void OnJumpStarted()
    {
        if (is_grounded == false && double_jump <= 0)
        {
            rigid_body.linearVelocity = new Vector2(rigid_body.linearVelocity.x, jumping_pow * 0.85f);
            double_jump += 1;
        }

        if (is_grounded)
        {
            rigid_body.linearVelocity = new Vector2(rigid_body.linearVelocity.x, jumping_pow);
        }
    }


   /*  void OnSprint()
    {
        sprinting = true;
        trail.Play();
    }

    void OnSprintOff()
    {
        sprinting = false;
        trail.Stop();
    }

    */

    void LandCollissions()
    {
        is_grounded = Physics2D.OverlapCircle(ground_check.position, 0.2f, ground_layer);
        if (is_grounded)
        {
            double_jump = 0;
        }
    }


    /*
    //Para evitar que el trail siga activado despues de sprintar en el suelo
    void ParticleManager()
    {
        if (!is_grounded)
        {
            trail.Stop();
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & ground_layer) != 0)
        {
            if (ground_particles != null)
            {
                Vector2 collisionPoint = collision.contacts[0].point;

                Vector2 offset = new Vector3(0.5f, 0);
                ground_particles.transform.position = collisionPoint + offset;

                ground_particles.Play();
            }
        }

    }

    */
    private void RestartJump(JumpBooster booster)
    {
        double_jump = 0;
    }
}