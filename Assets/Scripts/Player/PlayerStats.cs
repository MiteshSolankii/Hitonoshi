using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public float maxHealth;

   [SerializeField] public float currentHealth;

    [SerializeField]
    private GameObject
        deathBloodParticle = null;

    private Gamemanager GM;
    private PlayerController PC;
    private HealItem healItem;

    public Slider healthBar = null;

    public bool isGameOver;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        currentHealth = maxHealth;
        isGameOver = false;
        GM = GameObject.Find("GameManager").GetComponent<Gamemanager>();
        PC = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        healthBar.value = currentHealth;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void DecreaseHealth(float amount)
    {
       
        currentHealth -= amount;
        PC.anim.Play("PlayerRedFlash1");
        if (!isGameOver)
        PlayerAudiomanager.instance.PlaySound("PlayerHurt");


        if (currentHealth <= 0.0f)
        {

            isGameOver = true;
            
             Die();
             Invoke("GameOver", 0.9f);
            
        }
       
    }

    
  

    public void Die()
    {
        PlayerAudiomanager.instance.PlaySound("PlayerDie");
        PC.ableToMove = false;
        spriteRenderer.enabled = false;
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
       
        Destroy(gameObject,1f);    
    }

    public void GameOver()
    {
        Scenemanager.instance.SaveGameData();
        SceneManager.LoadScene("Game Over");
    }
    
  
   
    
}
