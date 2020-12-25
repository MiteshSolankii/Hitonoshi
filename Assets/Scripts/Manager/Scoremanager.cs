using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoremanager : MonoBehaviour
{
    public static Scoremanager instance;
    public Text text;
    private int score;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = "X" + score.ToString();
    }
}
