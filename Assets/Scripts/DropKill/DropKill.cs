using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropKill : MonoBehaviour
{
    private PlayerStats pS;

    void Start()
    {
        pS = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //  pS.currentHealth = 0f;
            // pS.isGameOver = true;
            pS.Invoke("GameOver", 0.2f);
            pS.Die();
        }
        if (collision.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
