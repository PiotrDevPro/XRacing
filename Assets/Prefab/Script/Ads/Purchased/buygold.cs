using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class buygold : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {

        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 2000000f);
        GameObject snd = GameObject.Find("Purchased");
        snd.GetComponent<AudioSource>().Play();
        Amplitude.Instance.logEvent("Buy 2 000 000 coins");
        Invoke("CoinFx",0.5f);
        //FbManager.manage.NoAds("Buy 2 000 000 coins");
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        print("" + product.definition.id + "failed due to" + reason);
    }

    void CoinFx()
    {
        MainMenuManager.manage.animate(25);
    }
}
