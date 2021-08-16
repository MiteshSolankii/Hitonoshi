using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationMethods : MonoBehaviour
{
    public static VibrationMethods instance = null;
    // Start is called before the first frame update
    #region singleton
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    #endregion

    public void Softvibration()
    {
        long[] pattern = { 10, 10, 10, 10 };
        Vibration.Vibrate(pattern, -1);
    }
    public void HardVibration()
    {
        long[] pattern = { 100, 100, 100, 100 };
        Vibration.Vibrate(pattern, -1);
    }
    public void LongDurationVibration(int vibMilisecon)
    {
        Vibration.Vibrate(vibMilisecon);
    }
}
