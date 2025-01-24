using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumping_pow;
    private bool isSprinting;
    private bool isGrounded;
    private bool isOnWall;
    private bool isSliding;

    private int doubleJump = 0;
    private Vector2 moveDir = Vector2.zero;
    
    private Rigidbody2D rigid_body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform wallCheck2;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ParticleSystem jumpParticles;
    //[SerializeField] private ParticleSystem trail;



    void Start()
    {
        rigid_body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isOnWall = false;
        isSliding = false;
        isSprinting = false;
        isGrounded = false;
        //trail.Stop();
        jumpParticles.Stop();
    }
    void FixedUpdate()
    {
        LandCollissions();
        ModifyGravity();
        WallSlide();
        UpdateAnimations();
        //ParticleManager();

        if (isSprinting && isGrounded) { rigid_body.linearVelocity = new Vector2(moveDir.x * (speed * 2f), rigid_body.linearVelocityY); }
        else { rigid_body.linearVelocity = new Vector2(moveDir.x * speed, rigid_body.linearVelocityY); }

        FaceDirection();
    }
    
    private void UpdateAnimations()
    {
        animator.SetBool("isWalking", moveDir.x != 0 && isGrounded);
        animator.SetBool("isJumping", !isGrounded);
    }

    private void FaceDirection()
    {
        if (moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
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
        moveDir = input_value;
    }

    void OnJumpStarted()
    {
        if (isGrounded == false && doubleJump <= 0)
        {
            rigid_body.linearVelocity = new Vector2(rigid_body.linearVelocity.x, jumping_pow * 0.85f);
            doubleJump += 1;
            jumpParticles.Play();
        }

        if (isGrounded)
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
    
        if (!isGrounded)
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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        isOnWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer)
        || Physics2D.OverlapCircle(wallCheck2.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            doubleJump = 0;
        }
    }



    private void WallSlide()
    {
        if (isOnWall && !isGrounded)
        {
            rigid_body.linearVelocityY = -1;
            isSliding = true;
            doubleJump = 0;
        }
        else
        {
            isSliding = false;
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

    */
    private void RestartJump(JumpBooster booster)
    {
        doubleJump = 0;
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
            doubleJump = 0;
        }
    }
    //CHEATS
}