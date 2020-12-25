using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallHealItem : MonoBehaviour
{
    PlayerStats playerStats;



    public float healAmount = 15f;

    void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            if (playerStats.currentHealth < playerStats.maxHealth && playerStats.isGameOver == false)
            {
                Audiomanager.instance.PlaySound("HealSound");
                playerStats.currentHealth += healAmount;

                Destroy(gameObject);

            }
        }
    }
}
