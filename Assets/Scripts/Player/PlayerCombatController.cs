using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    public bool combatEnabled = false;
    [SerializeField]
    private float inputTimer = 0f, attack1Radius = 0f, attack1Damage = 0f;
    [SerializeField]
    private Transform attack1HitBoxPos = null;
    [SerializeField]
    private LayerMask whatIsDamageable = default;

    private AttackDetails attackDetails;

    public bool gotInput, isFirstAttack, isAttacking;

    private float lastInputTime = Mathf.NegativeInfinity;

    private PlayerController PC;
    private PlayerStats PS;

    [SerializeField] private float stunDamageAmount = 1f;

    

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<PlayerController>();
        PS = GetComponent<PlayerStats>();
        
    }
    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
                
            }


        }

       
        
       
    }

    public void CheckAttacks()
    {
        if (gotInput)
        {
            
            
            if (!isAttacking && !PC.isWallSliding)
            {
                    AttackSound();
                    gotInput = false;
                    isAttacking = true;
                    isFirstAttack = !isFirstAttack;
                    anim.SetBool("isAttacking", isAttacking);
                    anim.SetBool("firstAttack", isFirstAttack);
                    anim.SetBool("attack1", true);

            }
           

            if(Time.time >= lastInputTime + inputTimer)
            {
                gotInput = false;
            }
        }

       
    }

    void CheckAttackHitBox()
    {
        Collider2D[] detectedObject = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        attackDetails.damageAmount = attack1Damage;
        attackDetails.position = transform.position;
        attackDetails.stunDamageAmount = stunDamageAmount;

        foreach (Collider2D collider in detectedObject)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }

        
    }

    private void FinishAttack1()
    {

        
            isAttacking = false;
            anim.SetBool("isAttacking", isAttacking);
            anim.SetBool("attack1", false);
            isFirstAttack = false;
    
       
    }

  

    private void Damage(AttackDetails attackDetails)
    {

        if (!PC.GetDashStatus())
        {
            int direction;

            PS.DecreaseHealth(attackDetails.damageAmount);

            if (attackDetails.position.x < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            PC.Knockback(direction);

           
        }
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }

    #region Audio
    void AttackSound()
    {
        PlayerAudiomanager.instance.PlaySound("PlayerAttack");
    }

    #endregion


}


