using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPointTest : MonoBehaviour
{

    public GameObject dialogTextBox;
   // public GameObject continueButton2;

    private PlayerController pc;

    public bool isDialogActive = false;

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
            dialogTextBox.SetActive(true);
            
            //   continueButton2.SetActive(true);

        }

    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            isDialogActive = false;
            dialogTextBox.SetActive(false);
           
            // continueButton2.SetActive(false);


        }
    }
}
