using System.Collections;
using System.Collections.Generic;
//using AppodealAds.Unity.Common;
using UnityEngine;

public class Restart : MonoBehaviour
{
    
    public void RestartLevel()
    {
        
        Time.timeScale = 1f;
        
        PlayerPrefs.SetInt("crashed", 0);
        //ironSourceManager.manage.ShowInterstitialButtonClicked();
        //AppDealManager.manage.ShowInterstatial();
        Invoke("latency", 0.1f);

    }

    void latency()
    {
        LapTimeManager.MinuteCount = 0;
        LapTimeManager.SecondCount = 0;
        LapTimeManager.MilliCount = 0;
        LapTimeManagerAI.MinuteCount = 0;
        LapTimeManagerAI.SecondCount = 0;
        LapTimeManagerAI.MilliCount = 0;
        AudioListener.pause = false;
        SaveManager.laps = 0;
        PlayerPrefs.SetInt("AiCrashed", 0);
        PlayerPrefs.SetInt("AiCrashed1", 0);
        PlayerPrefs.SetInt("AiCrashed2", 0);
        Application.LoadLevel(Application.loadedLevel);
        

    }
}
