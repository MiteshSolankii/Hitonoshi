using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverAnimation : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {

        anim.SetBool("GameEnd", true);
    }

    // Update is called once per frame
    void Update()
    {




    }

    void GameEnd2()
    {
        anim.Play("GameEnd2");
    }

    void GameEnd3()
    {
        anim.Play("GameEnd3");
    }

    void GoToMainMenu()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
       
    }
    
    

   
}
