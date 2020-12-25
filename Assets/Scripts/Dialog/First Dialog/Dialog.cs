using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    [TextArea]
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject dBox;

    private DialogPoint dialogPoint;
    private PlayerController playerController;
    private PlayerCombatController PCC;

    

    private void Start()
    {
        dialogPoint = FindObjectOfType<DialogPoint>();
        playerController = FindObjectOfType<PlayerController>();
        PCC = FindObjectOfType<PlayerCombatController>();
        textDisplay.text = "";
        StartCoroutine(Type());
    }
    private void Update()
    {
        if (dialogPoint.isDialogActive == true)
        {
            if (textDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
                playerController.ableToMove = false;

            }
        }
    }

    IEnumerator Type()
    {
        foreach (char  letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            dBox.SetActive(false);
           playerController.ableToMove = true;
         
         


        }
    }
}
