using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class buyLambo : MonoBehaviour
{
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "com.gamedevcorp.xracing.lp750")
        {
            PlayerPrefs.SetInt("boughtLP750",1);
            GameObject snd = GameObject.Find("Purchased");
            snd.GetComponent<AudioSource>().Play();
            Amplitude.Instance.logEvent("Lambo LP-750 Bought");
            Invoke("Latency",0.1f);
        }
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        print("" + product.definition.id + "failed due to" + reason);
        Amplitude.Instance.logEvent("Lambo LP-750 buy Failed");
    }

    void Latency()
    {
        MainMenuManager.manage.inAppCarsPanel.SetActive(false);
    }
}
