using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private PlayerStats playerStats;
    private PlayerController playerController;
        
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )//can also disable damage if dashing
        {
           // playerController.anim.Play("PlayerRedFlash1");
            playerController.Knockback(-playerController.facingDirection * 1 );
            playerStats.DecreaseHealth(5f);
            
            
        }
    }
    
}
