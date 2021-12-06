using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UniAdManager : MonoBehaviour, IUnityAdsListener
{
    public static UniAdManager manage;
#if UNITY_IOS || UNITY_IPHONE
    private string APP_KEY = "4107004";
#else
    private string APP_KEY = "4107005";
#endif
    private bool isAdsShop = false;
    private bool isAds25Sec = false;
    private bool isAdsEnergy = false;
    private bool isAdsEnergyArena = false;
    private bool isAdsEnergyInShop = false;
    private bool isAdsForCarCrashed = false;
    private void Awake()
    {
        manage = this;
        Advertisement.Initialize("4107005", true);
    }


    private void Start()
    {
        print(APP_KEY);
        Advertisement.AddListener(this);
        if (PlayerPrefs.GetInt("NoAds") != 1)
        {
            //ShowBanner();
        }
    }

    public void ShowBanner()
    {

                //StartCoroutine(ShowBannerMainMenu());
                //print("showBanner");
                //Amplitude.Instance.logEvent("showBanner");
    }

    IEnumerator ShowBannerMainMenu()
    {
    
        while (!Advertisement.IsReady())
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_LEFT);
        Advertisement.Banner.Show("Banner");
    }

    public void ShowAdShop()
    {
        if (Advertisement.IsReady() && Advertisement.isInitialized)
        {
            print("ShowAdShop");
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

    public void ShowAdForEnergyArena()
    {
        if (Advertisement.IsReady() && Advertisement.isInitialized)
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
            isAdsEnergyArena = true;
        }
    }

    public void ShowAdForEnergyInShop()
    {
        if (Advertisement.IsReady() && Advertisement.isInitialized)
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
            isAdsEnergyInShop = true;
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
                Advertisement.Show("Interstatial", new ShowOptions() { resultCallback = HandleAdResultSkipable });
            }
        }
    }

    public void ShowInterstatial()
    {
        if (PlayerPrefs.GetInt("NoAds") != 1)
        {
            if (Advertisement.IsReady() && Advertisement.isInitialized)
            {
                Amplitude.Instance.logEvent("TopSpeedInterstatialShow");
                Advertisement.Show("Interstatial", new ShowOptions() { resultCallback = HandleAdResultSkipable });
            }
        }
    }

    private void ShowBannerCallback(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Amplitude.Instance.logEvent("BannerCompleted");
                break;
            case ShowResult.Skipped:
                Amplitude.Instance.logEvent("BannerSkipped");
                break;
            case ShowResult.Failed:
                Amplitude.Instance.logEvent("BannerFailed");
                break;
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
                        Pause.manage.StartSoundtrack();
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
                    DPTrunkTwo.manage.point = 50;
                    DPTrunkTwo.manage.wheels1.gameObject.SetActive(true);
                    DPTrunkTwo.manage.wheels2.gameObject.SetActive(true);
                    DPTrunkTwo.manage.wheels1col.gameObject.SetActive(true);
                    DPTrunkTwo.manage.wheels2col.gameObject.SetActive(true);
                    Amplitude.Instance.logEvent("OnRewardedVideoForEnergy");
                }

                if (isAdsEnergyArena)
                {
                    CarDamage.manage.energy = CarDamage.manage.energy + 25;
                    CarDamage.manage.energyBarProgress.GetComponent<Text>().text = CarDamage.manage.energy.ToString();
                    CarDamage.manage.StartEngine();
                    CarDamage.manage.isDead = false;
                    ArenaManager.manage.lose.SetActive(false);
                    if (PlayerPrefs.GetInt("Soundtrack") == 0)
                    {
                        Pause.manage.StartSoundtrack();
                    }
                    isAdsEnergyArena = false;
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
                    DPTrunkTwo.manage.point = 50;
                    DPTrunkTwo.manage.wheels1.gameObject.SetActive(true);
                    DPTrunkTwo.manage.wheels2.gameObject.SetActive(true);
                    DPTrunkTwo.manage.wheels1col.gameObject.SetActive(true);
                    DPTrunkTwo.manage.wheels2col.gameObject.SetActive(true);
                    Amplitude.Instance.logEvent("OnRewardedVideoForEnergyArena");
                    //ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().StartEngine();
                    //ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().StartEngine();

                }
                if (isAdsForCarCrashed)
                {
                    CarDamage.manage.StartEngine();

                    AudioListener.pause = false;
                    PlayerPrefs.SetInt("crashed", 0);
                    CarDamage.manage.isDead = false;
                    CarDamage.manage.energy = 25;
                    if (PlayerPrefs.GetInt("Soundtrack") == 0)
                    {
                        Pause.manage.StartSoundtrack();
                    }
                    LevelManager.manage.Crashed.SetActive(false);
                    CarDamage.manage.energyBarProgress.GetComponent<Text>().text = CarDamage.manage.energy.ToString();
                    Amplitude.Instance.logEvent("OnRewardedVideoForCrashCar");
                    Time.timeScale = 1;
                    isAdsForCarCrashed = false;
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
                    DPTrunk.manage.Blow.SetActive(false);
                    DPTrunk.manage.point = 50;
                    DPTrunkTwo.manage.point = 50;
                    DPTrunkTwo.manage.wheels1.gameObject.SetActive(true);
                    DPTrunkTwo.manage.wheels2.gameObject.SetActive(true);
                    DPTrunkTwo.manage.wheels1col.gameObject.SetActive(true);
                    DPTrunkTwo.manage.wheels2col.gameObject.SetActive(true);
                    RCC_CarControllerV3.manage.ResetCrashedCar();
                }

                if (isAdsEnergyInShop)
                {
                    PlayerPrefs.SetInt("Energy", 25);
                    //MainMenuManager.manage.carSetting[PlayerPrefs.GetInt("CurrentCar")].energy += 25;
                    GameObject snd = GameObject.Find("Purchased");
                    snd.GetComponent<AudioSource>().Play();
                    MainMenuManager.manage.menuGUI.adsEnergyBtn.interactable = false;
                    
                }

                break;
            case ShowResult.Skipped:
                isAdsShop = false;
                isAdsForCarCrashed = false;
                isAds25Sec = false;
                isAdsEnergyInShop = false;
                isAdsEnergyArena = false;
                break;
            case ShowResult.Failed:
                isAdsShop = false;
                isAdsForCarCrashed = false;
                isAds25Sec = false;
                isAdsEnergyInShop = false;
                isAdsEnergyArena = false;
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
        if (!MainMenuManager.manage.isFreerideActive && !MainMenuManager.manage.isAllvsYou && !MainMenuManager.manage.isTopSpeedActive)
        {
            GameObject snd = GameObject.Find("Purchased");
            snd.GetComponent<AudioSource>().Play();
            Invoke("latencySpawnCoin", 0.5f);
            Invoke("addCoinsOnShop", 1.7f);
        }
        
    }

    void latencySpawnCoin()
    {
        MainMenuManager.manage.animate(25);
    }

    void addCoinsOnShop()
    {
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 3500f);
        Amplitude.Instance.logEvent("3500$ - OnRewardedVideoFinished");
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}


