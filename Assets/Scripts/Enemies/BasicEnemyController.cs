using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
   private enum State
   {
        Moving,
        Knockback,
        Dead
   }

    [SerializeField]
    private float groundCheckDistance = 0f,
        wallCheckDistance = 0f ,
        moveSpeed = 0f,
        maxHealth = 0f,
        knockbackDuration= 0f,
        touchDamageCooldown = 0f,
        touchDamage =0f,
        touchDamageWidth = 0f,
        touchDamageHeight = 0f,
        lastTouchDamageTime = 0f;

    

    [SerializeField]
    private Transform groundCheck = null, wallCheck = null, touchDamageCheck = null;

    [SerializeField]
    private LayerMask whatIsGround = default,whatIsPlayer = default;

    [SerializeField]
    private GameObject hitParticle = null, deathChunkParticle = null, deathBloodParticle = null;

    
    private GameObject alive;

    private Rigidbody2D rbAlive;

    private Animator aliveAnim;

    private bool groundDetected, wallDetected;

    private int facingDirection, damageDirection;

    private float currentHealth, knockbackStartTime;

    private float[] attackDetails = new float[2];
    
    private Vector2 
        movement,
        touchDamageBotLeft,
        touchDamageTopRight;

    [SerializeField]
    private Vector2 knockbackSpeed = new Vector2(0f,0f);

    private State currentState;

    private void Start()
    { 
    
        alive = transform.Find("Alive").gameObject;
        rbAlive = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();
        facingDirection = 1;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    //.. Walking State...............................................
    #region
    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position,transform.right,wallCheckDistance,whatIsGround);

        CheckTouchDamage();

        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(moveSpeed * facingDirection, rbAlive.velocity.y);
            rbAlive.velocity = movement;
        }

    }
 
    private void ExitMovingState()
    {

    }

    #endregion

    //..Knockback State............................................
    #region
    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, rbAlive.velocity.y);
        rbAlive.velocity = movement;
        aliveAnim.SetBool("knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if(Time.time > knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("knockback", false);
    }

#endregion

    //..Dead State...................................
    #region
    private void EnterDeadState() 
    {
        Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    #endregion

    //..Other Functions....................................
    #region

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        if(attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if(currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
                
        }
    }

    private void CheckTouchDamage()
    {
        if(Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if(hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);

                print("touch damage given");

            }

            
        }
   }


    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0f, 180f, 0f);
    }
    
    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));


        Vector2 botLeft  = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft  = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight ,topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }

    #endregion
}
