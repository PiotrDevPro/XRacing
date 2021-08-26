using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


    public class CarAi2 : MonoBehaviour
    {
        public static CarAi2 manage;
        public GameObject Blow;
        public int energy = 100;
        public int coin = 0;

        private GameObject HP3;
        private GameObject lbHP3;
    private void Awake()
        {
            manage = this;
        }

        private void Start()
        {
        if (SceneManager.GetActiveScene().name != "level_top_speed_test")
        {
            energy = Random.Range(100, 130);
        }
        else
        {
            energy = Random.Range(140, 200);
        }
    }
        private void OnTriggerEnter(Collider other)
        {
        if (!CarDamage.manage.isDead)
        {
            if (other.CompareTag("Car") || other.CompareTag("Player"))
            {
                if (MainMenuManager.manage.isAllvsYou || MainMenuManager.manage.isFreerideActive || MainMenuManager.manage.isTopSpeedActive)
                {
                    HP3 = GameObject.Find("LifeCar3");
                    lbHP3 = GameObject.Find("carLb3");
                    HP3.GetComponent<Text>().text = energy.ToString();
                    if (GetComponent<RCC_CarControllerV3>().speed > 90)
                    {
                        PlayerPrefs.SetInt("damage", 10);
                        energy -= 5;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead2)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;
                            Blow.SetActive(true);

                            Amplitude.Instance.logEvent("Bot3Crashed90KMH");
                        }
                    }

                    if (GetComponent<RCC_CarControllerV3>().speed > 140)
                    {
                        PlayerPrefs.SetInt("damage", 20);
                        energy -= 15;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead2)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;
                            Blow.SetActive(true);

                            Amplitude.Instance.logEvent("Bot3Crashed140KMH");
                        }
                    }

                    if (GetComponent<RCC_CarControllerV3>().speed > 250)
                    {
                        PlayerPrefs.SetInt("damage", 30);
                        energy -= 25;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead2)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            coin += 500;
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            Blow.SetActive(true);

                            Amplitude.Instance.logEvent("Bot3Crashed250KMH");
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("damage", 0);
                        energy -= 2;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead2)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            coin += 500;
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            Blow.SetActive(true);

                        }
                    }
                }

                if (energy < 0)
                {
                    energy = 0;
                }
                if (energy > 50)
                {
                    lbHP3.GetComponent<Text>().color = Color.green;
                }
                if (energy < 50)
                {
                    lbHP3.GetComponent<Text>().color = Color.yellow;
                }

                if (energy < 20)
                {
                    lbHP3.GetComponent<Text>().color = Color.red;
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
            if (!CarDamage.manage.AiIsDead2)
            {
                Invoke("latencyStartEngine", 1f);
            }

        }

        void latencyStartEngine()
        {
            GetComponent<RCC_CarControllerV3>().StartEngineNow();
        }
    }

