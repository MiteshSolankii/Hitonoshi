using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossDialogPoint : MonoBehaviour
{
    public GameObject dialogTextBox;
    

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
           

        }

    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            isDialogActive = false;
            dialogTextBox.SetActive(false);
            


        }
    }
}
