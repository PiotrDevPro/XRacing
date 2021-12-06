using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

    public class CarAi : MonoBehaviour
    {
        public static CarAi manage;
        public GameObject Blow;
        public int energy = 100;
        public int coin = 0;
        private GameObject HP1;
        private GameObject lbHP1;

        int c = 0;

    private void Awake()
        {
            manage = this;
        }

        private void Start()
        {

        if (SceneManager.GetActiveScene().name == "level_lap6")
        {
            energy = Random.Range(100, 130);
        } 


        if (SceneManager.GetActiveScene().name == "level_top_speed_test")
        {
            energy = Random.Range(140, 200);
        }

        if (SceneManager.GetActiveScene().name == "_arena_1" && MainMenuManager.manage.isArena1)
        {
            
            energy = Random.Range(100, 150);
            print(energy);
        }

    }

    private void Update()
    {
        c += 1;
        if (c == 1)
        {
            HP1 = GameObject.Find("LifeCar1");
            lbHP1 = GameObject.Find("carLbl");
            HP1.GetComponent<Text>().text = energy.ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
        {
        if (!CarDamage.manage.isDead)
        {
            if (other.CompareTag("Car") || other.CompareTag("Player")) //|| other.CompareTag("baseball_bat"))
            {
                if (MainMenuManager.manage.isAllvsYou || MainMenuManager.manage.isFreerideActive || MainMenuManager.manage.isTopSpeedActive || MainMenuManager.manage.isArena1)
                {
                    //print("PlayerDetect");
                    //if (baseball_b.manage.isAiCarDetect)
                    //{
                    //   energy -= PlayerPrefs.GetInt("damageAi");
                    //   print("baseball_kick" + PlayerPrefs.GetInt("damageAi"));
                    //  print(energy);
                    //   if (energy <= 0 && !CarDamage.manage.AiIsDead)
                    //   {
                    //       energy = 0;
                    //       GetComponent<RCC_CarControllerV3>().KillEngine();
                    //      Blow.SetActive(true);
                    //      PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                    //      coin += 500;
                    //      Amplitude.Instance.logEvent("Bot1Crashed90KMH");
                    //   }
                    //}

                    
                    if (GetComponent<RCC_CarControllerV3>().speed > 90)
                    {
                        PlayerPrefs.SetInt("damage", 10);
                        energy -= 5;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            Blow.SetActive(true);
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;
                            Amplitude.Instance.logEvent("Bot1Crashed90KMH");
                        }
                    }

                    if (GetComponent<RCC_CarControllerV3>().speed > 160)
                    {
                        PlayerPrefs.SetInt("damage", 20);
                        energy -= 15;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            Blow.SetActive(true);
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;
                            Amplitude.Instance.logEvent("Bot1Crashed140KMH");
                        }
                    }

                    if (GetComponent<RCC_CarControllerV3>().speed > 250)
                    {
                        PlayerPrefs.SetInt("damage", 30);
                        energy -= 25;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            Blow.SetActive(true);
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;

                            Amplitude.Instance.logEvent("Bot1Crashed250KMH");
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("damage", 0);
                        energy -= 2;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            Blow.SetActive(true);
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;

                            Amplitude.Instance.logEvent("Bot1Crashed");
                        }
                    }
                }
                if (energy < 0)
                {
                    energy = 0;
                }
                if (energy > 50)
                {
                    lbHP1.GetComponent<Text>().color = Color.green;
                }
                if (energy < 50)
                {
                    lbHP1.GetComponent<Text>().color = Color.yellow;
                }

                if (energy < 20)
                {
                    lbHP1.GetComponent<Text>().color = Color.red;
                }
            }
            if (CarDamage.manage.isDead && !MainMenuManager.manage.isTopSpeedActive)
            {
                GetComponent<RCC_CarControllerV3>().KillEngine();
            }
        }   
    }

        public void StartEngine()
        {
            if (!CarDamage.manage.AiIsDead)
            {
                Invoke("latencyStartEngine", 1f);
            }

        }

        void latencyStartEngine()
        {
            GetComponent<RCC_CarControllerV3>().StartEngineNow();
        }

    }