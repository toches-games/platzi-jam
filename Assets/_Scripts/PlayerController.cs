﻿using UnityEngine;
using UnityEngine.Playables;

public enum PlayerState
{
    Dizzy,
    Normal
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController SI;

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private float wallSlidingSpeed = 5f;
    [SerializeField] private LayerMask groundLayer = default;
    [SerializeField] private Transform groundChecker = default;
    [SerializeField] private float groundCheckerRadius = 0.5f;
    [SerializeField] private Transform wallChecker = default;
    [SerializeField] private float wallCheckerDistance = 1f;

    private Rigidbody2D rig;
    private Animator anim;
    private float movementDirection;
    private bool isFacingRight = true;
    private bool jumpInput;
    private float currentSpeed;
    // for smoothdamp
    private float currentVelocity;

    private bool isWalking;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;

    public bool IsUnestablePlatform { get; private set; }
    public PlayerState State { get; set; }
    private void Awake()
    {
        SI = SI == null ? this : SI;

        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Physics2D.gravity = Vector2.down * gravity;
        State = PlayerState.Normal;
    }

    private void Update()
    {
        if (GameManager.SI.currentGameState != GameState.InGame)
        {
            return;
        }
        CheckInput();
        CheckMovementDirection();
        Jump();
        UpdateAnimations();
        CheckWallSliding();
        CheckWalking();
    }

    private void FixedUpdate()
    {
        if (GameManager.SI.currentGameState != GameState.InGame)
        {
            return;
        }
        Movement();
        CheckState();
    }

    private void CheckWalking()
    {
        if (movementDirection != 0)
        {
            isWalking = true;
        }

        else
        {
            isWalking = false;
        }
    }

    private void CheckWallSliding()
    {
        if(isTouchingWall && !isGrounded && rig.velocity.y < 0)
        {
            isWallSliding = true;
        }

        else
        {
            isWallSliding = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckerRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(wallChecker.position, new Vector2(wallChecker.position.x + wallCheckerDistance, wallChecker.position.y));
    }

    private void CheckState()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, groundLayer);
        isTouchingWall = Physics2D.Raycast(wallChecker.position, transform.right, wallCheckerDistance, groundLayer);

        //IsUnestablePlatform = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, groundLayer)
                    //.CompareTag("UnestablePlatform");
    }

    private void UpdateAnimations()
    {

        anim.SetBool("Is Dizzy", State == PlayerState.Dizzy);
    }

    private void Jump()
    {
        if (jumpInput && isGrounded)
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpSpeed);
        }

        else if(jumpInput && isWallSliding)
        {
            //if(isFacingRight && movementDirection < 0 || !isFacingRight && movementDirection > 0)
            {
                rig.velocity = new Vector2(movementDirection, 1f) * jumpSpeed;
            }
        }
    }

    private void CheckMovementDirection()
    {
        if (State == PlayerState.Dizzy)
        {
            movementDirection *= -1f;
        }

        if (isFacingRight && movementDirection < 0)
        {
            Flip();
        }

        else if(!isFacingRight && movementDirection > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if (!isWallSliding)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void Movement()
    {
        if (isWallSliding)
        {
            rig.velocity = new Vector2(rig.velocity.x, -wallSlidingSpeed);
        }

        else if(!isGrounded && !isWallSliding && movementDirection != 0)
        {
            rig.velocity = new Vector2(currentSpeed * 0.80f, rig.velocity.y);
        }

        else 
        {
            rig.velocity = new Vector2(currentSpeed, rig.velocity.y);
        }
    }

    private void CheckInput()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.UpArrow);

        if (State == PlayerState.Dizzy)
        {
            movementDirection *= -1f;
        }

        currentSpeed = Mathf.SmoothDamp(currentSpeed, movementSpeed * movementDirection, ref currentVelocity, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mint"))
        {
            collision.gameObject.SetActive(false);

            ResetState();
        }

        if (collision.CompareTag("NextRoom"))
        {
            GameObject.Find("TimeLineNextRoom").GetComponent<PlayableDirector>().Play();
        }
    }

    private void ResetState()
    {
        State = PlayerState.Normal;
        RotateMap.Instance.CurrentRotationCount = 0;
    }
}
