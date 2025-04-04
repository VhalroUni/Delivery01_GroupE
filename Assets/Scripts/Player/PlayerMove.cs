﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumping_pow;
    private bool isGrounded;
    private bool isOnWall;
    private int doubleJump = 0;
    private bool canDoubleJump = false;
    private bool powerJump = false;
    private Vector2 moveDir = Vector2.zero;
    private bool facingR;
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
        FaceDirection();
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isWalking", moveDir.x != 0 && isGrounded);
        animator.SetBool("isJumping", !isGrounded);
    }

    void OnEnable()
    {
        JumpBooster.OnBoosterTouched += RestartJump;
        PowerJump.OnEnter += MegaJump;
        PowerJump.OnExit += NoMegaJump;
        NewPowerJump.OnEnter += MegaJump;
        ActivateDoubleJump.OnEnter += TriggerDoubleJump;
        Bullet.Burning += IronEx;
    }
    void OnDisable()
    {
        JumpBooster.OnBoosterTouched -= RestartJump;
        PowerJump.OnEnter -= MegaJump;
        PowerJump.OnExit -= NoMegaJump;
        NewPowerJump.OnEnter -= MegaJump;
        ActivateDoubleJump.OnEnter -= TriggerDoubleJump;
        Bullet.Burning -= IronEx;
    }

    private void FaceDirection()
    {
        if (moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
            facingR = true;
        }
        else if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
            facingR = false;
        }
    }

    
    void OnMove(InputValue value)
    {
        var input_value = value.Get<Vector2>();
        moveDir = input_value;
        rigidBody.linearVelocityX = moveDir.x * speed;
    }

    void OnJumpStarted()
    {
        if (canDoubleJump) 
        {
            if ((!isGrounded) && (doubleJump <= 0)) 
            {
                rigidBody.linearVelocityY = jumping_pow * 0.85f;

                doubleJump += 1;
                jumpParticles.Play();
                ControlSound.instance.RunSound(jumpSound);
            }
        }

        if (isGrounded) 
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumping_pow);
            ControlSound.instance.RunSound(jumpSound);
        }

        if (isOnWall && !isGrounded) 
        { 
            if (facingR)
            {
                Vector2 dir = new Vector2(10, 10);
                rigidBody.AddForce(dir, ForceMode2D.Force);
            }
            else 
            {
                Vector2 dir = new Vector2(-10, 10);
                rigidBody.AddForce(dir, ForceMode2D.Force);
            }  
        }

        powerJump = false;
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

    private void TriggerDoubleJump()
    {
        canDoubleJump = true;  
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

    private void RestartJump()
    {
        doubleJump = 0;
    }

    private void MegaJump() 
    {
        powerJump = true;
    }

    private void NoMegaJump() 
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
    private void IronEx(Vector2 direction) 
    {
        direction.Normalize();
        rigidBody.AddForce(-direction*20, ForceMode2D.Impulse);
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