using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class dashboardbm : MonoBehaviour
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

    private RCC_CarControllerV3 inputs;
    [SerializeField]private Text speed;
    [SerializeField]private Text RPM;
    [SerializeField]private Slider spd_bar;

    private void Update()
    {
        speed.text = ((int)inputs.speed).ToString();
        RPM.text = ((int)inputs.engineRPM).ToString();

        spd_bar.value = ((int)inputs.speed);
    }

     void Start()
     {
         inputs = GetComponent<RCC_CarControllerV3>();
     }

    /*

     void OnEnable()
     {

         StopAllCoroutines();
         StartCoroutine("LateDisplay");

     }


     IEnumerator LateDisplay()
     {

         while (true)
         {

             yield return new WaitForSeconds(.04f);

             if (RPM)
             {
                 RPM.text = inputs.RPM.ToString("0");
             }

             if (speed)
             {
                 if (RCC_Settings.Instance.units == RCC_Settings.Units.KMH)
                     speed.text = inputs.KMH.ToString("0") + "\nKMH";
                 else
                     speed.text = (inputs.KMH * 0.62f).ToString("0") + "\nMPH";
             }
         }
     }
     */
}
