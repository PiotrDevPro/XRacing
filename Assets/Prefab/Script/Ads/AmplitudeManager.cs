using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmplitudeManager : MonoBehaviour
{
    public static AmplitudeManager manage;
    public int count = 0;
    bool isFirstActivate = false;
    void Awake()
    {
        manage = this;
        Amplitude amplitude = Amplitude.Instance;
        amplitude.logging = true;
        amplitude.init("8578eac3466937dd610b84480ff56bd8");

        if (PlayerPrefs.GetInt("Install") == 0)
        {
            Amplitude.Instance.logEvent("AppOpen", FirstTime);
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 10000f);
            PlayerPrefs.SetInt("Install", PlayerPrefs.GetInt("Install") + 1);
            isFirstActivate = true;

        }
    }

    Dictionary<string, object> FirstTime = new Dictionary<string, object>() {
            {"First time" , true},
        };
    Dictionary<string, object> SecondTime = new Dictionary<string, object>() {
            {"First time" , false},
        };

    void Start()

    {
        Amplitude.Instance.logEvent("SeenMainScreen");
        

    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus == true)
        {

            if (count == 0 && !isFirstActivate)
            {
                Amplitude.Instance.logEvent("AppOpen", SecondTime);
                PlayerPrefs.SetInt("box", 0);
                count += 1;
            }
        }
        if (focus == false)
        {
            count = 1;

        }
    }
}
