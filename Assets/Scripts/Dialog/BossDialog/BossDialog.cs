using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossDialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    [TextArea(0, 10)]
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject dBox;
    public GameObject bossHealthBar;
   

    private BossDialogPoint bossDialogPoint;
    private PlayerController playerController;
    private PlayerCombatController PCC;
    private FinalBoss boss;



    private void Start()
    {
        bossDialogPoint = FindObjectOfType<BossDialogPoint>();
        playerController = FindObjectOfType<PlayerController>();
        PCC = FindObjectOfType<PlayerCombatController>();
        boss = FindObjectOfType<FinalBoss>();
        textDisplay.text = "";
        StartCoroutine(Type());
    }
    private void Update()
    {
        if (bossDialogPoint.isDialogActive == true)
        {
            if (textDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
                playerController.ableToMove = false;
                boss.stateMachine.ChangeState(boss.idleState);
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
            playerController.ableToMove = true;
            CursorShowHide cursor = FindObjectOfType<CursorShowHide>();
            cursor.LockCursor();
            boss.stateMachine.ChangeState(boss.moveState);
            bossHealthBar.SetActive(true);
            gameObject.SetActive(false);
           

        }
    }
}
