using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public const string MinSave = "MinuteSave";
    public const string SecSave = "SecondSave";
    public const string MilliSave = "MilliSave";
    public const string LapsEnded = "LapSave";
    public const string DriftScore = "DriftScore";
    public const string DriftHighScore = "DriftHighScore";
    public const string DriftCoin = "DriftCoin";
    public static int min = 0;
    public static int sec = 0;
    public static int laps = 0;
    public static float milli = 0;
    public static int drift = 0;
    public static int drifthigh = 0;
    public static float driftcoin = 0;

    public int minCount;
    public int secCount;
    public float milliCount;
    public int driftCount;

    public GameObject MinDisplay;
    public GameObject SecDisplay;
    public GameObject MilliDisplay;
    public Text DriftDisplay;
    //public Text cashDisplay;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "battle_online")
        {
            min = PlayerPrefs.GetInt("MinuteSave");
            sec = PlayerPrefs.GetInt("SecondSave");
            milli = PlayerPrefs.GetFloat("MilliSave");
            laps = PlayerPrefs.GetInt("LapSave");
            drift = PlayerPrefs.GetInt("DriftScore");
            drifthigh = PlayerPrefs.GetInt("DriftHighScore");
            driftcoin = PlayerPrefs.GetFloat("DriftCoin");
            //PlayerPrefs.DeleteAll();

            DriftDisplay.text = drifthigh.ToString();
            //cashDisplay.text = driftcoin.ToString() + "$";

            minCount = min;
            secCount = sec;
            milliCount = milli;
            driftCount = drifthigh;

            MinDisplay.GetComponent<Text>().text = "" + minCount + ":";
            if (sec < 10)
            {
                SecDisplay.GetComponent<Text>().text = "0" + secCount + ".";
            }
            else
            {
                SecDisplay.GetComponent<Text>().text = "" + secCount + ".";
            }

            MilliDisplay.GetComponent<Text>().text = "" + milliCount;
        }
    }

    public static void UpdateMin()
    {
        PlayerPrefs.SetInt("MinuteSave", min);
        min = PlayerPrefs.GetInt("MinuteSave");
        PlayerPrefs.Save();
    }
    public static void UpdateSec()
    {
        PlayerPrefs.SetInt("SecondSave", sec);
        sec = PlayerPrefs.GetInt("SecondSave");
        PlayerPrefs.Save();

    }
    public static void UpdateMilli()
    {
        PlayerPrefs.SetFloat("MilliSave", milli);
        milli = PlayerPrefs.GetFloat("MilliSave");
        PlayerPrefs.Save();
    }

    public static void UpdateLaps()
    {
        PlayerPrefs.SetInt("LapSave", laps);
        laps = PlayerPrefs.GetInt("LapSave");
        PlayerPrefs.Save();
    }

    public static void UpdateDriftScore()
    {
        PlayerPrefs.SetInt("DriftScore", drift);
        drift = PlayerPrefs.GetInt("DriftScore");
        PlayerPrefs.Save();
    }

    public static void UpdateDriftHighScore()
    {
        PlayerPrefs.SetInt("DriftHighScore", drifthigh);
        drifthigh = PlayerPrefs.GetInt("DriftHighScore");
        PlayerPrefs.Save();
    }

    public static void UpdateDriftCoin()
    {
        PlayerPrefs.SetFloat("DriftCoin", driftcoin);
        driftcoin = PlayerPrefs.GetFloat("DriftCoin");
        PlayerPrefs.Save();
    }
}
