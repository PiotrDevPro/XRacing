using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class Donate : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {

        GameObject snd = GameObject.Find("Purchased");
        snd.GetComponent<AudioSource>().Play();
        Amplitude.Instance.logEvent("Donate");
        Invoke("CoinFx", 0.5f);

        Invoke("CoinLatency", 0.5f);
        Invoke("CashFlow", 1.7f);
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        print("" + product.definition.id + "failed due to" + reason);
    }


    void CoinLatency()
    {
        MainMenuManager.manage.animate(35);

    }

    void CashFlow()
    {
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 250000f);
    }
}
