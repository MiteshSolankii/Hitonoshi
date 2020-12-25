using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 25f;
    private float groundCheckRadius = 0.16f;
    private float wallCheckDistance = 0.23f;
    public float wallSlideSpeed = 2f;
   // private float moveForceInAir = 12f;
    private float airDrag = 0.9f;
    public float variableJumpHeight = 0.8f;
    private float jumpTimer;
    private float turnTimer;
    private float wallJumpTimer;
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown;
    
        

    private int amountOfJumpLeft;
    private int amountOfJump = 1;
    public int facingDirection = 1;
    private int lastWallJumpDirection;

    public float wallJumpForce = 43f;
    //public float wallHopForce;
    private float jumpTimerSet = 0.15f;
    private float turnTimerSet = 0.1f;
    private float wallJumpTimerSet = 0.5f;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;
    private float knockbackStartTime;
    [SerializeField]
    private float knockbackDuration = 0f;


    public Vector2 wallJumpDirection;
    // public Vector2 wallHopDirection;
    [SerializeField]
    private Vector2 knockbackSpeed = new Vector2(0f,0f);

  /*  [SerializeField]
    private Vector2
        ledgePosBot,
        ledgePos1,
        ledgePos2;

    [SerializeField]
    private float
        ledgeClimbXOffset1 = 0f,
        ledgeClimbYOffset1 = 0f,
        ledgeClimbXOffset2 = 0f,
        ledgeClimbYOffset2 = 0f;
  */


    public float moveInput;

    private bool isFacingRight = true;
    public bool isWalking;
    public bool isGrounded;
    private bool canNormalJump;
    private bool canWallJump;
    private bool isTouchingWall;
    public bool isWallSliding;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    private bool canMove;
    public bool canFlip;
    private bool hasWallJumped;
    public bool isDashing;
    private bool knockback;
    /* private bool isTouchingLedge;
     private bool canClimbLedge = false;
     private bool ledgeDetected;
    */

    public Transform groundCheck;
    public Transform wallCheck;
    // public Transform ledgeCheck;
    public Transform keyFollowPoint;

    public LayerMask whatIsGround;

    private PlayerCombatController PCC;

    [SerializeField] private ParticleSystem dustTrail = null;
    [SerializeField] private ParticleSystem dustPound = null;
   
    Rigidbody2D playerRb;
   public Animator anim;

    public bool ableToMove = false;

    private bool wasOnGround;

    public Key followingKey;



    private void Awake()
    {
        

        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        PCC = GetComponent<PlayerCombatController>();
    }

    void Start()
    {
        ableToMove = true;
        amountOfJumpLeft = amountOfJump;
       // wallHopDirection.Normalize();
        wallJumpDirection.Normalize();

       

    }

    // Update is called once per frame
    void Update()
    {
        
        CheckInput();
        CheckJump();
        CheckPlayerDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckDash();
        CheckKnockback();
        //CheckLedgeClimb();
        DialogAction();


       

    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    void CheckInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded || (amountOfJumpLeft) > 0 && !isTouchingWall)
            {
                NormalJump();
                
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        if(Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if(!isGrounded && moveInput != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
                   
            }
        }

        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if(turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if (checkJumpMultiplier && !Input.GetButton("Jump"))  // to vary jump height
        {
            checkJumpMultiplier = false;
            playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y * variableJumpHeight);
        }

        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown))
                
                AttemptToDash();
        }


    }

    private void DisableFlip()
    {
        canFlip = false;
    }

    private void EnableFlip()
    {
        canFlip = true;
    }

    void AttemptToDash()
    {
        if (FindObjectOfType<Gamemanager>().isPaused ==false){ DashSound(); }
        CreatDustTrail();
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    public void Knockback(int direction)
    {
        knockback = true;
      
        knockbackStartTime = Time.time;
        playerRb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    private void CheckKnockback()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
           
            playerRb.velocity = new Vector2(0.0f, playerRb.velocity.y);


        }
    }


    void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {

                canMove = false;
                canFlip = false;
                playerRb.velocity = new Vector2(dashSpeed * facingDirection, playerRb.velocity.y);

                dashTimeLeft -= Time.deltaTime;

                if(Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if(dashTimeLeft <= 0 || isTouchingWall)
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
            }
        }
    }

    public bool GetDashStatus()
    {
        return isDashing;
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }


    void ApplyMovement()
    {
        // can use && PCC.isAttacking == false in if statement but not working fine...
        if (!isGrounded && !isWallSliding && moveInput == 0 && !knockback)   //let go of movement when not pressing keys while jumping
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x * airDrag, playerRb.velocity.y);
        }

        else if (canMove && !knockback)
        {
            playerRb.velocity = new Vector2(moveInput * moveSpeed, playerRb.velocity.y);
            
        }

        if (isWallSliding)
        {
            if (playerRb.velocity.y < -wallSlideSpeed)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, -wallSlideSpeed);
                
            }
        }
        JumpLand();
    }

    void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

       /* isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if(isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;
        }*/
    }

  /*  private void CheckLedgeClimb()
    {
        if (ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if (isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset2, Mathf.Ceil(ledgePosBot.y) + ledgeClimbYOffset2);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Ceil(ledgePosBot.y) + ledgeClimbYOffset2);
            }

            canMove = false;
            canFlip = false;
            anim.SetBool("canClimbLedge", canClimbLedge);
        }

        if (canClimbLedge)
        {
            transform.position = ledgePos1;
        }
    }*/

   /* private void FinishLedgeClimb()
    {
        canClimbLedge = false;
        canFlip = true;
        canMove = true;
        ledgeDetected = false;
        transform.position = ledgePos2;
        anim.SetBool("canClimbLedge", canClimbLedge);
    }*/


    void CheckJump()
    {
       
        if(jumpTimer > 0)
        {
            if(!isGrounded && isTouchingWall && moveInput !=0 && moveInput != facingDirection)
            {
                WallJump();
            }

            else if (isGrounded)
            {
                
                NormalJump();
            }
        }
        if(isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }

        if(wallJumpTimer > 0)
        {
            if(hasWallJumped && moveInput == -lastWallJumpDirection)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0.0f);
                hasWallJumped = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }

       

    }

    void NormalJump()
    {
        if (canNormalJump)
        {
            JumpSound();
           playerRb.velocity = Vector2.up * jumpForce;
           amountOfJumpLeft--;
           jumpTimer = 0;
           isAttemptingToJump = false;
           checkJumpMultiplier = true;
        }


    }

    void WallJump()
    {
         if (canWallJump)
         {

                playerRb.velocity = new Vector2(playerRb.velocity.x, 0.0f);
                isWallSliding = false;
                amountOfJumpLeft = amountOfJump;
                amountOfJumpLeft--;
                Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * moveInput, wallJumpForce * wallJumpDirection.y);
                playerRb.AddForce(forceToAdd, ForceMode2D.Impulse);
                jumpTimer = 0;
                isAttemptingToJump = false;
                checkJumpMultiplier = true;
                turnTimer = 0;
                canMove = true;
                canFlip = true;
                hasWallJumped = true;
                wallJumpTimer = wallJumpTimerSet;
                lastWallJumpDirection = -facingDirection;

           


         }
    }
    void CheckIfCanJump()
    {
        if (isGrounded && playerRb.velocity.y < 0.01f) 
        {
            amountOfJumpLeft = amountOfJump;
           
        }

        if (isTouchingWall)
        {
            canWallJump = true;
        }

        if(amountOfJumpLeft <= 0)
        {
            canNormalJump = false;
            
        }

        else
        {
            canNormalJump = true;
           
        }
    }

    void CheckIfWallSliding()
    {
        if (isTouchingWall && moveInput == facingDirection && playerRb.velocity.y < 0)
        {
            isWallSliding = true;
            
           
        }

        else
        {
            isWallSliding = false;
        }

    }

    void CheckPlayerDirection()
    {
        if (isFacingRight && moveInput < 0)
        {
            Flip();

        }

        else if (!isFacingRight && moveInput > 0)
        {
            Flip();

        }


        if ((Mathf.Abs(playerRb.velocity.x) > 0.01f))
        {
            isWalking = true;
        }

        else
        {
            isWalking = false;
        }

    }

    void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", playerRb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    void Flip()
    {
        if (!isWallSliding && canFlip && !knockback)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
            CreatDustTrail();
        }
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));

       
    }

    void CreatDustTrail()
    {
        dustTrail.Play();
      
    }
    void CreateDustPound()
    {
        dustPound.Play();
    }

    void JumpLand()
    {
        //show jump land
        if (!wasOnGround && isGrounded)
        {
            JumpLandSound();
            CreateDustPound();
        }
        wasOnGround = isGrounded;
    }

   void DialogAction()
    {
        if (!ableToMove)
        {
            
            
            //stop player movement and attack here
            moveInput = 0f;
            PCC.combatEnabled = false;
            dashSpeed = 0f;
            jumpForce = 0f;
            canFlip = false;
            canNormalJump = false;
            

            
        }
        else
        {

            moveInput = Input.GetAxisRaw("Horizontal");
            PCC.combatEnabled = true;
            dashSpeed = 40f;
            jumpForce = 24f;
            canFlip = true;
            canNormalJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.gameObject.transform;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }

       
    }

   
    #region Audio

    void WalkSound()
    {
        PlayerAudiomanager.instance.PlaySound("PlayerWalk");
    }
    
    void JumpSound()
    {
        PlayerAudiomanager.instance.PlaySound("PlayerJump");
    }
    void DashSound()
    {
        PlayerAudiomanager.instance.PlaySound("PlayerDash");
    }
    void JumpLandSound()
    {
        PlayerAudiomanager.instance.PlaySound("PlayerJumpLand");
    }


    #endregion

}
