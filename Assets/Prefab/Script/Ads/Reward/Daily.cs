using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Assertions.Must;

public class Daily : MonoBehaviour
{
    public static Daily manage;
    public float msToWait = 10000f;

    public Text Timer;
    public Button RewardButton;
    private ulong lastOpen;
    public bool isReadyB;
    private void Awake()
    {
        manage = this;
    }
    void Start()
    {
        
        lastOpen = ulong.Parse(PlayerPrefs.GetString("lastOpen"));
        if (!isReady())
        {
            RewardButton.interactable = false;
            //isReadyB = true;
        }

        if (!RewardButton.IsInteractable())
        {
            RewardButton.GetComponent<Animator>().SetBool("push", false);
        }
    }


    void Update()
    {
        if (RewardButton.IsInteractable())
        {
            RewardButton.GetComponent<Animator>().SetBool("push", true);
        }
            
        if (!RewardButton.IsInteractable())
        {
            if (isReady())
            {
                RewardButton.interactable = true;
                Timer.text = "DAILY REWARD";
                RewardButton.GetComponent<Animator>().SetBool("push", true);
                //isReadyB = false;
                return;
            }

            ulong diff = ((ulong)DateTime.Now.Ticks - lastOpen);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float seconleft = (float)(msToWait - m) / 1000f;
            string t = "";
            t += ((int)seconleft / 3600).ToString() + ":";
            seconleft -= ((int)seconleft / 3600) * 3600;
            t += ((int)seconleft / 60).ToString("00") + ":";
            t += ((int)seconleft % 60).ToString("00") + "";
            Timer.text = t.ToString();
            //GameObject anim = GameObject.Find("DayReward");
            //anim.GetComponent<Animator>().SetBool("push",true);
        }
    }

    public void Click()
    {
        Amplitude.Instance.logEvent("Daily Reward");
        lastOpen = ((ulong)DateTime.Now.Ticks);
        PlayerPrefs.SetString("lastOpen", lastOpen.ToString());
        RewardButton.interactable = false;
        GameObject snd = GameObject.Find("Purchased");
        snd.GetComponent<AudioSource>().Play();
        Invoke("latencySpawnCoin", 0.5f);
        Invoke("CashFlow", 1.7f);
        RewardButton.GetComponent<Animator>().SetBool("push", false);

    }

    void latencySpawnCoin()
    {
        MainMenuManager.manage.animate(25);
    }

    void CashFlow()
    {
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 10000f);
    }

    private bool isReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float seconleft = (float)(msToWait - m) / 1000f;

        if (seconleft < 0)
        {
            Timer.text = "DAILY REWARD";
            return true;
        }

        return false;
    }
}
