using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnewayPlatform : MonoBehaviour
{
    
    public PlatformEffector2D effector;
    public float waitTime = 0.5f;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.S) || (Input.GetKeyUp(KeyCode.DownArrow)))
        {
            waitTime = 0.5f;
        }
        if(Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.DownArrow)))
        {
            if(waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            effector.rotationalOffset = 0f;
        }

       

    }

    public void OnHoldTimerFinished()
    {
      //  effector.rotationalOffset = 180f;
        waitTime = 0f;
       
    }
    public void OnHoldTimerUp()
    {
        waitTime = 0.5f;
       
    }
    public void ResetOneWay()
    {
        effector.rotationalOffset = 0f;
    }
}
