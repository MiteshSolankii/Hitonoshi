using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPoint : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject continueButton;

    private PlayerController pc;

    public  bool isDialogActive = false;

    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D trig)
    {
        
        
       if (trig.gameObject.CompareTag("Player"))
       {
           isDialogActive = true;
           dialogBox.SetActive(true);
           continueButton.SetActive(true);
          
       }
        
    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            isDialogActive = false;
            dialogBox.SetActive(false);
            continueButton.SetActive(false);
         
            
        }
    }
}
