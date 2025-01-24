using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumping_pow;
    private bool is_sprinting;
    private bool is_grounded;
    private bool is_on_wall;
    private bool is_sliding;

    private int double_jump = 0;
    private Vector2 move_dir = Vector2.zero;
    
    private Rigidbody2D rigid_body;
    [SerializeField] private Transform ground_check;
    [SerializeField] private Transform wall_check;
    [SerializeField] private Transform wall_check2;
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private LayerMask wall_layer;
    //[SerializeField] private ParticleSystem ground_particles;
    //[SerializeField] private ParticleSystem trail;



    void Start()
    {
        rigid_body = GetComponent<Rigidbody2D>();
        is_on_wall = false;
        is_sliding = false;
        is_sprinting = false;
        is_grounded = false;
        //trail.Stop();
        //ground_particles.Stop();
    }
    void FixedUpdate()
    {
        LandCollissions();
        ModifyGravity();
        //ParticleManager();

        if (is_sprinting && is_grounded) { rigid_body.linearVelocity = new Vector2(move_dir.x * (speed * 2f), rigid_body.linearVelocityY); }
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
        is_sprinting = true;
        trail.Play();
    }

    void OnSprintOff()
    {
        is_sprinting = false;
        trail.Stop();
    }

    */
    void ModifyGravity() 
    {
    
        if (!is_grounded)
        {
            if (rigid_body.linearVelocityY> 0)
            {
            rigid_body.gravityScale = 4;
            }
            else if (rigid_body.linearVelocityY < 0)
            {
            rigid_body.gravityScale = 6;
            }
        }
        else
        {
            rigid_body.gravityScale = 2; 
        }
    }

    void LandCollissions()
    {
        is_grounded = Physics2D.OverlapCircle(ground_check.position, 0.2f, ground_layer);
        is_on_wall = Physics2D.OverlapCircle(wall_check.position, 0.2f, wall_layer)
        || Physics2D.OverlapCircle(wall_check2.position, 0.2f, wall_layer);

        if (is_grounded)
        {
            double_jump = 0;
        }
    }


    /*
    private void WallSlide()
    {
        if (is_on_wall)
        {
            rigid_body.linearVelocityY = -1;
            is_sliding = true;
            double_jump = 0;
        }
        else
        {
            is_sliding = false;
        }
    }

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

  //CHEATS
    bool cheating = false;
    private void Cheat()
    {
        if(Input.GetKeyDown("c"))
        {
            cheating = !cheating;
            Debug.Log("Cheats " + cheating);
        }
    }
    private void Update()
    {
        Cheat();
        if (cheating)
        {
            double_jump = 0;
        }
    }
    //CHEATS
}