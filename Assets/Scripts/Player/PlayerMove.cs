using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumping_pow;
    private bool isGrounded;
    private bool isOnWall;
    private int doubleJump = 0;
    private bool powerJump = false;
    private Vector2 moveDir = Vector2.zero;
    bool cheating = false;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform wallCheck2;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ParticleSystem jumpParticles;
    [SerializeField] private AudioClip jumpSound;

    private GameObject currentPowerUp; // Variable para guardar el power-up recogido

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isOnWall = false;
        isGrounded = false;
        jumpParticles.Stop();
    }
    void FixedUpdate()
    {
        LandCollissions();
        ModifyGravity();
        WallSlide();
        ChangeJumpPow();
        UpdateAnimations();

        rigidBody.linearVelocity = new Vector2(moveDir.x * speed, rigidBody.linearVelocityY); 

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
        PowerJump.OnEnter += MegaJump;
        PowerJump.OnExit += NoMegaJump;
        NewPowerJump.OnEnter += PowerUp;
    }
    void OnDisable()
    {
        JumpBooster.OnBoosterTouched -= RestartJump;
        PowerJump.OnEnter -= MegaJump;
        PowerJump.OnExit -= NoMegaJump;
        NewPowerJump.OnEnter -= PowerUp;
    }

    void OnMove(InputValue value)
    {
        var input_value = value.Get<Vector2>();
        moveDir = input_value;
    }

    void OnJumpStarted()
    {
        if ((!isGrounded) && (doubleJump <= 0)) //Double jumping
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumping_pow * 0.85f);

            doubleJump += 1;
            jumpParticles.Play();
            ControlSound.instance.RunSound(jumpSound);
        }

        if (isGrounded) //Regular jump
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumping_pow);
            ControlSound.instance.RunSound(jumpSound);
        }

        StopMegaJump();
    }

    void ModifyGravity()
    {
        if (!isGrounded)
        {
            if (rigidBody.linearVelocityY > 0)
            {
                rigidBody.gravityScale = 4;
            }
            else if (rigidBody.linearVelocityY < 0)
            {
                rigidBody.gravityScale = 6;
            }
        }
        else
        {
            rigidBody.gravityScale = 2;
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
        if (isOnWall && !isGrounded && rigidBody.linearVelocityY < 0)
        {
            rigidBody.linearVelocityY = -1;
        }
    }

    private void RestartJump(JumpBooster booster)
    {
        doubleJump = 0;
    }

    private void PowerUp(NewPowerJump powerUp) 
    {
        powerJump = true;
    }
    private void MegaJump(PowerJump booster) 
    {
        powerJump = true;
    }
    private void NoMegaJump(PowerJump booster) 
    {
        StopMegaJump();
    }

    private void StopMegaJump() 
    {
        powerJump = false;
    }

    private void ChangeJumpPow() 
    {
        if (powerJump) 
        {
            jumping_pow = 30;
        }
        else 
        {
            jumping_pow = 15.5f;
        }
    }

    //CHEATS
    private void Cheat()
    {
        if (Input.GetKeyDown("c"))
        {
            cheating = !cheating;
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