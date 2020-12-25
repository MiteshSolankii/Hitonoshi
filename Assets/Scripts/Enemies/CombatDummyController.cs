using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 30f;

    [SerializeField]
    private bool applyKnockback = false;

    [SerializeField]
    private float knockbackSpeedX =0f, knockbackSpeedY = 0f, knockbackDuration =0f;

    [SerializeField]
    private float knockbackDeathSpeedX =0f, knockbackDeathSpeedY = 0f, deathTorque =0f;

    [SerializeField]
    private GameObject hitparticle = null;

    private int playerFacingDirection;

    private bool playerOnLeft, knockback;
   
    private float currentHealth, knockbackStart;

    private PlayerController pc;

    private GameObject aliveGo, brokenTopGo, brokenBotGo;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator aliveAnim;

    private void Start()
    {
        currentHealth = maxHealth;

       pc = GameObject.Find("Player").GetComponent< PlayerController > ();

        aliveGo = transform.Find("Alive").gameObject;
        brokenTopGo = transform.Find("Broken Top").gameObject;
        brokenBotGo = transform.Find("Broken Bottom").gameObject;

        aliveAnim = aliveGo.GetComponent<Animator>();

        rbAlive = aliveGo.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGo.GetComponent<Rigidbody2D>();
        rbBrokenBot = brokenBotGo.GetComponent<Rigidbody2D>();

        aliveGo.SetActive(true);
        brokenTopGo.SetActive(false);
        brokenBotGo.SetActive(false);
    }

    private void Update()
    {
        CHeckKnockback();
    }

    public void Damage(AttackDetails details)
    {
        currentHealth -= details.damageAmount;

      //  playerFacingDirection = pc.GetFacingDirection();

        if(details.position.x < aliveGo.transform.position.x)
        {
            playerFacingDirection = 1;
        }
        else
        {
            playerFacingDirection = -1;
        }

        Instantiate(hitparticle, aliveGo.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));


        if(playerFacingDirection == 1)
        {
           playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }

        aliveAnim.SetBool("playerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("damage");

        if( applyKnockback && currentHealth > 0.0f)
        {
            //apply knockback
            Knockback();

            Audiomanager.instance.PlaySound("DummyHurt");
        }

        if(currentHealth <= 0.0f)
        {
            //die 
            Die();

            Audiomanager.instance.PlaySound("DummyDie");
        }
    }

    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }

    private void CHeckKnockback()
    {
        if(Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        aliveGo.SetActive(false);
        brokenBotGo.SetActive(true);
        brokenTopGo.SetActive(true);

        brokenTopGo.transform.position = aliveGo.transform.position;
        brokenBotGo.transform.position = aliveGo.transform.position;

        rbBrokenBot.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection,knockbackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);

      //Destroy(brokenBotGo, 1.5f);
      //Destroy(brokenTopGo, 1.5f);

    }

    
}
