using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine;

public class UnityAd : MonoBehaviour
{
    public static UnityAd manage;

    private bool isAdsShop = false;
    private bool isAds25Sec = false;
    private bool isAdsEnergy = false;
    private bool isAdsForCarCrashed = false;
    private void Awake()
    {
        manage = this;
        Advertisement.Initialize("4107005", true);
    }

    public void ShowAdShop()
    {
        if (Advertisement.IsReady() && Advertisement.isInitialized)
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
            isAdsShop = true;
        }
    }

    public void ShowAdForEnergy()
    {
        if (Advertisement.IsReady() && Advertisement.isInitialized)
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
            isAdsEnergy = true;
        }
    }

    public void ShowAd25sec()
    {
        if (Advertisement.IsReady() && Advertisement.isInitialized)
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
            isAds25Sec = true;
        }
    }

    public void ShowAdForCarCrashed()
    {
        if (Advertisement.IsReady() && Advertisement.isInitialized)
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
            isAdsForCarCrashed = true;
        }
    }

    public void ShowAdDefault()
    {
        if (PlayerPrefs.GetInt("NoAds") != 1)
        {
            if (Advertisement.IsReady() && Advertisement.isInitialized)
            {
                Amplitude.Instance.logEvent("InterstatialShow");
                Advertisement.Show("video", new ShowOptions() { resultCallback = HandleAdResultSkipable });
            }
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                if (isAdsShop)
                {
                    latencyCoinUpdate15();
                    isAdsShop = false;
                }
                if (isAds25Sec)
                {
                    Checkpoint.manage.ContinueAdsForTimeCheckpoint();
                    Amplitude.Instance.logEvent("OnRewardedVideoForCheckpoint");
                    isAds25Sec = false;
                }

                if (isAdsEnergy)
                {
                    CarDamage.manage.energy = CarDamage.manage.energy + 25;
                    CarDamage.manage.energyBarProgress.GetComponent<Text>().text = CarDamage.manage.energy.ToString();
                    CarDamage.manage.StartEngine();
                    CarDamage.manage.isDead = false;
                    CarAi.manage.StartEngine();
                    CarAi1.manage.StartEngine();
                    CarAi2.manage.StartEngine();
                    CarManager.manage.lose.SetActive(false);
                    if (PlayerPrefs.GetInt("Soundtrack") == 0)
                    {
                        Pause.manage.tracks[Random.Range(0, 6)].Play();
                    }
                    isAdsEnergy = false;
                    RCC_CarControllerV3.manage.ResetCarForCoin();
                    DamagePartsHood.manage.wheels1.gameObject.SetActive(true);
                    DamagePartsHood.manage.wheels2.gameObject.SetActive(true);
                    DamagePartsHood.manage.wheels1col.gameObject.SetActive(true);
                    DamagePartsHood.manage.wheels2col.gameObject.SetActive(true);
                    DamagePartsHood.manage.blow.SetActive(false);
                    DamagePartsHood.manage.point = 50;
                    DPTrunk.manage.wheels1.gameObject.SetActive(true);
                    DPTrunk.manage.wheels2.gameObject.SetActive(true);
                    DPTrunk.manage.wheels1col.gameObject.SetActive(true);
                    DPTrunk.manage.wheels2col.gameObject.SetActive(true);
                    DPTrunk.manage.point = 50;
                    Amplitude.Instance.logEvent("OnRewardedVideoForEnergy");
                }
                if (isAdsForCarCrashed)
                {
                    CarDamage.manage.StartEngine();

                    AudioListener.pause = false;
                    PlayerPrefs.SetInt("crashed", 0);
                    if (PlayerPrefs.GetInt("Soundtrack") == 0)
                    {
                        Pause.manage.tracks[Random.Range(0, 6)].Play();
                    }
                    LevelManager.manage.Crashed.SetActive(false);
                    CarDamage.manage.energyBarProgress.GetComponent<Text>().text = CarDamage.manage.energy.ToString();
                    Amplitude.Instance.logEvent("OnRewardedVideoForCrashCar");
                    Time.timeScale = 1;
                    isAdsForCarCrashed = false;
                }

                break;
            case ShowResult.Skipped:
                isAdsShop = false;
                isAdsForCarCrashed = false;
                isAds25Sec = false;
                break;
            case ShowResult.Failed:
                isAdsShop = false;
                isAdsForCarCrashed = false;
                isAds25Sec = false;
                break;
        }
    }

    private void HandleAdResultSkipable(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Amplitude.Instance.logEvent("InterstatialCompleted");
                break;
            case ShowResult.Skipped:
                Amplitude.Instance.logEvent("InterstatialSkipped");
                break;
            case ShowResult.Failed:
                Amplitude.Instance.logEvent("InterstatialFailed");
                break;
        }
    }

    #region Add coin's/energy for Ad
    void latencyCoinUpdate15()
    {

        GameObject snd = GameObject.Find("Purchased");
        snd.GetComponent<AudioSource>().Play();
        Invoke("latencySpawnCoin", 0.5f);
        Invoke("addCoinsOnShop", 1.7f);
    }


    void latencySpawnCoin()
    {
        MainMenuManager.manage.animate(25);
    }

    void addCoinsOnShop()
    {
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 2500f);
        Amplitude.Instance.logEvent("2500$ - OnRewardedVideoFinished");
    }
    #endregion
}


