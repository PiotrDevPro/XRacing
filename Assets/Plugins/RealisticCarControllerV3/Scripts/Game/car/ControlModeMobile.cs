using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlModeMobile : MonoBehaviour
{
    #region RCC Settings Instance
    private RCC_Settings RCCSettingsInstance;
    private RCC_Settings RCCSettings
    {
        get
        {
            if (RCCSettingsInstance == null)
            {
                RCCSettingsInstance = RCC_Settings.Instance;
                return RCCSettingsInstance;
            }
            return RCCSettingsInstance;
        }
    }

    #endregion
    public GameObject ButtonMode;
   // private float gyroInput = 0f;

    void Start()
    {
        switch (PlayerPrefs.GetString("ControlMode"))
        {
            case "Buttons":
                ButtonMode.SetActive(true);
               // Debug.Log(RCC_Settings.Instance.useAccelerometerForSteering);
              //  Debug.Log(RCC_Settings.Instance.useSteeringWheelForSteering);
                break;
            case "Accel":
                ButtonMode.SetActive(false);
              //  Debug.Log(RCC_Settings.MobileController.Gyro);
                break;
        }
    }
}
