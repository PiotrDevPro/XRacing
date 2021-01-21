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
    public Text erndMoneyWin;
    public Text erndMoneyLose;
    public Text carDisplayWin;
    public Text carDisplayLose;
    public GameObject LapTimer;
    public GameObject Laps;
    public GameObject FinishLine;
    public GameObject AiCars1;
    public GameObject AiCars2;
    public GameObject AiCars3;


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
            FinishLine.SetActive(false);
            //AiCars.transform.position = new Vector3(-164,4,162);
            AiCars1.transform.eulerAngles = new Vector3(-2.5f,360,2);
            AiCars1.transform.position = new Vector3(55f,4.07f,100);
            AiCars2.transform.eulerAngles = new Vector3(-2.5f, 360, 2);
            AiCars2.transform.position = new Vector3(50f, 4.07f, 100);
            AiCars3.transform.eulerAngles = new Vector3(-2.5f, 360, 2);
            AiCars3.transform.position = new Vector3(46, 4.07f, 100);
        }
    }

    public void Lose()
    {
        Invoke("LatencyLose", 0.5f);
    }

    public void Winner()
    {
        Invoke("LatencyWin",0.5f);
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 5000f);
        print("Winner");
    }

    void LatencyWin()
    {
        Win.SetActive(true);
        erndMoneyWin.text = (CarAi.manage.coin + CarAi1.manage.coin + CarAi2.manage.coin).ToString();
        carDisplayWin.text = (CarDamage.manage.frag).ToString();
        winSnd.Play();
    }

    void LatencyLose()
    {
        erndMoneyLose.text = (CarAi.manage.coin + CarAi1.manage.coin + CarAi2.manage.coin).ToString();
        carDisplayLose.text = (CarDamage.manage.frag).ToString();
        lose.SetActive(true);
        loseSnd.Play();
    }

    public void EnergyForAds()
    {
        CarDamage.manage.energy = CarDamage.manage.energy + 25;
        CarDamage.manage.energyBarProgress.GetComponent<Text>().text = CarDamage.manage.energy.ToString();
        CarDamage.manage.StartEngine();
        CarDamage.manage.isDead = false;
        CarAi.manage.StartEngine();
        CarAi1.manage.StartEngine();
        CarAi2.manage.StartEngine();
        lose.SetActive(false);
    }
}
