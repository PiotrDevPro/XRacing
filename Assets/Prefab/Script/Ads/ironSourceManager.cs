using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ironSourceManager : MonoBehaviour
{
    public static ironSourceManager manage;

    private void Awake()
    {
        manage = this;
    }

    /*
    void Start()
    {
#if UNITY_ANDROID
        string appKey = "d6849cb1";
#elif UNITY_IPHONE
        string appKey = "";
#else
        string appKey = "unexpected_platform";
#endif
        Debug.Log("unity-script: MyAppStart Start called");
        //IronSource.Agent.validateIntegration();
        //Dynamic config example
        IronSourceConfig.Instance.setClientSideCallbacks(true);

        string id = IronSource.Agent.getAdvertiserId();
        Debug.Log("unity-script: IronSource.Agent.getAdvertiserId : " + id);

        Debug.Log("unity-script: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration();

        Debug.Log("unity-script: unity version" + IronSource.unityVersion());

        // Add Banner Events
        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

        //Add Rewarded Video Events
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;

        //Add Rewarded Video DemandOnly Events
        IronSourceEvents.onRewardedVideoAdOpenedDemandOnlyEvent += RewardedVideoAdOpenedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdClosedDemandOnlyEvent += RewardedVideoAdClosedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdLoadedDemandOnlyEvent += this.RewardedVideoAdLoadedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdRewardedDemandOnlyEvent += RewardedVideoAdRewardedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedDemandOnlyEvent += RewardedVideoAdShowFailedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdClickedDemandOnlyEvent += RewardedVideoAdClickedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdLoadFailedDemandOnlyEvent += this.RewardedVideoAdLoadFailedDemandOnlyEvent;

        // Add Interstitial Events
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        // Add Interstitial DemandOnly Events
        IronSourceEvents.onInterstitialAdReadyDemandOnlyEvent += InterstitialAdReadyDemandOnlyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedDemandOnlyEvent += InterstitialAdLoadFailedDemandOnlyEvent;
        IronSourceEvents.onInterstitialAdShowFailedDemandOnlyEvent += InterstitialAdShowFailedDemandOnlyEvent;
        IronSourceEvents.onInterstitialAdClickedDemandOnlyEvent += InterstitialAdClickedDemandOnlyEvent;
        IronSourceEvents.onInterstitialAdOpenedDemandOnlyEvent += InterstitialAdOpenedDemandOnlyEvent;
        IronSourceEvents.onInterstitialAdClosedDemandOnlyEvent += InterstitialAdClosedDemandOnlyEvent;

        // Add Offerwall Events
        IronSourceEvents.onOfferwallClosedEvent += OfferwallClosedEvent;
        IronSourceEvents.onOfferwallOpenedEvent += OfferwallOpenedEvent;
        IronSourceEvents.onOfferwallShowFailedEvent += OfferwallShowFailedEvent;
        IronSourceEvents.onOfferwallAdCreditedEvent += OfferwallAdCreditedEvent;
        IronSourceEvents.onGetOfferwallCreditsFailedEvent += GetOfferwallCreditsFailedEvent;
        IronSourceEvents.onOfferwallAvailableEvent += OfferwallAvailableEvent;
    // SDK init
    Debug.Log("unity-script: IronSource.Agent.init");
        IronSource.Agent.init(appKey);
        
        IronSource.Agent.loadInterstitial();
        //IronSource.Agent.init (appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
        //IronSource.Agent.initISDemandOnly (appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL);

        //Set User ID For Server To Server Integration
        //// IronSource.Agent.setUserId ("UserId");

        // Load Banner example
       // IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.TOP);

    }

    void OnApplicationPause(bool isPaused)
    {
        Debug.Log("unity-script: OnApplicationPause = " + isPaused);
        IronSource.Agent.onApplicationPause(isPaused);
    }

    void CoinLatency()
    {
        MainMenuManager.manage.animate(10);
    }

    #region Banner
    //Banner Events
    void BannerAdLoadedEvent()
    {
        Debug.Log("unity-script: I got BannerAdLoadedEvent");
    }

    void BannerAdLoadFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
    }

    void BannerAdClickedEvent()
    {
        Debug.Log("unity-script: I got BannerAdClickedEvent");
    }

    void BannerAdScreenPresentedEvent()
    {
        Debug.Log("unity-script: I got BannerAdScreenPresentedEvent");
    }

    void BannerAdScreenDismissedEvent()
    {
        Debug.Log("unity-script: I got BannerAdScreenDismissedEvent");
    }

    void BannerAdLeftApplicationEvent()
    {
        Debug.Log("unity-script: I got BannerAdLeftApplicationEvent");
    }
    #endregion

    #region Reward Video

    public void ShowRewardedVideoButtonClicked()
    {

        Debug.Log("unity-script: ShowRewardedVideoButtonClicked");
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
        }

        // DemandOnly
        // ShowDemandOnlyRewardedVideo ();
    }

    //Invoked when the RewardedVideo ad view has opened.
    //Your Activity will lose focus. Please avoid performing heavy 
    //tasks till the video ad will be closed.
    void RewardedVideoAdOpenedEvent()
    {
        
    }
    //Invoked when the RewardedVideo ad view is about to be closed.
    //Your activity will now regain its focus.
    void RewardedVideoAdClosedEvent()
    {
    }
    //Invoked when there is a change in the ad availability status.
    //@param - available - value will change to true when rewarded videos are available. 
    //You can then show the video by calling showRewardedVideo().
    //Value will change to false when no videos are available.
    void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        //Change the in-app 'Traffic Driver' state according to availability.
        bool rewardedVideoAvailability = available;
    }

    //Invoked when the user completed the video and should be rewarded. 
    //If using server-to-server callbacks you may ignore this events and wait for 
    // the callback from the  ironSource server.
    //@param - placement - placement object which contains the reward data
    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        Amplitude.Instance.logEvent("RewardVideoShowSuccess");
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 2500f);
        GameObject snd = GameObject.Find("Purchased");
        snd.GetComponent<AudioSource>().Play();
        Invoke("CoinLatency", 0.5f);


    }
    //Invoked when the Rewarded Video failed to show
    //@param description - string - contains information about the failure.
    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
    }

    // ----------------------------------------------------------------------------------------
    // Note: the events below are not available for all supported rewarded video ad networks. 
    // Check which events are available per ad network you choose to include in your build. 
    // We recommend only using events which register to ALL ad networks you include in your build. 
    // ----------------------------------------------------------------------------------------

    //Invoked when the video ad starts playing. 
    void RewardedVideoAdStartedEvent()
    {
    }
    //Invoked when the video ad finishes playing. 
    void RewardedVideoAdEndedEvent()
    {
        
    }

    void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
    {
        Debug.Log("unity-script: I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName());
    }

    /************* RewardedVideo DemandOnly Delegates *************/
    /*
    void RewardedVideoAdLoadedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdLoadedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        Debug.Log("unity-script: I got RewardedVideoAdLoadFailedDemandOnlyEvent for instance: " + instanceId + ", code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void RewardedVideoAdOpenedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdOpenedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdRewardedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdRewardedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdClosedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdClosedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        Debug.Log("unity-script: I got RewardedVideoAdShowFailedDemandOnlyEvent for instance: " + instanceId + ", code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void RewardedVideoAdClickedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdClickedDemandOnlyEvent for instance: " + instanceId);
    }
    #endregion

    #region Interstitial

    public void ShowInterstitialButtonClicked()
    {
        Amplitude.Instance.logEvent("ShowInterstitialButtonClick");
        if (PlayerPrefs.GetInt("NoAds") != 1)
        {
            Debug.Log("unity-script: ShowInterstitialButtonClicked");
            if (IronSource.Agent.isInterstitialReady())
            {
                IronSource.Agent.showInterstitial();
                
            }
            else
            {
                Debug.Log("unity-script: IronSource.Agent.isInterstitialReady - False");
            }
        }
        // DemandOnly
        // ShowDemandOnlyInterstitial ();
    }

    public void LoadInterstitialReady()
    {
        IronSource.Agent.loadInterstitial();
        //DemandOnly
        // LoadDemandOnlyInterstitial ();
    }
    //Invoked when the initialization process has failed.
    //@param description - string - contains information about the failure.
    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
    }
    //Invoked right before the Interstitial screen is about to open.
    void InterstitialAdShowSucceededEvent()
    {
        Amplitude.Instance.logEvent("ShowInterstitialStart");
    }
    //Invoked when the ad fails to show.
    //@param description - string - contains information about the failure.
    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
    }
    // Invoked when end user clicked on the interstitial ad
    void InterstitialAdClickedEvent()
    {

    }
    //Invoked when the interstitial ad closed and the user goes back to the application screen.
    void InterstitialAdClosedEvent()
    {
        Amplitude.Instance.logEvent("ShowInterstitialFinish");
    }
    //Invoked when the Interstitial is Ready to shown after load function is called
    void InterstitialAdReadyEvent()
    {
    }
    //Invoked when the Interstitial Ad Unit has opened
    void InterstitialAdOpenedEvent()
    {
        IronSource.Agent.loadInterstitial();

    }
    
    /************* Interstitial DemandOnly Delegates *************/
    /*
    void InterstitialAdReadyDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdReadyDemandOnlyEvent for instance: " + instanceId);
    }

    void InterstitialAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdLoadFailedDemandOnlyEvent for instance: " + instanceId + ", error code: " + error.getCode() + ",error description : " + error.getDescription());
    }

    void InterstitialAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdShowFailedDemandOnlyEvent for instance: " + instanceId + ", error code :  " + error.getCode() + ",error description : " + error.getDescription());
    }

    void InterstitialAdClickedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdClickedDemandOnlyEvent for instance: " + instanceId);
    }

    void InterstitialAdOpenedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdOpenedDemandOnlyEvent for instance: " + instanceId);
        //ShowText.GetComponent<UnityEngine.UI.Text>().color = UnityEngine.Color.red;
    }

    void InterstitialAdClosedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdClosedDemandOnlyEvent for instance: " + instanceId);
    }
    #endregion

    #region Offerwall

    public void ShowOfferwallButtonClicked()
    {
        if (PlayerPrefs.GetInt("NoAds") != 1)
        {
            if (IronSource.Agent.isOfferwallAvailable())
            {
                Amplitude.Instance.logEvent("showOfferwallAds");
                IronSource.Agent.showOfferwall();
                
            }
            else
            {
                Debug.Log("IronSource.Agent.isOfferwallAvailable - False");
            }
        }
    }

    void OfferwallOpenedEvent()
    {
        Debug.Log("I got OfferwallOpenedEvent");
    }

    void OfferwallClosedEvent()
    {
        Debug.Log("I got OfferwallClosedEvent");
    }

    void OfferwallShowFailedEvent(IronSourceError error)
    {
        Debug.Log("I got OfferwallShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void OfferwallAdCreditedEvent(Dictionary<string, object> dict)
    {
        Debug.Log("I got OfferwallAdCreditedEvent, current credits = " + dict["credits"] + " totalCredits = " + dict["totalCredits"]);

    }

    void GetOfferwallCreditsFailedEvent(IronSourceError error)
    {
        Debug.Log("I got GetOfferwallCreditsFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void OfferwallAvailableEvent(bool canShowOfferwal)
    {
        Debug.Log("I got OfferwallAvailableEvent, value = " + canShowOfferwal);
        if (canShowOfferwal)
        {

        }
        else
        {

        }
    }
    #endregion

    */
}
