using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorShowHide : MonoBehaviour
{
    public bool lockCursor = false;
    void Start()
    {
        if (lockCursor)
        {
            LockCursor();
        }
        else
        {
            UnlockCursor();
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }

   public void LockCursor()
    {
       // lockCursor = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

  public  void UnlockCursor()
    {
       // lockCursor = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
