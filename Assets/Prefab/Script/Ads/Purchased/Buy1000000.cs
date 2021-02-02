using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class Buy1000000 : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {

        GameObject snd = GameObject.Find("Purchased");
        snd.GetComponent<AudioSource>().Play();
        Amplitude.Instance.logEvent("1000000gold");
        Invoke("CoinLatency", 0.7f);
        Invoke("CashFlow", 1.7f);
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        print("" + product.definition.id + "failed due to" + reason);
        Amplitude.Instance.logEvent("1000000goldFailed");
    }
    void CoinLatency()
    {
        MainMenuManager.manage.animate(70);

    }
    void CashFlow()
    {
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 1000000f);
    }
}
