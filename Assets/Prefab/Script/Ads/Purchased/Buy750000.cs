using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class Buy750000 : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "com.gamedevcorp.xracing.gold750000")
        {
            GameObject snd = GameObject.Find("Purchased");
            snd.GetComponent<AudioSource>().Play();
            Amplitude.Instance.logEvent("750 000 gold");
            Invoke("CoinLatency", 0.7f);
            Invoke("CashFlow", 1.7f);
        }
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        print("" + product.definition.id + "failed due to" + reason);
        Amplitude.Instance.logEvent("750000goldFailed");
    }
    void CoinLatency()
    {
        MainMenuManager.manage.animate(50);

    }
    void CashFlow()
    {
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 750000f);
    }
}
