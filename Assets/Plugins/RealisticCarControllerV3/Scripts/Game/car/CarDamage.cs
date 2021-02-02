using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
    public PhotonView photonView;

    private GameObject HP1;
    private GameObject HP2;
    private GameObject HP3;
    private GameObject lbHP1;
    private GameObject lbHP2;
    private GameObject lbHP3;

    int count = 0;
    public int frag = 0;

    private void Update()
    {
      //  print(energy);
    }

    private void Awake()
    {
        manage = this;
    }

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
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
            if (other.CompareTag("Car") || other.CompareTag("CarAI") && !isDead)
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
                energy -= 1;
                energyBarProgress.GetComponent<Text>().text = energy.ToString();
                if (GetComponent<RCC_CarControllerV3>().speed > 50)
                    {
                    energy -= 5;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                }

                if (GetComponent<RCC_CarControllerV3>().speed > 130)
                {
                    energy -= 15;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                }

                if (energy <= 0)
                {
                    energy = 0;
                    GetComponent<RCC_CarControllerV3>().KillEngine();
                    PlayerPrefs.SetInt("crashed", 1);
                    energyBarProgress.GetComponent<Text>().text = "0";
                    LevelManager.manage.Lose();
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
                
            }

            #region Enemy Car Display
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
            #endregion

        }
        if (SceneManager.GetActiveScene().name == "battle_online")
        {
            energyBarProgress = GameObject.Find("EnergyNum");

            if (other.CompareTag("Car") && !isDead)
            {
                
                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 80f )
                {
                        energy -= 3;
                        energyBarProgress.GetComponent<Text>().text = energy.ToString();
                        Amplitude.Instance.logEvent("CarHitOnspeed > 80f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 140f)
                {
                    energy -= 5;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("CarHitOnspeed > 120f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 180f)
                {
                    energy -= 10;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("CarHitOnspeed > 180f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 250f)
                {
                    energy -= 15;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("CarHitOnspeed > 250f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 300f)
                {
                    energy -= 45;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("CarHitOnspeed > 300f");
                }
                else
                {
                    energy -= 1;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                }

                if (energy <= 0)
                {
                    energy = 0;
                    GetComponent<RCC_CarControllerV3>().KillEngine();
                    energyBarProgress.GetComponent<Text>().text = "0";
                    PlayerPrefs.SetInt("crashed", 1);
                    netManager.manage.CarCrashedNetwork();
                    isDead = true;
                    Amplitude.Instance.logEvent("DeadFromTrafficCar");

                }

            }
            if (other.CompareTag("Player") && !isDead)
            {

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 70f)
                {
                    energy -= 2;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("NetworkPlayerHitOnspeed > 80f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 120f)
                {
                    energy -= 4;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("NetworkPlayerHitOnspeed > 120f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 180f)
                {
                    energy -= 6;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("NetworkPlayerHitOnspeed > 180f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 250f)
                {
                    energy -= 10;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("NetworkPlayerHitOnspeed > 250f");
                }
                if (energy <= 0)
                {
                    energy = 0;
                    GetComponent<RCC_CarControllerV3>().KillEngine();
                    PlayerPrefs.SetInt("crashed", 1);
                    netManager.manage.CarCrashedNetwork();
                    Amplitude.Instance.logEvent("DeadFromOtherNetworkPlayer");
                    isDead = true;
                }
                else
                {
                    energy -= 1;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
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
