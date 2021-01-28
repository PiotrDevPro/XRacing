using System.Collections;
using System.Collections.Generic;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine.UI;
using UnityEngine;


    public class AppDealManager : MonoBehaviour, IInterstitialAdListener, INonSkippableVideoAdListener, IBannerAdListener, IRewardedVideoAdListener
    {
        public static AppDealManager manage;
        public bool isAdsShop = false;
        public bool isAdsEnergy = false;
        public bool isAdsForCarCrashed = false;
        public bool isAdsForSecCheckpoint = false;
        //public bool isCoin4x = false;
        //public bool isSubShow = false;
        //public bool isAdRestart = false;
        //public bool isMagicBox = false;
        private int count = 0;
#if UNITY_IOS || UNITY_IPHONE
    private const string APP_KEY = "";
#else
        private const string APP_KEY = "96ef05657cdb77782e1b8746bcefb3d431c0b22a31831a55";
#endif
    private void Awake()
    {
        manage = this;
    }

    private void Start()
    {
        //Appodeal.setLogLevel(Appodeal.LogLevel.Debug);
        Intiliaze(true);
        //ShowBanner();

    }

    private void Intiliaze(bool isTesting)
    {
        Appodeal.setTesting(isTesting);
        Appodeal.muteVideosIfCallsMuted(true);
        Appodeal.initialize(APP_KEY, Appodeal.INTERSTITIAL | Appodeal.NON_SKIPPABLE_VIDEO | Appodeal.BANNER_VIEW | Appodeal.REWARDED_VIDEO);
        Appodeal.setBannerCallbacks(this);
        Appodeal.setInterstitialCallbacks(this);
        Appodeal.setRewardedVideoCallbacks(this);

    }

    public void ShowInterstatial()
    {
        if (PlayerPrefs.GetInt("NoAds") != 1)
        {

            if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
            {
                Appodeal.show(Appodeal.INTERSTITIAL);
                Amplitude.Instance.logEvent("ShowInterstatial", Interstitial);
                AudioListener.pause = false;
            }
        }
    }
    public void ShowNonSkipable()
    {
        if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
        {
            Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
            GameObject ButtnCoin = GameObject.Find("x2Coin");
            ButtnCoin.SetActive(false);
            //levelGenerated.manage.levelCoin4xAnalitics();
            //levelGenerated.manage.Next();
        }
        else
        {
            //Main.manage.NoWiFi.SetActive(true);
            //GameObject snd1 = GameObject.Find("Error");
            //snd1.GetComponent<AudioSource>().Play();

        }

    }
    public void ShowRewardedAds20Video()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
            isAdsShop = true;
        }
        else
        {
            //Main.manage.NoWiFi.SetActive(true);
            //GameObject snd1 = GameObject.Find("Error");
            //snd1.GetComponent<AudioSource>().Play();

        }
    }
    public void ShowRewardedAdsForSecCheckpoint()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
            isAdsForSecCheckpoint = true;
        }
    }
    public void ShowAdsForEnergy()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
            isAdsEnergy = true;
        }
    }
    public void ShowAdsForCarCrashed()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
            isAdsForCarCrashed = true;
        }
    }
    public void ShowRVForMagicBox()
        {

            {
                if (Appodeal.canShow(Appodeal.REWARDED_VIDEO))
                {
                    Appodeal.show(Appodeal.REWARDED_VIDEO);
                }
                else
                {
                    //Main.manage.NoWiFi.SetActive(true);
                    //GameObject snd1 = GameObject.Find("Error");
                    //snd1.GetComponent<AudioSource>().Play();

                }
            }
        }
    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("NoAds") != 1)
        {

            Appodeal.show(Appodeal.BANNER_BOTTOM, "default");

        }
    }


        #region CoinFX

        void Latency()
        {
            //isAdsShop = false;
            //isCoin4x = false;
            //isAdRestart = false;
            //isMagicBox = false;
        }
        void latencySpawnCoin15()
        {
            GameObject sndFx = GameObject.Find("coinStart");
            sndFx.GetComponent<AudioSource>().Play();
            //CoinDotween.manage.animate(15);
        }
        void latencySpawnCoin20()
        {
            GameObject sndFx = GameObject.Find("coinStart");
            sndFx.GetComponent<AudioSource>().Play();
            //CoinDotween.manage.animate(20);
        }
        void latencyCoinUpdate15()
        {

            StartCoroutine(CoinGet());
            // Main.manage._coinFx.SetActive(true);

        }
        void latencyCoinUpdate20()
        {

            StartCoroutine(CoinGet20());
            // Main.manage._coinFx.SetActive(true);

        }
        IEnumerator CoinGet20()
        {

            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);
            GameObject sndfx = GameObject.Find("coinGet");
            sndfx.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);
            sndfx.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);
            sndfx.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 5);
            sndfx.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 5);
            sndfx.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 7);
            sndfx.GetComponent<AudioSource>().Play();
        }
        IEnumerator CoinGet()
        {
            ;
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);
            GameObject sndfx = GameObject.Find("coinGet");
            sndfx.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);
            sndfx.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);
            sndfx.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 4);
            sndfx.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 4);
            sndfx.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 4);
            sndfx.GetComponent<AudioSource>().Play();
        }
        void TimeToDeactivate()
        {
            //Main.manage._coinFx.SetActive(false);
            //isNoDoubleAds = false;
        }

        #endregion

        #region Dictionary of Events
        Dictionary<string, object> Ads20 = new Dictionary<string, object>() {
            {"Source" , "Store"},
            {"Type" , "RV"},
        };
        Dictionary<string, object> Coin4x = new Dictionary<string, object>() {
            {"Source" , "EndLvl"},
            {"Type" , "RV"},
        };
        Dictionary<string, object> Interstitial = new Dictionary<string, object>() {
            {"Source" , "EndLvl"},
            {"Type" , "Skip-video"},
        };
        Dictionary<string, object> Restart = new Dictionary<string, object>() {
            {"Source" , "Restart4AD"},
            {"Type" , "RV"},
        };

        #endregion

        #region InterstitialAd
        public void onInterstitialLoaded(bool isPrecache)
        {
            print("onInterstitialLoaded");
        }

        public void onInterstitialFailedToLoad()
        {
            print("onInterstitialFailedToLoad");
        }

        public void onInterstitialShowFailed()
        {
            print("onInterstitialShowFailed");
        }

        public void onInterstitialShown()
        {
            print("onInterstitialShown");
        }

        public void onInterstitialClosed()
        {
            print("onInterstitialClosed");
        }

        public void onInterstitialClicked()
        {
            print("onInterstitialClicked");
        }

        public void onInterstitialExpired()
        {
            print("onInterstitialExpired");
        }

        #endregion

        #region NonSkippableVideo

        public void onNonSkippableVideoLoaded(bool isPrecache)
        {

        }

        public void onNonSkippableVideoFailedToLoad()
        {
        }

        public void onNonSkippableVideoShowFailed()
        {

        }

        public void onNonSkippableVideoShown()
        {

        }

        public void onNonSkippableVideoFinished()
        {
            //PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 15);
            //Main.manage._coinFx.SetActive(true);
            Amplitude.Instance.logEvent("onNonSkippableVideoFinished");

            //Invoke("TimeToDeactivate", 0.5f);
            //print("onNonSkippableVideoFinished");
        }

        public void onNonSkippableVideoClosed(bool finished)
        {

        }

        public void onNonSkippableVideoExpired()
        {

        }
        #endregion

        #region Banner
        public void onBannerLoaded(int height, bool isPrecache)
        {

        }

        public void onBannerFailedToLoad()
        {

        }

        public void onBannerShown()
        {

        }

        public void onBannerClicked()
        {

        }

        public void onBannerExpired()
        {

        }


        #endregion

        #region Rewarder Video

        public void onRewardedVideoLoaded(bool precache)
        {
            print("onRewardedVideoLoaded");
        }

        public void onRewardedVideoFailedToLoad()
        {
            print("onRewardedVideoFailedToLoad");
        }

        public void onRewardedVideoShowFailed()
        {
            print("onRewardedVideoShowFailed");
        }

        public void onRewardedVideoShown()
        {
            print("onRewardedVideoShown");
        }

        public void onRewardedVideoFinished(double amount, string name)
        {
            if (isAdsShop)
            {
                PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 2500f);
                Amplitude.Instance.logEvent("OnRewardedVideoFinished");
                isAdsShop = false;
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
                isAdsForCarCrashed = false;
                Amplitude.Instance.logEvent("OnRewardedVideoForCrashCar");
                Time.timeScale = 1;
            }

            if (isAdsForSecCheckpoint)
            {
            Checkpoint.manage.ContinueAdsForTimeCheckpoint();
            Amplitude.Instance.logEvent("OnRewardedVideoForCheckpoint");
            isAdsForSecCheckpoint = false;
        }
     }


        public void onRewardedVideoClosed(bool finished)
        {
            isAdsShop = false;
            isAdsEnergy = false;
            isAdsForCarCrashed = false;
            Time.timeScale = 1;
        }

        public void onRewardedVideoExpired()
        {
            print("onRewardedVideoExpired");
        }

        public void onRewardedVideoClicked()
        {
            print("onRewardedVideoClicked");
        }
        private void Update()
        {

        }
        #endregion
    }

