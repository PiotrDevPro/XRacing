using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class noAds : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {

        PlayerPrefs.SetInt("NoAds", 1);
        GameObject snd = GameObject.Find("Purchased");
        snd.GetComponent<AudioSource>().Play();
        Amplitude.Instance.logEvent("+1 000 000 Coins");
        //FbManager.manage.NoAds("No ads no more");
        
        Invoke("CoinLatency", 0.5f);
        Invoke("CashFlow",1.7f);
        Invoke("TimeToDeactivate", 0.5f);
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        print("" + product.definition.id + "failed due to" + reason);
    }

    void TimeToDeactivate()
    {
       // MainMenuManager.manage.menuGUI.NoAdsBtn.SetActive(false);
    }

    void CoinLatency()
    {
        MainMenuManager.manage.animate(35);
        
    }

    void CashFlow()
    {
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 1000000f);
    }

}
