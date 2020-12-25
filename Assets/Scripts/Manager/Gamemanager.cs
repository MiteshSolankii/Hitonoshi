using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gamemanager : MonoBehaviour
{
   /* [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnTime;
    private float respawnTimeStart;
    private bool respawn;
   */

    private CinemachineVirtualCamera CVC;

    public GameObject pauseMenu;
    public GameObject UI_coin, UI_healthBar;
   // public GameObject dropKeyEnemy,key;

    public  bool isPaused;
  

    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

       
    }

  /*   void DropKey()
     {
          if (dropKeyEnemy != null)
          {
              if (dropKeyEnemy.GetComponent<Entity>().isDead == true)
              {
                // GameObject keydrop =  Instantiate(key, dropKeyEnemy.transform.position, dropKeyEnemy.transform.rotation);
                  GameObject.Instantiate(key, dropKeyEnemy.transform.position, key.transform.rotation);
                Debug.Log("Object creaeeted");
              }
          }
     }
  */

    /*   public void Respawn()
       {
           respawnTimeStart = Time.time;
           respawn = true;

       }

       public void CheckRespawn()
       {
           if(Time.time >= respawnTime+ respawnTimeStart && respawn)
           {
              var playerTemp =  Instantiate(player, respawnPoint);
               CVC.m_Follow = playerTemp.transform;
               respawn = false;
           }
       }
   */


    #region Pause menu

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
        playerController.ableToMove = false;
        UI_coin.SetActive(false);
        UI_healthBar.SetActive(false);
    }

   public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
        playerController.ableToMove = true;
        UI_coin.SetActive(true);
        UI_healthBar.SetActive(true);
    }

   public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        Scenemanager.instance.SaveGameData();
        SceneManager.LoadScene("Main Menu");

    }

    #endregion
}
