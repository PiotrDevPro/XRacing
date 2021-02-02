using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class Buy500000 : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {

        GameObject snd = GameObject.Find("Purchased");
        snd.GetComponent<AudioSource>().Play();
        Amplitude.Instance.logEvent("500000gold");
        Invoke("CoinLatency", 0.7f);
        Invoke("CashFlow", 1.7f);
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        print("" + product.definition.id + "failed due to" + reason);
        Amplitude.Instance.logEvent("500000goldFailed");
    }
    void CoinLatency()
    {
        MainMenuManager.manage.animate(35);

    }
    void CashFlow()
    {
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500000f);
    }
}

