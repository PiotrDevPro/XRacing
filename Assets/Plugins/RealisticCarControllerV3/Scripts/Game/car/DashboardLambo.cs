using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashboardLambo : MonoBehaviour
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

    [SerializeField] private Text curr_gear;
    [SerializeField] private Text spd;
    int gearNum;

    private RCC_CarControllerV3 inputs;

    private void Start()
    {
        inputs = GetComponent<RCC_CarControllerV3>();
        
    }

    private void Update()
    { 
        gearNum = inputs.currentGear + 1;
        curr_gear.text = gearNum.ToString();
        spd.text = ((int)inputs.speed).ToString() + " kmh";
    }
}
