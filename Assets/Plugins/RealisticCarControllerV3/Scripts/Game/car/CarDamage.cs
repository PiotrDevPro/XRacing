using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class CarDamage : MonoBehaviour
{
    public static CarDamage manage;
    public int energy = 100;
    public bool isDead = false;
    public bool isWin = false;
    public bool isAdsShowed = false;
    public bool AiIsDead = false;
    public bool AiIsDead1 = false;
    public bool AiIsDead2 = false;

    public GameObject energyBarProgress;
    private GameObject enemyDestroy;

    private GameObject HP1;
    private GameObject HP2;
    private GameObject HP3;
    private GameObject lbHP1;
    private GameObject lbHP2;
    private GameObject lbHP3;

    int count = 0;
    public int frag = 0;

    private void Awake()
    {
        manage = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "level_lap6")
        {
            if (other.CompareTag("CarAI") && !isWin)

                if (MainMenuManager.manage.isFreerideActive && !isDead || MainMenuManager.manage.isAllvsYou && !isDead)
                {
                    energyBarProgress = GameObject.Find("LifeGauge");
                    enemyDestroy = GameObject.Find("currenemy");
                    PlayerPrefs.SetInt("crashed", 0);
                    HP1 = GameObject.Find("LifeCar1");
                    HP2 = GameObject.Find("LifeCar2");
                    HP3 = GameObject.Find("LifeCar3");
                    lbHP1 = GameObject.Find("carLbl");
                    lbHP2 = GameObject.Find("carLbl1");
                    lbHP3 = GameObject.Find("carLbl2");
                    HP1.GetComponent<Text>().text = (CarAi.manage.energy).ToString();
                    HP2.GetComponent<Text>().text = (CarAi1.manage.energy).ToString();
                    HP3.GetComponent<Text>().text = (CarAi2.manage.energy).ToString();
                    energy -= 1 + PlayerPrefs.GetInt("damage");

                    if (energy <= 0)
                    {
                        energy = 0;
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        PlayerPrefs.SetInt("crashed", 1);
                        energyBarProgress.GetComponent<Text>().text = "0";
                        CarManager.manage.Lose();
                        isDead = true;
                        Pause.manage.tracks[0].Stop();
                        Pause.manage.tracks[1].Stop();
                        Pause.manage.tracks[2].Stop();
                        Pause.manage.tracks[3].Stop();
                        Pause.manage.tracks[4].Stop();
                        Pause.manage.tracks[5].Stop();
                        Pause.manage.tracks[6].Stop();
                        Amplitude.Instance.logEvent("PlayerIsDead");
                    }

                    if (CarAi.manage.energy <= 0 && !AiIsDead)
                    {

                        frag += 1;
                        enemyDestroy.GetComponent<Text>().text = frag.ToString();
                        AiIsDead = true;
                        Amplitude.Instance.logEvent("CarIsDead1");

                    }

                    if (CarAi1.manage.energy <= 0 && !AiIsDead1)
                    {
                        frag += 1;
                        enemyDestroy.GetComponent<Text>().text = frag.ToString();
                        AiIsDead1 = true;
                        Amplitude.Instance.logEvent("CarIsDead2");
                    }

                    if (CarAi2.manage.energy <= 0 && !AiIsDead2)
                    {
                        frag += 1;
                        enemyDestroy.GetComponent<Text>().text = frag.ToString();
                        AiIsDead2 = true;
                        Amplitude.Instance.logEvent("CarIsDead3");
                    }

                    if (CarAi.manage.energy <= 0 && CarAi1.manage.energy <= 0 && CarAi2.manage.energy <= 0)
                    {
                        CarManager.manage.Winner();
                        isWin = true;
                        Amplitude.Instance.logEvent("PlayerIsWinBattle");
                    }

                    #region Car Display
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    HP1.GetComponent<Text>().text = (CarAi.manage.energy).ToString();
                    if (CarAi.manage.energy < 0)
                    {
                        CarAi.manage.energy = 0;
                    }
                    if (CarAi.manage.energy > 50)
                    {
                        lbHP1.GetComponent<Text>().color = Color.green;
                    }
                    if (CarAi.manage.energy < 50)
                    {
                        lbHP1.GetComponent<Text>().color = Color.yellow;
                    }

                    if (CarAi.manage.energy < 20)
                    {
                        lbHP1.GetComponent<Text>().color = Color.red;
                    }

                    HP2.GetComponent<Text>().text = (CarAi1.manage.energy).ToString();
                    if (CarAi1.manage.energy < 0)
                    {
                        CarAi1.manage.energy = 0;
                    }
                    if (CarAi1.manage.energy > 50)
                    {
                        lbHP2.GetComponent<Text>().color = Color.green;
                    }
                    if (CarAi1.manage.energy < 50)
                    {
                        lbHP2.GetComponent<Text>().color = Color.yellow;
                    }

                    if (CarAi1.manage.energy < 20)
                    {
                        lbHP2.GetComponent<Text>().color = Color.red;
                    }
                    HP3.GetComponent<Text>().text = (CarAi2.manage.energy).ToString();
                    if (CarAi2.manage.energy < 0)
                    {
                        CarAi2.manage.energy = 0;
                    }
                    if (CarAi2.manage.energy > 50)
                    {
                        lbHP3.GetComponent<Text>().color = Color.green;
                    }
                    if (CarAi2.manage.energy < 50)
                    {
                        lbHP3.GetComponent<Text>().color = Color.yellow;
                    }

                    if (CarAi2.manage.energy < 20)
                    {
                        lbHP3.GetComponent<Text>().color = Color.red;
                    }
                    #endregion
                }
        }

        if (SceneManager.GetActiveScene().name == "level_top_speed_test")
        {
            
            if (other.CompareTag("Car"))
            {
                if (GetComponent<RCC_CarControllerV3>().speed > 130)
                {
                    GetComponent<RCC_CarControllerV3>().KillEngine();
                    LevelManager.manage.Lose();
                    PlayerPrefs.SetInt("crashed", 1);
                    Invoke("Latency",5f);
                    Pause.manage.tracks[0].Stop();
                    Pause.manage.tracks[1].Stop();
                    Pause.manage.tracks[2].Stop();
                    Pause.manage.tracks[3].Stop();
                    Pause.manage.tracks[4].Stop();
                    Pause.manage.tracks[5].Stop();
                    Pause.manage.tracks[6].Stop();
                    Amplitude.Instance.logEvent("PlayerIsDead");
                }
            }
        }
    }

    void Latency()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void StartEngine()
    {
        GetComponent<RCC_CarControllerV3>().KillOrStartEngine();
    }

}
