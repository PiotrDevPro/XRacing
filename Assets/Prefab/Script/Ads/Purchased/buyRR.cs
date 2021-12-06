using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class buyRR : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "com.gamedevcorp.xracing.roadrider")
        {
            PlayerPrefs.SetInt("RRBigBoss",1);
            PlayerPrefs.SetInt("Crown", PlayerPrefs.GetInt("Crown") + 1);
            GameObject snd = GameObject.Find("Purchased");
            snd.GetComponent<AudioSource>().Play();
            Amplitude.Instance.logEvent("RRBigBoss Bought");
            Invoke("Latency",0.1f);
        }
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        print("" + product.definition.id + "failed due to" + reason);
        Amplitude.Instance.logEvent("RRBigBoss buy Failed");
    }

    void Latency()
    {
        MainMenuManager.manage.inAppRRPanel.SetActive(false);
    }
}
