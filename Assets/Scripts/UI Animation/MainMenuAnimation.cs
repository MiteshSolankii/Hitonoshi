using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Logo");
    }

    // Update is called once per frame
    void Update()
    {
        
        

        
    }

    private void UIAnim()
    {
        if (Scenemanager.instance.continueButton != null)
        {
            Scenemanager.instance.sceneToContinue = PlayerPrefs.GetInt("SavedScene");

            if (Scenemanager.instance.sceneToContinue != 0)
            {

                anim.Play("Nav");
            }
            else
            {
                Scenemanager.instance.continueButton.SetActive(false);
                anim.Play("Nav1");
            }
        }
        else
        {
            anim.Play("Nav");
        }

        
      
    }
    private void AnimStop()
    {
        anim.StopPlayback();
    }
}
