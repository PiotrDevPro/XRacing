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
        PlayerPrefs.SetInt("AppActive", PlayerPrefs.GetInt("AppActive") + 1);
        if (LobbyManager.manage.isCityLoadingBtnClicked)
        {
            PlayerPrefs.SetInt("AppActivate", PlayerPrefs.GetInt("AppActivate") + 1);
        }
        if (PlayerPrefs.GetInt("FirstActive") == 0)
        {
            Amplitude.Instance.logEvent("AppOpen", FirstTime);
            Amplitude.Instance.logEvent("FirstTime");
            PlayerPrefs.SetString("Player", "Player" + Random.Range(0,9999));
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 10000f);
            PlayerPrefs.SetInt("FirstActive", PlayerPrefs.GetInt("FirstActive") + 1);
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

    private void Update()
    {
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus == true)
        {

            if (count == 0 && !isFirstActivate)
            {
                Amplitude.Instance.logEvent("AppOpen", SecondTime);
                print("OnApplicationFocusTrue");
                PlayerPrefs.SetInt("AppActivate", 1);
                count += 1;
            }
        }
        if (focus == false)
        {
            count = 1;
            //PlayerPrefs.SetInt("AppActive", 0);
            print("OnApplicationFocusFalse");
        }
    }
}
