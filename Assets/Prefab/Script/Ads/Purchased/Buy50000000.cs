using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class Buy50000000 : MonoBehaviour
{

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "com.gamedevcorp.xracing.gold50000000")
        {
            GameObject snd = GameObject.Find("Purchased");
            snd.GetComponent<AudioSource>().Play();
            Amplitude.Instance.logEvent("50 000 000gold");
            Invoke("CoinLatency", 0.7f);
            Invoke("CashFlow", 1.7f);
        }
        
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        print("" + product.definition.id + "failed due to" + reason);
        Amplitude.Instance.logEvent("50 000 000goldFailed");
    }
    void CoinLatency()
    {
        MainMenuManager.manage.animate(75);

    }
    void CashFlow()
    {
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 50000000f);
    }
}
