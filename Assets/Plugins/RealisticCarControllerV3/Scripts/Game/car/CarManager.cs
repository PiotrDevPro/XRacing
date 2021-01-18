using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public static CarManager manage;
    public GameObject lose;
    public GameObject Win;
    public AudioSource loseSnd;
    public AudioSource winSnd;
    public Text erndMoney;
    public GameObject LapTimer;
    public GameObject Laps;

    private void Awake()
    {
        manage = this;
    }

    private void Start()
    {
        lose.SetActive(false);
        Win.SetActive(false);
        if (MainMenuManager.manage.isAllvsYou)
        {
            LapTimer.SetActive(false);
            Laps.SetActive(false);
        }
    }

    public void Lose()
    {
        Invoke("LatencyLose", 0.5f);
    }

    public void Winner()
    {
        Invoke("LatencyWin",0.5f);
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 2500f);
        print("Winner");
    }

    void LatencyWin()
    {
        Win.SetActive(true);
        erndMoney.text = (CarAi.manage.coin + CarAi1.manage.coin + CarAi2.manage.coin).ToString();
        winSnd.Play();
    }

    void LatencyLose()
    {
        lose.SetActive(true);
        loseSnd.Play();
    }
}
