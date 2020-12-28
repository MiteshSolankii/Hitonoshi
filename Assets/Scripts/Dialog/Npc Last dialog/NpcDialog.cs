using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcDialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    [TextArea(0, 10)]
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject dBox;

    private NpcDialogPoint npcDialogPoint;
    private PlayerController playerController;
    private PlayerCombatController PCC;
    public GameObject GOA;
    public GameObject UI_Coin, UI_Healthbar;



    private void Start()
    {
        
        npcDialogPoint = FindObjectOfType<NpcDialogPoint>();
        playerController = FindObjectOfType<PlayerController>();
        PCC = FindObjectOfType<PlayerCombatController>();
        textDisplay.text = "";
        StartCoroutine(Type());
    }
    private void Update()
    {
        if (npcDialogPoint.isDialogActive == true)
        {
            
            if (textDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
                playerController.ableToMove = false;
                CursorShowHide cursor = FindObjectOfType<CursorShowHide>();
                cursor.UnlockCursor();
                

            }
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
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
            CursorShowHide cursor = FindObjectOfType<CursorShowHide>();
            cursor.LockCursor();
            //playerController.ableToMove = true;
            UI_Coin.SetActive(false);
            UI_Healthbar.SetActive(false);
            GOA.SetActive(true);
        }
    }
}
