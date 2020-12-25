using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            Scoremanager.instance.ChangeScore(coinValue);
            Destroy(gameObject);
            Audiomanager.instance.PlaySound("CoinSound");
        }
    }
}
