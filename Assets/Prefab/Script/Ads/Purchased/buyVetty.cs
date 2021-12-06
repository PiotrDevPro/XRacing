using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class buyVetty : MonoBehaviour
{

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "com.gamedevcorp.xracing.vetty")
        {
            PlayerPrefs.SetInt("Vetty", 1);
            PlayerPrefs.SetInt("Crown", PlayerPrefs.GetInt("Crown") + 1);
            GameObject snd = GameObject.Find("Purchased");
            snd.GetComponent<AudioSource>().Play();
            Amplitude.Instance.logEvent("Vetty Bought");
            Invoke("Latency",0.1f);
        }
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        print("" + product.definition.id + "failed due to" + reason);
        Amplitude.Instance.logEvent("Vetty buy Failed");
    }

    void Latency()
    {
        MainMenuManager.manage.inAppVettyPanel.SetActive(false);
    }

}
